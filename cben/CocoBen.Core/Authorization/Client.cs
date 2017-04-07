using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CocoBen.Authorization
{
    public class Client
    {

        public int ClientId { get; set; }

        public string ClientIdentifier { get; set; }

        public string ClientSecret { get; set; }

        public string Callback { get; set; }

        public string Name { get; set; }

        public int ClientType { get; set; }


    }
}
