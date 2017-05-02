using Cben.AutoMapper;
using System.Collections.Generic;

namespace Erp.Application.Process
{

    [AutoMapFrom(typeof(Models.ProcessCategory))]
    public class ProcessCategoryListDto
    {

        public string Name { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        public int OrderNum { get; set; }

    }
}