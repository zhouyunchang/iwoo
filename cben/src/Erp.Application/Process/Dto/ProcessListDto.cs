using Cben.Application.Services.Dto;
using Cben.Application.Users.Dto;
using Cben.AutoMapper;
using System;

namespace Erp.Application.Process
{

    [AutoMapFrom(typeof(Erp.Models.Process))]
    public class ProcessListDto : EntityDto<int>
    {

        public string Name { get; set; }

        public ProcessCategoryListDto Category { get; set; }

        public UserListDto CreatorUser { get; set; }

        public DateTime CreationTime { get; set; }

    }
}