using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.DTOs.Common;
using Application.DTOs.User;

namespace Application.Interfaces
{
    public interface IUserService
    {
        Task<ApiResponseDto<UserDto>> RegisterUserAsync(UserCreateDto userCreateDto);
        Task<ApiResponseDto<UserDto>> AuthenticateAsync(UserLoginDto userLoginDto);
        Task<ApiResponseDto<UserDto>> GetUserByIdAsync(int id);
        Task<ApiResponseDto<IEnumerable<UserDto>>> GetAllUsersAsync();
        Task<ApiResponseDto<UserDto>> UpdateUserAsync(int id, UserUpdateDto userUpdateDto);
        Task<ApiResponseDto<bool>> ChangePasswordAsync(int userId, UserPasswordChangeDto passwordChangeDto);
        Task<ApiResponseDto<bool>> DeactivateUserAsync(int id);
        Task<ApiResponseDto<bool>> ReactivateUserAsync(int id);
        Task<ApiResponseDto<IEnumerable<UserDto>>> GetUsersByRoleAsync(string role);
        Task<ApiResponseDto<bool>> DeleteUserAsync(int id);
        Task<ApiResponseDto<bool>> UpdateSellerRatingAsync(int sellerId);
    }
}
