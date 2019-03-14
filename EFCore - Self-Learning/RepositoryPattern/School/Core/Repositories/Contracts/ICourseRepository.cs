using SoftUni.Models;
using System.Collections.Generic;

namespace SoftUni.Core.Repositories.Contracts
{
    public interface ICourseRepository : IRepository<Course>
    {
        IEnumerable<Course> GetTopExpensiveCourses(int count);

        IEnumerable<Course> GetTopCheapestCourses(int count);

        IEnumerable<Course> GetCoursesWithTeachers(int pageIndex, int pageSize); 

        //IEnumerable<Tag> GetAllTagsForCourse(); 
    }
}
