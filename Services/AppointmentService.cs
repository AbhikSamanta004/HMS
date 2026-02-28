using AutoMapper;
using HMS.API.DTOs;
using HMS.API.Models.Entities;
using HMS.API.Repositories.Interfaces;
using HMS.API.Services.Interfaces;

namespace HMS.API.Services
{
    public class AppointmentService : IAppointmentService
    {
        private readonly IGenericRepository<Appointment> _appointmentRepo;
        private readonly IMapper _mapper;

        public AppointmentService(IGenericRepository<Appointment> appointmentRepo, IMapper mapper)
        {
            _appointmentRepo = appointmentRepo;
            _mapper = mapper;
        }

        public async Task<AppointmentResponseDto> BookAppointmentAsync(AppointmentRequestDto appointmentDto)
        {
            var appointment = _mapper.Map<Appointment>(appointmentDto);
            appointment.Status = "Scheduled";
            await _appointmentRepo.AddAsync(appointment);
            await _appointmentRepo.SaveAsync();
            
            // Reload with relations for the response
            var saved = (await _appointmentRepo.GetAllAsync(a => a.Id == appointment.Id, "Patient,Doctor")).First();
            return _mapper.Map<AppointmentResponseDto>(saved);
        }

        public async Task<bool> DeleteAppointmentAsync(int id)
        {
            var appointment = await _appointmentRepo.GetByIdAsync(id);
            if (appointment == null) return false;
            _appointmentRepo.Delete(appointment);
            await _appointmentRepo.SaveAsync();
            return true;
        }

        public async Task<IEnumerable<AppointmentResponseDto>> GetAllAppointmentsAsync()
        {
            var appointments = await _appointmentRepo.GetAllAsync(null, "Patient,Doctor");
            return _mapper.Map<IEnumerable<AppointmentResponseDto>>(appointments);
        }

        public async Task<AppointmentResponseDto?> GetAppointmentByIdAsync(int id)
        {
            var appointment = (await _appointmentRepo.GetAllAsync(a => a.Id == id, "Patient,Doctor")).FirstOrDefault();
            return _mapper.Map<AppointmentResponseDto>(appointment);
        }

        public async Task<bool> UpdateStatusAsync(int id, string status)
        {
            var appointment = await _appointmentRepo.GetByIdAsync(id);
            if (appointment == null) return false;
            appointment.Status = status;
            _appointmentRepo.Update(appointment);
            await _appointmentRepo.SaveAsync();
            return true;
        }
    }
}
