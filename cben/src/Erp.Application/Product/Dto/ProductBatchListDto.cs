using Cben.Application.Services.Dto;
using Cben.Application.Users.Dto;
using Cben.AutoMapper;
using Erp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Erp.Application.Product.Dto
{

    /// <summary>
    /// 产品批次Dto
    /// </summary>
    [AutoMapFrom(typeof(ProductBatch))]
    public class ProductBatchListDto : EntityDto<int>
    {
        /// <summary>
        /// 批号
        /// </summary>
        public string BatchNo { get; set; }

        /// <summary>
        /// 规格/容积
        /// </summary>
        public string Spec { get; set; }

        /// <summary>
        /// 工艺号
        /// </summary>
        public string TechNo { get; set; }

        /// <summary>
        /// 直径
        /// </summary>
        public double Diameter { get; set; }

        /// <summary>
        /// 压力
        /// </summary>
        public double Pressure { get; set; }

        public UserListDto CreatorUser { get; set; }

        public DateTime CreationTime { get; set; }


    }
}
