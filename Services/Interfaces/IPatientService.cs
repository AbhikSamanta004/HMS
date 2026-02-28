using HMS.API.DTOs;

namespace HMS.API.Services.Interfaces
{
    public interface IPatientService
    {
        Task<IEnumerable<PatientResponseDto>> GetAllPatientsAsync();
        Task<PatientResponseDto?> GetPatientByIdAsync(int id);
        Task<PatientResponseDto> CreatePatientAsync(PatientRequestDto patientDto);
        Task<bool> UpdatePatientAsync(int id, PatientRequestDto patientDto);
        Task<bool> DeletePatientAsync(int id);
    }
}
