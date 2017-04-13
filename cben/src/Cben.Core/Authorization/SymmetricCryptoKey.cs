using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cben.Authorization
{
    public class SymmetricCryptoKey
    {

        public string Bucket { get; set; }

        public string Handle { get; set; }

        public DateTime ExpiresUtc { get; set; }

        public byte[] Secret { get; set; }

    }
}
