using Cben;
using Cben.Application;
using Cben.Application.Services.Dto;
using Cben.AutoMapper;
using Cben.Domain.Uow;
using Erp.Application.Employee.Dto;
using Erp.Repositories;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Erp.Application.Employee
{
    public class EmployeeAppService : CbenAppServiceBase, IEmployeeAppService
    {
        private readonly IEmployeeRepository _employeeRepository;

        public EmployeeAppService(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }


        [UnitOfWork(true)]
        public async Task AddEmployee(AddEmployeeInput input)
        {
            var currentUser = await GetCurrentUserAsync();

            var user = new Cben.Core.Users.User
            {
                UserName = input.UserName,
                Name = input.Name,
                Surname = input.Name,
                PhoneNumber = input.Phone,
                EmailAddress = input.UserName,
                Password = new PasswordHasher().HashPassword(Cben.Core.Users.User.DefaultPassword)
            };
            await CreateUser(user);

            user = UserManager.FindByName(input.UserName);
            var employee = new Models.Employee
            {
                User = user,
                CreatorUserId = currentUser?.Id,
                SerialNumber = input.SerialNumber
            };

            await _employeeRepository.InsertAsync(employee);
        }

        [UnitOfWork(true)]
        public async Task UpdateEmployee(UpdateEmployeeInput input)
        {
            var emp = _employeeRepository.Get(input.Id);
            emp.SerialNumber = input.SerialNumber;

            await UnitOfWorkManager.Current.SaveChangesAsync();

            var user = emp.User;
            user.Name = input.Name;
            user.Surname = input.Name;
            user.PhoneNumber = input.Phone;
            user.UserName = input.UserName;

            await UnitOfWorkManager.Current.SaveChangesAsync();
        }

        private async Task CreateUser(Cben.Core.Users.User user)
        {
            IdentityResult result = await UserManager.CreateAsync(user);
            UnitOfWorkManager.Current.SaveChanges();

            if (result == null)
                throw new CbenException("创建用户失败");

            if (!result.Succeeded)
            {
                if (result.Errors != null)
                {
                    throw new CbenException(string.Join("\r\n", result.Errors));
                }
            }

        }

        [UnitOfWork(true)]
        public async Task RemoveEmployee(long employeeId)
        {
            var emp = await _employeeRepository.GetAsync(employeeId);
            emp.IsDeleted = true;
            emp.User.IsDeleted = true;
        }


        public async Task<ListResultDto<EmployeeListDto>> GetAllEmployee()
        {
            var employee = await _employeeRepository.GetAllListAsync();

            return new ListResultDto<EmployeeListDto>(
                employee.MapTo<List<EmployeeListDto>>()
                );
        }

        public async Task<EmployeeListDto> GetById(long id)
        {
            var emp = await _employeeRepository.GetAsync(id);

            return emp.MapTo<EmployeeListDto>();
        }
    }
}
