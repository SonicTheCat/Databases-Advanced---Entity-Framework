using SoftUni.Core.Repositories.Contracts;

using System;

namespace SoftUni.Core
{
    public interface IUnitOfWork : IDisposable
    {
        ITeacherRepository Teachers { get; }
        ICourseRepository Courses { get; }
        int Complete(); 
    }
}