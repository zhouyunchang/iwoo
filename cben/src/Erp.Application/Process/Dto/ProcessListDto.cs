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

        /// <summary>
        /// 指导价格
        /// </summary>
        public decimal GuidePrice { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        public int OrderNum { get; set; }

        public ProcessCategoryListDto Category { get; set; }

        public UserListDto CreatorUser { get; set; }

        public DateTime CreationTime { get; set; }

    }
}