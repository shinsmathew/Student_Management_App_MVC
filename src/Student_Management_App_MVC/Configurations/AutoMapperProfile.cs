using AutoMapper;
using Student_Management_App_MVC.Models.DTOs.Student;
using Student_Management_App_MVC.Models.DTOs.User;
using Student_Management_App_MVC.Models.Entities;

namespace Student_Management_App_MVC.Configurations
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<UserRegisterDto, User>();

            CreateMap<Student, StudentReadDto>();

            CreateMap<StudentCreateDto, Student>();
           
            CreateMap<StudentUpdateDto, Student>();
               
        }
    }
   
}
