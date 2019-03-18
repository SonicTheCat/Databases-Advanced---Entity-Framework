namespace NonStopMarket.App.Core.Contracts
{
    using System;

    using Dtos.ManagerDtos;
    using Dtos.EmployeeDtos;
    using NonStopMarket.Models;

    public interface IEmployeeRepository : IRepository<Employee>
    {
        void AddEmployee(EmployeeDto employee);

        void SetBirthday(int id, DateTime date);

        void SetAddress(int id, string address);

        EmployeeDto EmployeeInfo(int id); 

        EmployeePersonalInfoDto EmployeePersonalInfo(int id);

        void SetManager(int employeeId, int managerId);

        ManagerDto ManagerInfo(int id); 
    }
}