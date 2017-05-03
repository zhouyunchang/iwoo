using Cben.Application;
using Cben.Application.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cben.Application.Services.Dto;
using Erp.Repositories;
using Cben.AutoMapper;
using Cben.Domain.Uow;
using Cben;

namespace Erp.Application.Process
{
    public class ProcessAppService : CbenAppServiceBase, IProcessAppService
    {
        private readonly IProcessRepository _processRepository;
        private readonly IProcessCategoryRepository _processCategoryRepository;

        public ProcessAppService(
            IProcessRepository processRepository,
            IProcessCategoryRepository processCategoryRepository)
        {
            _processRepository = processRepository;
            _processCategoryRepository = processCategoryRepository;
        }


        public async Task AddProcess(AddProcessInput input)
        {
            await _processRepository.InsertAsync(new Models.Process
            {
                CategoryId = input.CategoryId,
                Name = input.Name,
                GuidePrice = input.GuidePrice
            });
        }

        public async Task AddProcessCategory(AddProcessCategoryInput input)
        {
            await _processCategoryRepository.InsertAsync(new Models.ProcessCategory
            {
                Name = input.Name
            });
        }

        public async Task<ListResultDto<ProcessListDto>> GetAllProcess()
        {
            List<Models.Process> process = await _processRepository.GetAllListAsync();
            return new ListResultDto<ProcessListDto>(
                process.MapTo<List<ProcessListDto>>()
                );
        }

        public async Task<ListResultDto<ProcessCategoryListDto>> GetAllProcessCategory()
        {
            List<Models.ProcessCategory> category = await _processCategoryRepository.GetAllListAsync();
            return new ListResultDto<ProcessCategoryListDto>(
                category.MapTo<List<ProcessCategoryListDto>>()
                );
        }

        public async Task<ProcessListDto> GetProcessById(int id)
        {
            var process = await _processRepository.GetAsync(id);
            return process.MapTo<ProcessListDto>();
        }

        public async Task<ProcessCategoryListDto> GetProcessCategoryById(int id)
        {
            var category = await _processCategoryRepository.GetAsync(id);
            return category.MapTo<ProcessCategoryListDto>();
        }

        [UnitOfWork(true)]
        public async Task RemoveProcess(int processId)
        {
            var process = await _processRepository.GetAsync(processId);
            process.IsDeleted = true;
        }

        [UnitOfWork(true)]
        public async Task RemoveProcessCategory(int categoryId)
        {
            var category = await _processCategoryRepository.GetAsync(categoryId);
            if (category.Processes != null && category.Processes.Count > 0)
            {
                throw new CbenException("无法删除存在工序的分组");
            }
            category.IsDeleted = true;
        }

        [UnitOfWork(true)]
        public async Task UpdateProcess(UpdateProcessInput input)
        {
            var process = await _processRepository.GetAsync(input.Id);
            if (process == null)
            {
                throw new CbenException("工序不存在");
            }
            process.Name = input.Name;
            process.CategoryId = input.CategoryId;
            process.GuidePrice = input.GuidePrice;

            await UnitOfWorkManager.Current.SaveChangesAsync();
        }

        [UnitOfWork(true)]
        public async Task UpdateProcessCategory(UpdateProcessCategoryInput input)
        {
            var category = await _processCategoryRepository.GetAsync(input.Id);
            if (category == null)
            {
                throw new CbenException("分组不存在");
            }
            category.Name = input.Name;

            await UnitOfWorkManager.Current.SaveChangesAsync();
        }
    }
}
