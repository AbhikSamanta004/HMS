using AutoMapper;
using HMS.API.DTOs;
using HMS.API.Helpers;
using HMS.API.Models.Entities;
using HMS.API.Repositories.Interfaces;
using HMS.API.Services.Interfaces;
using BC = BCrypt.Net.BCrypt;

namespace HMS.API.Services
{
    public class AuthService : IAuthService
    {
        private readonly IGenericRepository<User> _userRepo;
        private readonly IMapper _mapper;
        private readonly JwtHelper _jwtHelper;

        public AuthService(IGenericRepository<User> userRepo, IMapper mapper, JwtHelper jwtHelper)
        {
            _userRepo = userRepo;
            _mapper = mapper;
            _jwtHelper = jwtHelper;
        }

        public async Task<AuthResponseDto?> LoginAsync(LoginDto loginDto)
        {
            var user = (await _userRepo.GetAllAsync(u => u.Email == loginDto.Email)).FirstOrDefault();
            if (user == null || !BC.Verify(loginDto.Password, user.PasswordHash))
            {
                return null;
            }

            var response = _mapper.Map<AuthResponseDto>(user);
            response.Token = _jwtHelper.GenerateToken(user);
            return response;
        }

        public async Task<AuthResponseDto?> RegisterAsync(RegisterDto registerDto)
        {
            var existingUser = (await _userRepo.GetAllAsync(u => u.Email == registerDto.Email)).FirstOrDefault();
            if (existingUser != null) return null;

            var user = _mapper.Map<User>(registerDto);
            user.PasswordHash = BC.HashPassword(registerDto.Password);
            user.Role = "Patient"; // Secure: Hardcoded to Patient

            await _userRepo.AddAsync(user);
            await _userRepo.SaveAsync();

            var response = _mapper.Map<AuthResponseDto>(user);
            response.Token = _jwtHelper.GenerateToken(user);
            return response;
        }

        public async Task<AuthResponseDto?> CreateUserByAdminAsync(AdminUserCreateDto adminDto)
        {
            var existingUser = (await _userRepo.GetAllAsync(u => u.Email == adminDto.Email)).FirstOrDefault();
            if (existingUser != null) return null;

            var user = new User
            {
                Username = adminDto.Username,
                Email = adminDto.Email,
                PasswordHash = BC.HashPassword(adminDto.Password),
                Role = adminDto.Role // Admin can set the role
            };

            await _userRepo.AddAsync(user);
            await _userRepo.SaveAsync();

            return _mapper.Map<AuthResponseDto>(user);
        }
    }
}
