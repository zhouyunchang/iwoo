using Cben.Application;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cben.Application.Services.Dto;
using Erp.Application.Product.Dto;
using Erp.Repositories;
using Erp.Models;
using Cben.AutoMapper;
using Cben;

namespace Erp.Application.Product
{
    public class ProductBatchAppService : CbenAppServiceBase, IProductBatchAppService
    {

        private readonly IProductBatchRepository _prodBatchRepository;

        public ProductBatchAppService(
            IProductBatchRepository prodBatchRepository)
        {
            _prodBatchRepository = prodBatchRepository;
        }


        public async Task AddProductBatch(AddProductBatchInput input)
        {
            ProductBatch entity = new ProductBatch
            {
                BatchNo = input.BatchNo,
                Diameter = input.Diameter,
                Pressure = input.Pressure,
                Spec = input.Spec,
                TechNo = input.TechNo
            };

            await _prodBatchRepository.InsertAsync(entity);
        }

        public async Task<ListResultDto<ProductBatchListDto>> GetAllProductBatch()
        {
            var lst = await _prodBatchRepository.GetAllListAsync();
            return new ListResultDto<ProductBatchListDto>(
                lst.MapTo<List<ProductBatchListDto>>()
                );
        }

        public async Task<ProductBatchListDto> GetProductBatchById(int id)
        {
            var entity = await _prodBatchRepository.GetAsync(id);
            return entity.MapTo<ProductBatchListDto>();
        }

        public async Task RemoveProductBatch(int id)
        {
            var entity = await _prodBatchRepository.GetAsync(id);
            entity.IsDeleted = true;
        }

        public async Task UpdateProductBatch(UpdateProductBatchInput input)
        {
            var entity = await _prodBatchRepository.GetAsync(input.Id);
            if (entity == null)
            {
                throw new CbenException("批次不存在");
            }
            entity.BatchNo = input.BatchNo;
            entity.Diameter = input.Diameter;
            entity.Pressure = input.Pressure;
            entity.Spec = input.Spec;
            entity.TechNo = input.TechNo;

            await UnitOfWorkManager.Current.SaveChangesAsync();
        }
    }
}
