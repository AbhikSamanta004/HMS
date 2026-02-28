using HMS.API.DTOs;

namespace HMS.API.Services.Interfaces
{
    public interface IDoctorService
    {
        Task<IEnumerable<DoctorResponseDto>> GetAllDoctorsAsync();
        Task<DoctorResponseDto?> GetDoctorByIdAsync(int id);
        Task<DoctorResponseDto> CreateDoctorAsync(DoctorRequestDto doctorDto);
        Task<bool> UpdateDoctorAsync(int id, DoctorRequestDto doctorDto);
        Task<bool> DeleteDoctorAsync(int id);
    }
}
