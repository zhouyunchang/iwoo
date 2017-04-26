using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Cben.Erp.Api
{
    public interface IAuth
    {
        void Authorize(HttpWebRequest request);

        void Authorization(params string[] scopes);
    }
}
