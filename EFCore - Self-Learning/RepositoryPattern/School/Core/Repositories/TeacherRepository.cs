using Microsoft.EntityFrameworkCore;

using SoftUni.Data;
using SoftUni.Core.Repositories.Contracts;
using SoftUni.Models;

using System.Linq;

namespace SoftUni.Core.Repositories
{
    public class TeacherRepository : Repository<Teacher>, ITeacherRepository
    {
        public TeacherRepository(SoftUniDbContext context)
            : base(context)
        {
        }

        public SoftUniDbContext SoftUniContext => this.context as SoftUniDbContext;

        public Teacher GetTeacherWithCourses(int id)
        {
            return this.SoftUniContext.Teachers.Include(x => x.Courses).SingleOrDefault(x => x.Id == id);
        }
    }
}