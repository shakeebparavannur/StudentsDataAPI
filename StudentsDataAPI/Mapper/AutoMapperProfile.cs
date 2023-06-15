using AutoMapper;
using StudentsDataAPI.Model;
using StudentsDataAPI.Model.Dto;

namespace StudentsDataAPI.Mapper
{
    public class AutoMapperProfile:Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Student,AddStudentDto>().ReverseMap();
        }
    }
}
