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
using Erp.Application.Employee.Dto;

namespace Erp.Application.Product
{
    public class WorkStatAppService : CbenAppServiceBase, IWorkStatAppService
    {

        private readonly IProcessRepository _processRepository;
        private readonly IProductBatchRepository _productBatchRepository;
        private readonly IProductRepository _productRepository;
        private readonly IEmployeeRepository _employeeRepository;

        public WorkStatAppService(
            IProcessRepository processRepository,
            IProductBatchRepository productBatchRepository,
            IProductRepository productRepository,
            IEmployeeRepository employeeRepository)
        {
            _processRepository = processRepository;
            _productBatchRepository = productBatchRepository;
            _productRepository = productRepository;
            _employeeRepository = employeeRepository;
        }


        public Task<ListResultDto<EmployeeWorkStatDto>> GetMonthStat(DateTime month)
        {
            DateTime beginTime = new DateTime(month.Year, month.Month, 1);
            DateTime endTime = beginTime.AddMonths(1);

            var productGroup = _productRepository.GetAll()
                .Where(i => i.ProductBatch.CreationTime >= beginTime && i.CreationTime < endTime)
                .GroupBy(i => i.ProductBatch).ToList();
            List<EmployeeWorkStatDto> lst = new List<EmployeeWorkStatDto>();
            foreach (var grp in productGroup)
            {
                IDictionary<long, List<ProcessCountDto>> dict = new Dictionary<long, List<ProcessCountDto>>();
                foreach (var prod in grp)
                {
                    foreach (var pr in prod.ProcessRecords)
                    {
                        foreach (var e in pr.PersonInCharge)
                        {
                            if (!dict.ContainsKey(e.EmployeeId))
                            {
                                dict.Add(e.EmployeeId, new List<ProcessCountDto>());
                            }

                            var process = dict[e.EmployeeId].FirstOrDefault(i => i.ProcessId == pr.ProcessId);
                            if (process == null)
                            {
                                dict[e.EmployeeId].Add(new ProcessCountDto
                                {
                                    ProcessId = pr.ProcessId,
                                    Count = e.Times
                                });
                            }
                            else
                            {
                                process.Count += e.Times;
                            }
                        }
                    }
                }

                var batchDto = grp.Key.MapTo<ProductBatchListDto>();
                foreach (var item in dict)
                {
                    var employee = _employeeRepository.Get(item.Key).MapTo<EmployeeListDto>();
                    lst.Add(new EmployeeWorkStatDto
                    {
                        Employee = employee,
                        ProductBatch = batchDto,
                        Process = item.Value
                    });
                }
            }

            return Task.FromResult(new ListResultDto<EmployeeWorkStatDto>(lst));
        }
    }
}
