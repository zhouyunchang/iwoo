using DotNetOpenAuth.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cben.Erp.Exceptions
{
    public class NeedAuthorizationException : Exception
    {

        public NeedAuthorizationException(string message, OutgoingWebResponse response)
            : base(message)
        {
            OAuthResponse = response;
        }

        public OutgoingWebResponse OAuthResponse
        {
            get;
            set;
        }

    }
}
