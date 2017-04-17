using System.ComponentModel.DataAnnotations;
using Cben.Auditing;
using Cben.Authorization.Users;
using Cben.AutoMapper;
using Cben.Core.Users;

namespace Cben.Application.Users.Dto
{
    [AutoMap(typeof(User))]
    public class CreateUserInput
    {
        [Required]
        [StringLength(CbenUserBase.MaxUserNameLength)]
        public string UserName { get; set; }

        [Required]
        [StringLength(User.MaxNameLength)]
        public string Name { get; set; }

        [Required]
        [StringLength(User.MaxSurnameLength)]
        public string Surname { get; set; }

        [Required]
        [EmailAddress]
        [StringLength(CbenUserBase.MaxEmailAddressLength)]
        public string EmailAddress { get; set; }

        [Required]
        [StringLength(User.MaxPlainPasswordLength)]
        [DisableAuditing]
        public string Password { get; set; }

        public bool IsActive { get; set; }
    }
}