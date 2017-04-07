using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CocoBen.Authorization
{
    public class ClientAuthorization
    {

        public int AuthorizationId { get; set; }

        public DateTime CreatedOnUtc { get; set; }

        public int ClientId { get; set; }

        public int UserId { get; set; }

        public string Scope { get; set; }

        public DateTime? ExpirationDateUtc { get; set; }


    }
}
