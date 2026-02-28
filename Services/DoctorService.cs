using AutoMapper;
using HMS.API.DTOs;
using HMS.API.Models.Entities;
using HMS.API.Repositories.Interfaces;
using HMS.API.Services.Interfaces;

namespace HMS.API.Services
{
    public class DoctorService : IDoctorService
    {
        private readonly IGenericRepository<Doctor> _doctorRepo;
        private readonly IMapper _mapper;

        public DoctorService(IGenericRepository<Doctor> doctorRepo, IMapper mapper)
        {
            _doctorRepo = doctorRepo;
            _mapper = mapper;
        }

        public async Task<DoctorResponseDto> CreateDoctorAsync(DoctorRequestDto doctorDto)
        {
            var doctor = _mapper.Map<Doctor>(doctorDto);
            await _doctorRepo.AddAsync(doctor);
            await _doctorRepo.SaveAsync();
            return _mapper.Map<DoctorResponseDto>(doctor);
        }

        public async Task<bool> DeleteDoctorAsync(int id)
        {
            var doctor = await _doctorRepo.GetByIdAsync(id);
            if (doctor == null) return false;
            _doctorRepo.Delete(doctor);
            await _doctorRepo.SaveAsync();
            return true;
        }

        public async Task<IEnumerable<DoctorResponseDto>> GetAllDoctorsAsync()
        {
            var doctors = await _doctorRepo.GetAllAsync();
            return _mapper.Map<IEnumerable<DoctorResponseDto>>(doctors);
        }

        public async Task<DoctorResponseDto?> GetDoctorByIdAsync(int id)
        {
            var doctor = await _doctorRepo.GetByIdAsync(id);
            return _mapper.Map<DoctorResponseDto>(doctor);
        }

        public async Task<bool> UpdateDoctorAsync(int id, DoctorRequestDto doctorDto)
        {
            var doctor = await _doctorRepo.GetByIdAsync(id);
            if (doctor == null) return false;

            _mapper.Map(doctorDto, doctor);
            _doctorRepo.Update(doctor);
            await _doctorRepo.SaveAsync();
            return true;
        }
    }
}
