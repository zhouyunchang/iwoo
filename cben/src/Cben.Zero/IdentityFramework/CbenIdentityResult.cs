using System.Collections.Generic;
using Microsoft.AspNet.Identity;

namespace Cben.IdentityFramework
{
    public class CbenIdentityResult : IdentityResult
    {
        public CbenIdentityResult()
        {
            
        }

        public CbenIdentityResult(IEnumerable<string> errors)
            : base(errors)
        {
            
        }

        public CbenIdentityResult(params string[] errors)
            :base(errors)
        {
            
        }

        public static CbenIdentityResult Failed(params string[] errors)
        {
            return new CbenIdentityResult(errors);
        }
    }
}