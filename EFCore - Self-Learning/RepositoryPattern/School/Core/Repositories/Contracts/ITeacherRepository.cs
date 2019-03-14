using SoftUni.Models;

namespace SoftUni.Core.Repositories.Contracts
{
    public interface ITeacherRepository : IRepository<Teacher>
    {
        Teacher GetTeacherWithCourses(int id); 
    }
}