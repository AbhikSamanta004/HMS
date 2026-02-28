using HMS.API.DTOs;

namespace HMS.API.Services.Interfaces
{
    public interface IAppointmentService
    {
        Task<IEnumerable<AppointmentResponseDto>> GetAllAppointmentsAsync();
        Task<AppointmentResponseDto?> GetAppointmentByIdAsync(int id);
        Task<AppointmentResponseDto> BookAppointmentAsync(AppointmentRequestDto appointmentDto);
        Task<bool> UpdateStatusAsync(int id, string status);
        Task<bool> DeleteAppointmentAsync(int id);
    }
}
