using System;

using SoftUni.Core.Repositories;
using SoftUni.Core.Repositories.Contracts;
using SoftUni.Data;

namespace SoftUni.Core
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly SoftUniDbContext context;

        public UnitOfWork(SoftUniDbContext context)
        {
            this.context = context;
            this.Teachers = new TeacherRepository(this.context);
            this.Courses = new CourseRepository(this.context);
        }

        public ITeacherRepository Teachers { get; private set; }

        public ICourseRepository Courses { get; private set; }

        public int Complete()
        {
            return this.context.SaveChanges();
        }

        public void Dispose()
        {
            this.context.Dispose();
        }
    }
}
