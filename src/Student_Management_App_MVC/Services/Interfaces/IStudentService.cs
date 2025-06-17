using Student_Management_App_MVC.Models.DTOs.Student;

namespace Student_Management_App_MVC.Services.Interfaces
{
    public interface IStudentService
    {
        Task<IEnumerable<StudentReadDto>> GetAllStudentsAsync();
        Task<StudentReadDto> GetStudentByIdAsync(int id);
        Task<StudentReadDto> AddStudentAsync(StudentCreateDto studentcreate);
        Task<StudentReadDto> UpdateStudentAsync(StudentUpdateDto studentupdate);
        Task<bool> DeleteStudentAsync(int id);

    }
}
