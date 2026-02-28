using AutoMapper;
using HMS.API.DTOs;
using HMS.API.Models.Entities;
using HMS.API.Repositories.Interfaces;
using HMS.API.Services.Interfaces;

namespace HMS.API.Services
{
    public class PatientService : IPatientService
    {
        private readonly IGenericRepository<Patient> _patientRepo;
        private readonly IMapper _mapper;

        public PatientService(IGenericRepository<Patient> patientRepo, IMapper mapper)
        {
            _patientRepo = patientRepo;
            _mapper = mapper;
        }

        public async Task<PatientResponseDto> CreatePatientAsync(PatientRequestDto patientDto)
        {
            var patient = _mapper.Map<Patient>(patientDto);
            await _patientRepo.AddAsync(patient);
            await _patientRepo.SaveAsync();
            return _mapper.Map<PatientResponseDto>(patient);
        }

        public async Task<bool> DeletePatientAsync(int id)
        {
            var patient = await _patientRepo.GetByIdAsync(id);
            if (patient == null) return false;
            _patientRepo.Delete(patient);
            await _patientRepo.SaveAsync();
            return true;
        }

        public async Task<IEnumerable<PatientResponseDto>> GetAllPatientsAsync()
        {
            var patients = await _patientRepo.GetAllAsync();
            return _mapper.Map<IEnumerable<PatientResponseDto>>(patients);
        }

        public async Task<PatientResponseDto?> GetPatientByIdAsync(int id)
        {
            var patient = await _patientRepo.GetByIdAsync(id);
            return _mapper.Map<PatientResponseDto>(patient);
        }

        public async Task<bool> UpdatePatientAsync(int id, PatientRequestDto patientDto)
        {
            var patient = await _patientRepo.GetByIdAsync(id);
            if (patient == null) return false;

            _mapper.Map(patientDto, patient);
            _patientRepo.Update(patient);
            await _patientRepo.SaveAsync();
            return true;
        }
    }
}
