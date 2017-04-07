using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CocoBen.Authorization
{
    public class User
    {

        public int UserId { get; set; }

        public string OpenIDClaimedIdentifier { get; set; }

        public string OpenIDFriendlyIdentifier { get; set; }

    }
}
