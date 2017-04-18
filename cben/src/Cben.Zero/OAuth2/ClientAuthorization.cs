using Cben.Authorization.Users;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cben.Zero.OAuth2
{
    public class ClientAuthorization
    {

        [Key]
        public virtual int AuthorizationId { get; set; }

        [Required]
        public virtual DateTime CreatedOnUtc { get; set; }

        [Required]
        public virtual int ClientId { get; set; }

        [Required]
        public virtual int UserId { get; set; }

        public virtual string Scope { get; set; }

        public virtual DateTime? ExpirationDateUtc { get; set; }

        public virtual Client Client { get; set; }
        

    }
}
