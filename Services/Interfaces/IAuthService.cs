using HMS.API.DTOs;

namespace HMS.API.Services.Interfaces
{
    public interface IAuthService
    {
        Task<AuthResponseDto?> RegisterAsync(RegisterDto registerDto);
        Task<AuthResponseDto?> LoginAsync(LoginDto loginDto);
        Task<AuthResponseDto?> CreateUserByAdminAsync(AdminUserCreateDto adminDto); // New Admin Method
    }
}
