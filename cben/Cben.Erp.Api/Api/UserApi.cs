using Cben.Application.Users.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cben.Erp.Api
{

    /// <summary>
    /// 用户Api
    /// </summary>
    public class UserApi : ApiBase
    {

        /// <summary>
        /// 获取当前用户信息
        /// </summary>
        /// <returns></returns>
        public UserListDto GetUserInfo()
        {
            var result = Request<ApiResult<UserListDto>>(
                "/api/identity/GetUserInfo", HttpMethod.GET);
            return result.Result;
        }

    }
}
