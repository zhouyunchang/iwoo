using Cben.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cben.Zero.OAuth2
{
    public class Client : Entity<int>
    {
        public const int MaxClientIdentifierLength = 50;

        public const int MaxClientSecretLength = 50;

        [Required]
        [StringLength(MaxClientIdentifierLength)]
        public virtual string ClientIdentifier { get; set; }

        [StringLength(MaxClientSecretLength)]
        public virtual string ClientSecret { get; set; }

        public virtual string Callback { get; set; }

        public virtual string Name { get; set; }

        public virtual int ClientType { get; set; }

        [ForeignKey("ClientId")]
        public virtual ICollection<ClientAuthorization> Authorizations { get; set; }



    }
}
