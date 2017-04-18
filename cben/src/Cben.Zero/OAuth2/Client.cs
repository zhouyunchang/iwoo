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
    public class Client
    {
        public const int MaxClientIdentifierLength = 50;

        public const int MaxClientSecretLength = 50;

        [Key]
        public int ClientId { get; set; }

        [Required]
        [StringLength(MaxClientIdentifierLength)]
        public string ClientIdentifier { get; set; }

        [StringLength(MaxClientSecretLength)]
        public string ClientSecret { get; set; }

        public string Callback { get; set; }

        public string Name { get; set; }

        public int ClientType { get; set; }

        [ForeignKey("ClientId")]
        public virtual ICollection<ClientAuthorization> Authorizations { get; set; }

    }
}
