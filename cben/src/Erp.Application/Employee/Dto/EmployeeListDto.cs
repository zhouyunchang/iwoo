using Cben.Application.Services.Dto;
using Cben.Application.Users.Dto;
using Cben.AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Erp.Application.Employee.Dto
{
    [AutoMapFrom(typeof(Erp.Models.Employee))]
    public class EmployeeListDto : EntityDto<long>
    {

        public string SerialNumber { get; set; }

        public UserListDto User { get; set; }
        
        public UserListDto CreatorUser { get; set; }

        public DateTime CreationTime { get; set; }

        public override string ToString()
        {
            return string.Format("{0}({1})", SerialNumber, User?.Name);
        }

    }
}
