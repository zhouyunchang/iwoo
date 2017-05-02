using Cben.Application.Services;
using Cben.Application.Services.Dto;
using Erp.Application.Product.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Erp.Application.Product
{
    public interface IProductBatchAppService : IApplicationService
    {

        Task AddProductBatch(AddProductBatchInput input);

        Task UpdateProductBatch(UpdateProductBatchInput input);

        Task RemoveProductBatch(int id);

        Task<ListResultDto<ProductBatchListDto>> GetAllProductBatch();

        Task<ProductBatchListDto> GetProductBatchById(int id);

    }
}
