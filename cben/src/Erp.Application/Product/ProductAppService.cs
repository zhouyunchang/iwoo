using Cben.Application;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cben.Application.Services.Dto;
using Erp.Application.Product.Dto;
using Erp.Repositories;
using Cben.AutoMapper;
using Cben.Domain.Uow;

namespace Erp.Application.Product
{
    public class ProductAppService : CbenAppServiceBase, IProductAppService
    {

        private readonly IProductRepository _productRepository;
        private readonly IEmployeeRepository _employeeRepository;

        public ProductAppService(
            IProductRepository productRepository,
            IEmployeeRepository employeeRepository)
        {
            _productRepository = productRepository;
            _employeeRepository = employeeRepository;
        }


        /// <summary>
        /// 添加产品
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [UnitOfWork]
        public async Task AddProduct(AddProductInput input)
        {

            var prod = new Models.Product
            {
                ProductBatchId = input.ProductBatchId
            };

            if (input.ProcessRecords != null)
            {
                foreach (var record in input.ProcessRecords)
                {
                    List<Models.Employee> employee = null;
                    if (record.PersonInCharge != null && record.PersonInCharge.Count > 0)
                    {
                        employee = await _employeeRepository.GetAllListAsync(e => record.PersonInCharge.Contains(e.Id));
                    }
                    else
                    {
                        employee = new List<Models.Employee>();
                    }

                    prod.ProcessRecords.Add(new Models.ProcessRecord()
                    {
                        ProcessId = record.ProcessId,
                        PersonInCharge = employee
                    });
                }
            }

            await _productRepository.InsertAsync(prod);
        }

        /// <summary>
        /// 获取指定批次下的所有产品
        /// </summary>
        /// <param name="batchId"></param>
        /// <returns></returns>
        public async Task<ListResultDto<ProductListDto>> GetAllProduct(int batchId)
        {
            var lst = await _productRepository.GetAllListAsync(e => e.ProductBatchId == batchId);

            return new ListResultDto<ProductListDto>(
                lst.MapTo<List<ProductListDto>>()
                );
        }

        /// <summary>
        /// 获取指定产品
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<ProductListDto> GetProduct(long id)
        {
            var prod = await _productRepository.GetAsync(id);
            return prod.MapTo<ProductListDto>();
        }


        /// <summary>
        /// 删除产品
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task RemoveProduct(long id)
        {
            var prod = await _productRepository.GetAsync(id);
            prod.IsDeleted = true;

            await UnitOfWorkManager.Current.SaveChangesAsync();
        }


        /// <summary>
        /// 更新产品
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [UnitOfWork]
        public async Task UpdateProduct(UpdateProductInput input)
        {
            var prod = await _productRepository.GetAsync(input.Id);

            prod.ProductBatchId = input.ProductBatchId;

            // 更新工序负责人
            var updateProcesses = input.ProcessRecords.Select(i => i.ProcessId).ToList();
            var delProcessRecord = prod.ProcessRecords.Where(i => !updateProcesses.Contains(i.ProcessId)).ToList();
            foreach (var item in delProcessRecord)
            {
                await _productRepository.DeleteProcessRecordAsync(item);
            }
            var nowProcessId = prod.ProcessRecords.Select(i => i.ProcessId).ToList();
            var newProcess = input.ProcessRecords.Where(i => !nowProcessId.Contains(i.ProcessId)).ToList();
            foreach (var item in newProcess)
            {
                List<Models.Employee> employee = null;
                if (item.PersonInCharge != null && item.PersonInCharge.Count > 0)
                {
                    employee = await _employeeRepository.GetAllListAsync(e => item.PersonInCharge.Contains(e.Id));
                }
                else
                {
                    employee = new List<Models.Employee>();
                }

                prod.ProcessRecords.Add(new Models.ProcessRecord()
                {
                    ProcessId = item.ProcessId,
                    PersonInCharge = employee
                });
            }

            foreach (var pid in updateProcesses.Intersect(nowProcessId))
            {
                var record = prod.ProcessRecords.Where(i => i.ProcessId == pid).First();
                var item = input.ProcessRecords.Where(i => i.ProcessId == pid).First();
                record.PersonInCharge.Clear();
                List<Models.Employee> employee = null;
                if (item.PersonInCharge != null && item.PersonInCharge.Count > 0)
                {
                    employee = await _employeeRepository.GetAllListAsync(e => item.PersonInCharge.Contains(e.Id));
                    record.PersonInCharge.AddRange(employee);
                }
            }

            await UnitOfWorkManager.Current.SaveChangesAsync();
        }
    }
}
