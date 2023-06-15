using AutoMapper;
using Microsoft.EntityFrameworkCore;
using StudentsDataAPI.Data;
using StudentsDataAPI.Model;
using StudentsDataAPI.Model.Dto;
using StudentsDataAPI.Repository.Interfaces;

namespace StudentsDataAPI.Repository.Services
{
    public class StudentService : IStudentService
    {
        private readonly StudentContext context;
        private readonly IMapper mapper;
       
        public StudentService(StudentContext context,IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
            
        }

        public async Task<Student> CreateStudent(AddStudentDto studentDto)
        {
            if(studentDto == null)
            {
                throw new ArgumentNullException("Please check the input values");
            }

            var student = mapper.Map<Student>(studentDto);
            student.Age = CalculateAge(studentDto.DOB);
            await context.Students.AddAsync(student);
            await context.SaveChangesAsync();
            return student;
        }

        public async Task<bool> DeleteStudent(int id)
        {
            var student = await context.Students.FirstOrDefaultAsync(s=>s.Id==id) ?? throw new NullReferenceException("Student not found");
            context.Students.Remove(student);
            await context.SaveChangesAsync();
            return true;

        }

        public async Task<IEnumerable<Student>> GetAllStudents()
        {
            var students = await context.Students.ToListAsync();
            if (students == null)
            {
                throw new NullReferenceException("No students found ");
            }
            return students;
        }

        public async Task<IEnumerable<Student>> GetPaginatedStudents(int pageNumber, int pageSize)
        {
            var skipCount = (pageNumber - 1) * pageSize;
            var students = await context.Students.Skip(skipCount).Take(pageSize).ToListAsync();
            return students;
        }
        

        public async Task<Student> GetStudentById(int id)
        {
            var student = await context.Students.FirstOrDefaultAsync(s=>s.Id==id);
            if(student == null)
            {
                throw new NullReferenceException("Student Not Found");
            }
            return student;

        }

        public async Task<int> GetTotalStudentCount()
        {
            var totalCount = await context.Students.CountAsync();
            return totalCount;
        }

        public async Task<IEnumerable<Student>> SearchStudentByCourse(string course)
        {
            var students = await context.Students.Where(x=>x.Course.Contains(course)).ToListAsync();
            if(students == null)
            {
                throw new KeyNotFoundException("No students found");
            }
            return students;
        }

        public async Task<IEnumerable<Student>> SearchStudentByName(string name)
        {
            var students = await context.Students.Where(x=>x.Name.Contains(name)).ToListAsync();
            if(students == null)
            {
                throw new FileNotFoundException("No Student Found");
            }
            return students;
        }

        public async Task<Student> UpdateStudent(int id,AddStudentDto studentdto)
        {
            var stud = await context.Students.FirstOrDefaultAsync(s => s.Id == id);
            if(stud == null)
            {
                throw new NullReferenceException("Student not available");
            }
            stud.Name = studentdto.Name;
            stud.Course = studentdto.Course;
            stud.DOB = studentdto.DOB;
            stud.Age = CalculateAge(studentdto.DOB);

            await context.SaveChangesAsync();
            return stud;
        }

        

        private int CalculateAge(DateTime dob)
        {
            DateTime currentDate  = DateTime.Today;
            int age = currentDate.Year - dob.Year;
            if (dob.Date > currentDate.AddYears(-age))
            {
                age--;
            }
            return age;
        }
    }
}
