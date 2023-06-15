using StudentsDataAPI.Model;
using StudentsDataAPI.Model.Dto;

namespace StudentsDataAPI.Repository.Interfaces
{
    public interface IStudentService
    {
        Task<IEnumerable<Student>> GetAllStudents();
        Task<Student> GetStudentById(int id);
        Task <Student> CreateStudent (AddStudentDto student);
        Task <bool> DeleteStudent (int id);
        Task <Student> UpdateStudent(int id,AddStudentDto student);
        Task<IEnumerable<Student>> SearchStudentByName(string name);
        Task<IEnumerable<Student>> SearchStudentByCourse(string course);
        Task<IEnumerable<Student>> GetPaginatedStudents(int pageNumber, int pageSize);
        Task<int> GetTotalStudentCount();


    }
}
