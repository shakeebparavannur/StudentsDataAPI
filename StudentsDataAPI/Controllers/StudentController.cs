using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StudentsDataAPI.Model;
using StudentsDataAPI.Model.Dto;
using StudentsDataAPI.Repository.Interfaces;
using System.Net;
using System.Net.WebSockets;

namespace StudentsDataAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly IStudentService studentService;
        protected APIResponse response;

        public StudentController(IStudentService studentService)
        {
            this.studentService = studentService;
            response = new();
        }
        // Get all Student details
        [HttpGet("Stduents")]
        public async Task<IActionResult> GetStudents()
        {
            try
            {
                var students = await studentService.GetAllStudents();
                if (students == null)
                {
                    response.StatusCode = HttpStatusCode.BadRequest;
                    response.ErrorMessages.Add("somthing went wrong");
                    response.IsSuccess = false;
                    return BadRequest(response);
                }
                response.IsSuccess = true;
                response.StatusCode = HttpStatusCode.OK;
                response.Result = students;
                return Ok(response);
            }
            catch (NullReferenceException ex)
            {
                response.StatusCode = HttpStatusCode.BadRequest;
                response.ErrorMessages.Add(ex.Message);
                response.IsSuccess = false;
                return BadRequest(response);
            }


        }
        //Add new student
        [HttpPost("Addstudent")]
        public async Task<IActionResult> AddStudent(AddStudentDto student)
        {
            try
            {
                var AddStud = await studentService.CreateStudent(student);
                if (AddStud == null)
                {
                    response.StatusCode = HttpStatusCode.BadRequest;
                    response.ErrorMessages.Add("somthing went wrong");
                    response.IsSuccess = false;
                    return BadRequest(response);
                }
                response.IsSuccess = true;
                response.StatusCode = HttpStatusCode.OK;
                response.Result = AddStud;
                return Ok(response);
            }
            catch (NullReferenceException ex)
            {
                response.StatusCode = HttpStatusCode.BadRequest;
                response.ErrorMessages.Add(ex.Message);
                response.IsSuccess = false;
                return BadRequest(response);
            }
        }
        // get student by id
        [HttpGet("{id}")]
        public async Task <IActionResult> GetStudentById(int id)
        {
            try
            {


                var student = await studentService.GetStudentById(id);
                if (student == null)
                {
                    response.StatusCode = HttpStatusCode.BadRequest;
                    response.ErrorMessages.Add("Something Went Wrong");
                    response.IsSuccess = false;
                    return BadRequest(response);
                }
                response.IsSuccess = true;
                response.StatusCode = HttpStatusCode.OK;
                response.Result = student;
                return Ok(response);
            }
            catch(NullReferenceException ex)
            {
                response.StatusCode = HttpStatusCode.BadRequest;
                response.ErrorMessages.Add($"{ex.Message}");
                response.IsSuccess = false;
                return BadRequest(response);
            }
        }
        //update students data
        [HttpPut("Update StudentData/{id}")]
        public async Task <IActionResult> UpdateStudent(int id,AddStudentDto student)
        {
            try
            {

                var updateStudent = await studentService.UpdateStudent(id, student);
                if (updateStudent == null)
                {
                    response.StatusCode = HttpStatusCode.BadRequest;
                    response.ErrorMessages.Add("Something Went Wrong");
                    response.IsSuccess = false;
                    return BadRequest(response);
                }
                response.IsSuccess = true;
                response.StatusCode = HttpStatusCode.OK;
                response.Result = updateStudent;
                return Ok(response);

            }
            catch(NullReferenceException ex)
            {
                response.StatusCode = HttpStatusCode.BadRequest;
                response.ErrorMessages.Add($"{ex.Message}");
                response.IsSuccess = false;
                return BadRequest(response);
            }
        }
        // delete student
        [HttpDelete("delete/{id}")]
        public async Task <IActionResult> DeleteStudent(int id)
        {
            try
            {

                var deleteStudent = await studentService.DeleteStudent(id);
                if (deleteStudent == null)
                {
                    response.StatusCode = HttpStatusCode.BadRequest;
                    response.ErrorMessages.Add("Something Went Wrong");
                    response.IsSuccess = false;
                    return BadRequest(response);
                }
                response.IsSuccess = true;
                response.StatusCode = HttpStatusCode.OK;
                response.Result = deleteStudent;
                return Ok(response);
            }
            catch(NullReferenceException ex)
            {
                response.StatusCode = HttpStatusCode.BadRequest;
                response.ErrorMessages.Add($"{ex.Message}");
                response.IsSuccess = false;
                return BadRequest(response);
            }
        }

        //search students by name
        [HttpPost("searchByname")]
        public async Task<IActionResult> SearchByName([FromBody] string name)
        {
            try
            {
                var students = await studentService.SearchStudentByName(name);
                if (students == null)
                {
                    response.StatusCode = HttpStatusCode.BadRequest;
                    response.ErrorMessages.Add("Something Went Wrong");
                    response.IsSuccess = false;
                    return BadRequest(response);
                }
                response.IsSuccess = true;
                response.StatusCode = HttpStatusCode.OK;
                response.Result = students;
                return Ok(response);
            }
            catch(NullReferenceException ex)
            {
                response.StatusCode = HttpStatusCode.BadRequest;
                response.ErrorMessages.Add($"{ex.Message}");
                response.IsSuccess = false;
                return BadRequest(response);
            }
        }
        //seatch student by course
        [HttpPost("searchByCourse")]
        public async Task <IActionResult> SearchByCourse([FromBody] string course)
        {
            try
            {
                var students = await studentService.SearchStudentByCourse(course);
                if (students == null)
                {
                    response.StatusCode = HttpStatusCode.BadRequest;
                    response.ErrorMessages.Add("Something Went Wrong");
                    response.IsSuccess = false;
                    return BadRequest(response);
                }
                response.IsSuccess = true;
                response.StatusCode = HttpStatusCode.OK;
                response.Result = students;
                return Ok(response);
            }
            catch (NullReferenceException ex)
            {
                response.StatusCode = HttpStatusCode.BadRequest;
                response.ErrorMessages.Add($"{ex.Message}");
                response.IsSuccess = false;
                return BadRequest(response);
            }
        }

        // paginate data by setting page size and page number
        [HttpGet("paginated")]
        public async Task<ActionResult<IEnumerable<Student>>>GetStudentByPagination(int pagenumber=1,int pagesize = 5)
        {
            var students = await studentService.GetPaginatedStudents(pagenumber,pagesize);
            var totalcount = await studentService.GetTotalStudentCount();
            return Ok(new
            {
                TotalCount = totalcount,
                PageNumber = pagenumber,
                PageSize = pagesize,
                Students = students
            });
        }
    }
}
