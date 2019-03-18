namespace NonStopMarket.App.Core.Repositories
{
    using System;
    using System.Linq;
    using AutoMapper;
  
    using Contracts;
    using Microsoft.EntityFrameworkCore;
    using Dtos.EmployeeDtos;
    using NonStopMarket.Data;
    using NonStopMarket.Models;
    using Dtos.ManagerDtos;

    public class EmployeeRepository : Repository<Employee>, IEmployeeRepository
    {
        public EmployeeRepository(NonStopMarketContext context, IMapper mapper)
            : base(context, mapper)
        {
        }

        public NonStopMarketContext NonStopMarketContext => this.context as NonStopMarketContext;

        public void AddEmployee(EmployeeDto employee)
        {
            var empFromDto = this.mapper.Map<Employee>(employee);
            base.Add(empFromDto);
        }

        public EmployeeDto EmployeeInfo(int id)
        {
            var employee = base.Get(id);
            ValidateEmployeeExist(id, employee);

            var dto = this.mapper.Map<EmployeeDto>(employee);

            return dto;
        }

        public void SetAddress(int id, string address)
        {
            var employee = base.Get(id);
            ValidateEmployeeExist(id, employee);

            employee.Address = address;
        }

        public void SetBirthday(int id, DateTime date)
        {
            var employee = base.Get(id);
            ValidateEmployeeExist(id, employee);

            employee.Birthday = date; 
        }

        public EmployeePersonalInfoDto EmployeePersonalInfo(int id)
        {
            var employee = base.Get(id);
            ValidateEmployeeExist(id, employee);

            var employeeInfo = this.mapper.Map<EmployeePersonalInfoDto>(employee);
            return employeeInfo;
        }

        public void SetManager(int employeeId, int managerId)
        {
            var employee = base.Get(employeeId);
            var manager = base.Get(managerId);

            ValidateEmployeeExist(employeeId, employee);
            ValidateEmployeeExist(managerId, manager);

            employee.ManagerId = managerId;  
        }

        public ManagerDto ManagerInfo(int id)
        {
            var employee = NonStopMarketContext
                .Employees
                .Include(e => e.ManagerEmployees)
                .Where(e => e.EmployeeId == id)
                .SingleOrDefault();


            var manager = this.mapper.Map<ManagerDto>(employee); 

            //ValidateEmployeeExist(id, manager); 

            return manager; 
        }

        private static void ValidateEmployeeExist(int id, Employee employee)
        {
            if (employee == null)
            {
                throw new ArgumentException($"User with {id} does not exist!");
            }
        }
    }
}