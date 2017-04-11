using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cben.Authorization
{
    public class User
    {

        /// <summary>
        /// 用户Id
        /// </summary>
        public int UserId { get; set; }


        /// <summary>
        /// 身份标识
        /// </summary>
        public string OpenIDClaimedIdentifier { get; set; }

        /// <summary>
        /// 友好标识
        /// </summary>
        public string OpenIDFriendlyIdentifier { get; set; }

    }
}
