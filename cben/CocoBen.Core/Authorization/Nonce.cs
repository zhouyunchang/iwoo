using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CocoBen.Authorization
{
    public class Nonce
    {

        public string Context { get; set; }

        public string Code { get; set; }

        public DateTime Timestamp { get; set; }

    }
}
