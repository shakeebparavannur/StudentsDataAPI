using Microsoft.AspNetCore.Mvc;
using Moq;
using StudentsDataAPI.Controllers;
using StudentsDataAPI.Data;
using StudentsDataAPI.Model;
using StudentsDataAPI.Repository.Interfaces;
using StudentsDataAPI.Repository.Services;

namespace StudentDataAPITest
{
   
    
    public class StudentDataControllerTest
    {

        private readonly Mock<IStudentService> studentservice;

        public StudentDataControllerTest()
        {
            studentservice = new Mock<IStudentService>();
        }
        [Fact]
        public async void GetAll_StudentSuccess()
        {
            var studentList = StudentsData();
            studentservice.Setup(x => x.GetAllStudents())
                .ReturnsAsync(studentList);
            var studentController = new StudentController(studentservice.Object);
            
            var studentResult = await studentController.GetStudents();
            var resultType = studentResult as OkObjectResult;
            //var resultList = resultType.Value as List<Student>;
            var apiResponse =(APIResponse)resultType.Value;
             var resultList = apiResponse.Result as List<Student>;

            //assert
            Assert.NotNull(studentResult);
            Assert.IsType<OkObjectResult>(studentResult);
            Assert.IsType<APIResponse>(resultType.Value);
            Assert.Equal(2, resultList.Count);
        }
        [Fact]
        public async void GetStudentById_Student()
        {
            var studentList = StudentsData();
            studentservice.Setup(x => x.GetStudentById(2))
                .ReturnsAsync(studentList[1]);
            var studentController = new StudentController(studentservice.Object);

            var result =await studentController.GetStudentById(2);
            var resultType = result as OkObjectResult;
            var apiResponse = (APIResponse)resultType.Value;
            var resultList = apiResponse.Result as Student;

            Assert.NotNull(result);

            Assert.Equal(studentList[1].Id, resultList.Id);
            Assert.True(studentList[1].Name == resultList.Name);
        }

        private List<Student> StudentsData()
        {
            List<Student> studentList = new List<Student>()
            {
                new Student
                {
                    Id = 1,
                    Name = "Test",
                    Age = 18,
                    DOB = DateTime.Now,
                    Course = "science"
                },
                new Student
                {
                     Id = 2,
                    Name = "Test2",
                    Age = 19,
                    DOB = DateTime.Now,
                    Course = "commerce"
                }
            };
            return studentList;
        }
    }
}