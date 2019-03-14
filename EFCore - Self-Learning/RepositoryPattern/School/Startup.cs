using SoftUni.Core;
using SoftUni.Data;

namespace SoftUni
{
    public class Startup
    {
        public static void Main()
        {
            using (var unitOfWork = new UnitOfWork(new SoftUniDbContext()))
            {
                var teacher = unitOfWork.Teachers.Get(3);

                var teacherWithCourses = unitOfWork.Teachers.GetTeacherWithCourses(3);

                var cheapestCourses = unitOfWork.Courses.GetTopCheapestCourses(3);

                var mostExpensiveCourses = unitOfWork.Courses.GetTopExpensiveCourses(3);
            }
        }
    }
}