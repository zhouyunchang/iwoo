using DotNetOpenAuth.OAuth2;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Cben.Erp.Api
{
    public abstract class ApiBase
    {
        protected static readonly Uri ApiHost;

        static ApiBase()
        {
            string strHost = ConfigurationManager.AppSettings["ApiHost"];
            if (string.IsNullOrEmpty(strHost))
                throw new ConfigurationErrorsException("`ApiHost` invalid.");
            ApiHost = new Uri(strHost);
        }

        public ApiBase()
            : this(new Auth())
        {
        }

        public ApiBase(IAuth auth)
        {
            Auth = auth;
        }


        private int _timeout = 30 * 1000;

        /// <summary>
        /// 请求允许超时时间 
        /// </summary>
        public int Timeout
        {
            get
            {
                return _timeout;
            }
            set
            {
                _timeout = value;
            }
        }

        public IAuth Auth
        {
            get; private set;
        }

        private WebResponse Request(Uri url, string method, object body)
        {
            try
            {
                if (method == HttpMethod.GET && body != null)       // url encode
                {
                    url = UrlHelper.CombinQueryPath(url, body);
                }

                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                request.ContentType = "application/json";
                request.Method = method;
                request.Timeout = _timeout;
                if (body == null || method == HttpMethod.GET)
                {
                    request.ContentLength = 0;
                }
                Auth.Authorize(request);
                if (method != HttpMethod.GET && body != null)
                {
                    string jsonVal = JsonConvert.SerializeObject(body, Formatting.None);
                    using (var writer = new StreamWriter(request.GetRequestStream()))
                    {
                        writer.Write(jsonVal);
                        writer.Flush();
                    }
                }

                return request.GetResponse();
            }
            catch (WebException ex)
            {
                string content = "";
                var response = (HttpWebResponse)ex.Response;
                if (response == null)
                    throw ex;
                HttpStatusCode statusCode = response.StatusCode;
                if (statusCode == HttpStatusCode.Forbidden || statusCode == HttpStatusCode.Unauthorized)
                {
                    throw new WebException("无权限", ex, WebExceptionStatus.RequestCanceled, response);
                }
                using (var reader = new StreamReader(response.GetResponseStream()))
                {
                    content = reader.ReadToEnd();
                }
                if (string.IsNullOrEmpty(content))          // 无内容, 则直接抛出异常
                    throw ex;
                else if (statusCode == HttpStatusCode.BadRequest)       // 400, 为调用异常
                    throw new ApplicationException(content);
                else
                    throw new WebException(content, ex);
            }

        }

        public void Authorization(params string[] scopes)
        {
            Auth.Authorization(scopes);
        }

        protected bool Request(string apiRelativeUri, string method, object body)
        {
            Uri url = new Uri(ApiHost, apiRelativeUri);
            using (HttpWebResponse response = (HttpWebResponse)Request(url, method, body))
            {
                if (response.StatusCode == HttpStatusCode.Accepted ||
                    response.StatusCode == HttpStatusCode.OK ||
                    response.StatusCode == HttpStatusCode.NoContent)
                    return true;
                else
                    return false;

            }
        }

        protected bool Request(string apiRelativeUri, string method)
        {
            return Request(apiRelativeUri, method, null);
        }

        protected T Request<T>(string apiRelativeUri, string method, object body)
        {
            Uri url = new Uri(ApiHost, apiRelativeUri);

            try
            {
                using (HttpWebResponse response = (HttpWebResponse)Request(url, method, body))
                {
                    if (response.StatusCode == HttpStatusCode.Accepted ||
                        response.StatusCode == HttpStatusCode.OK)
                    {
                        using (var reader = new StreamReader(response.GetResponseStream()))
                        {
                            string str = reader.ReadToEnd();
                            return JsonConvert.DeserializeObject<T>(str, new JsonSerializerSettings()
                            {
                                ContractResolver = new CamelCasePropertyNamesContractResolver()
                            });
                        }
                    }
                    else if (response.StatusCode == HttpStatusCode.NoContent)
                    {
                        return default(T);
                    }
                    else
                    {
                        throw new HttpException((int)response.StatusCode, "远程服务器异常");
                    }
                }
            }
            catch
            {
                throw;
            }
        }

        protected T Request<T>(string apiRelativeUri, string method)
        {
            return Request<T>(apiRelativeUri, method, null);
        }

    }

    public class HttpMethod
    {
        public const string POST = "POST";
        public const string GET = "GET";
        public const string PUT = "PUT";
        public const string DELETE = "DELETE";
    }

    public class UrlHelper
    {
        private static string _combin(string url, NameValueCollection collection)
        {
            url = url.TrimEnd('&', '?');
            int idx = url.IndexOf("?");
            var array = (from key in collection.AllKeys
                         from value in collection.GetValues(key)
                         select string.Format("{0}={1}", HttpUtility.UrlEncode(key),
                         HttpUtility.UrlEncode(value))).ToArray();

            var query = string.Join("&", array);
            if (idx > 0)
                url = url + "&" + query;
            else
                url = url + "?" + query;

            return url;
        }

        public static Uri CombinQueryPath(Uri url, object obj)
        {
            NameValueCollection collection = new NameValueCollection();
            foreach (var prop in obj.GetType().GetProperties())
            {
                object val = prop.GetValue(obj, null);
                if (val != null)
                {
                    collection.Add(prop.Name, val.ToString());
                }
            }

            return new Uri(_combin(url.OriginalString, collection));
        }
    }
}
