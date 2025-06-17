using AutoMapper;
using Student_Management_App_MVC.DataBase.Repositories.Interfaces;
using Student_Management_App_MVC.Models.DTOs.Student;
using Student_Management_App_MVC.Models.Entities;
using Student_Management_App_MVC.Services.Interfaces;

namespace Student_Management_App_MVC.Services.Implementations
{
    public class StudentService : IStudentService
    {
        private readonly IStudentRepository _studentRepository;
        private readonly IMapper _mapper;
        private readonly ICacheService _cacheService;

        public StudentService(IStudentRepository studentRepository, IMapper mapper, ICacheService cacheService)
        {
            _studentRepository = studentRepository;
            _mapper = mapper;
            _cacheService = cacheService;
        }

        public async Task<IEnumerable<StudentReadDto>> GetAllStudentsAsync()
        {
            var cacheKey = "all_students";
            var cachestudents = await _cacheService.GetRedisCacheAsync<IEnumerable<StudentReadDto>>(cacheKey);
            if (cachestudents != null)
            {
                return cachestudents;
            }
            var students = await _studentRepository.GetAllStudentAsync();
            var studentDtos = _mapper.Map<IEnumerable<StudentReadDto>>(students);
            await _cacheService.SetRedisCacheAsync(cacheKey, studentDtos, TimeSpan.FromMinutes(30));
            return studentDtos;

        }
        public async Task<StudentReadDto> GetStudentByIdAsync(int id)
        {
            var cacheKey = $"student_{id}";
            var cachedStudent = await _cacheService.GetRedisCacheAsync<StudentReadDto>(cacheKey);
            if (cachedStudent != null)
            {
                return cachedStudent;
            }
            var student = await _studentRepository.GetStudentByIdAsync(id);
            if (student == null)
            {
                return null;
            }
            var studentDto = _mapper.Map<StudentReadDto>(student);
            await _cacheService.SetRedisCacheAsync(cacheKey, studentDto, TimeSpan.FromMinutes(30));
            return studentDto;
        }

        public async Task<StudentReadDto> AddStudentAsync(StudentCreateDto studentcreate)
        {
            var student = _mapper.Map<Student>(studentcreate);
            var createdStudent = await _studentRepository.AddStudentAsync(student);
            var studentDto = _mapper.Map<StudentReadDto>(createdStudent);
            await _cacheService.RemoveRedisCacheAsync("all_students");
            return studentDto;
        }
        public async Task<StudentReadDto> UpdateStudentAsync(StudentUpdateDto studentupdate)
        {
            var student = _mapper.Map<Student>(studentupdate);
            var updatedStudent = await _studentRepository.UpdateStudentAsync(student);
            var studentDto = _mapper.Map<StudentReadDto>(updatedStudent);
            await _cacheService.RemoveRedisCacheAsync($"student_{student.StudentID}");
            await _cacheService.RemoveRedisCacheAsync("all_students");
            return studentDto;
        }
        public async Task<bool> DeleteStudentAsync(int id)
        {
            var isDeleted = await _studentRepository.DeleteStudentAsync(id);
            if (isDeleted)
            {
                await _cacheService.RemoveRedisCacheAsync($"student_{id}");
                await _cacheService.RemoveRedisCacheAsync("all_students");
            }
            return isDeleted;
        }
    }
  
}
