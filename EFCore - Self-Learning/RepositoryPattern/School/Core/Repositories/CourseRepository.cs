using Microsoft.EntityFrameworkCore;
using SoftUni.Data;
using SoftUni.Core.Repositories.Contracts;
using SoftUni.Models;

using System.Collections.Generic;
using System.Linq;

namespace SoftUni.Core.Repositories
{
    public class CourseRepository : Repository<Course>, ICourseRepository
    {
        public CourseRepository(SoftUniDbContext context) 
            : base(context)
        {
        }

        public SoftUniDbContext SoftUniContext => this.context as SoftUniDbContext;

        public IEnumerable<Course> GetTopCheapestCourses(int count)
        {
            return this.SoftUniContext.Courses.OrderBy(x => x.FullPrice).Take(count).ToList();
        }

        public IEnumerable<Course> GetTopExpensiveCourses(int count)
        {
            return this.SoftUniContext.Courses.OrderByDescending(x => x.FullPrice).Take(count).ToList();
        }

        public IEnumerable<Course> GetCoursesWithTeachers(int pageIndex, int pageSize)
        {
            return this.SoftUniContext.Courses
               .Include(c => c.Teacher)
               .OrderBy(c => c.Name)
               .Skip((pageIndex - 1) * pageSize)
               .Take(pageSize)
               .ToList();
        }
    }
}
