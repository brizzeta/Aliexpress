using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.DTOs.Common;
using Application.DTOs.User;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;
using BCrypt.Net;

namespace Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;
        private readonly IJwtTokenService _jwtTokenService;

        public UserService(IUnitOfWork uow, IMapper mapper, IJwtTokenService jwtTokenService)
        {
            _uow = uow;
            _mapper = mapper;
            _jwtTokenService = jwtTokenService;
        }

        public async Task<ApiResponseDto<UserDto>> RegisterUserAsync(UserCreateDto userCreateDto)
        {
            try
            {
                var existingUser = (await _uow.Users.FindAsync(u => u.Email == userCreateDto.Email)).FirstOrDefault();
                if (existingUser != null)
                    return ApiResponseDto<UserDto>.FailureResult("Email already in use");

                var user = _mapper.Map<User>(userCreateDto);

                user.Password_hash = HashPassword(userCreateDto.Password);

                await _uow.Users.AddAsync(user);
                await _uow.CompleteAsync();

                var userDto = _mapper.Map<UserDto>(user);
                return ApiResponseDto<UserDto>.SuccessResult(userDto, "User registered successfully");
            }
            catch (Exception ex)
            {
                return ApiResponseDto<UserDto>.FailureResult("Error registering user", new List<string> { ex.Message });
            }
        }

        public async Task<ApiResponseDto<UserDto>> AuthenticateAsync(UserLoginDto userLoginDto)
        {
            try
            {
                var user = (await _uow.Users.FindAsync(u => u.Email == userLoginDto.Email)).FirstOrDefault();

                if (user == null)
                    return ApiResponseDto<UserDto>.FailureResult("Invalid email or password");

                if (!VerifyPassword(userLoginDto.Password, user.Password_hash))
                    return ApiResponseDto<UserDto>.FailureResult("Invalid email or password");

                if (!user.IsActive)
                    return ApiResponseDto<UserDto>.FailureResult("Account is deactivated");

                var token = _jwtTokenService.GenerateToken(user);
                var userDto = _mapper.Map<UserDto>(user);
                userDto.Token = token;

                return ApiResponseDto<UserDto>.SuccessResult(userDto, "Login successful");
            }
            catch (Exception ex)
            {
                return ApiResponseDto<UserDto>.FailureResult("Error during authentication", new List<string> { ex.Message });
            }
        }

        public async Task<ApiResponseDto<UserDto>> GetUserByIdAsync(int id)
        {
            try
            {
                var user = await _uow.Users.GetByIdAsync(id);
                if (user == null)
                    return ApiResponseDto<UserDto>.FailureResult($"User with ID {id} not found");

                var userDto = _mapper.Map<UserDto>(user);
                return ApiResponseDto<UserDto>.SuccessResult(userDto);
            }
            catch (Exception ex)
            {
                return ApiResponseDto<UserDto>.FailureResult("Error retrieving user", new List<string> { ex.Message });
            }
        }

        public async Task<ApiResponseDto<IEnumerable<UserDto>>> GetAllUsersAsync()
        {
            try
            {
                var users = await _uow.Users.GetAllAsync();
                var userDtos = _mapper.Map<IEnumerable<UserDto>>(users);
                return ApiResponseDto<IEnumerable<UserDto>>.SuccessResult(userDtos);
            }
            catch (Exception ex)
            {
                return ApiResponseDto<IEnumerable<UserDto>>.FailureResult("Error retrieving users", new List<string> { ex.Message });
            }
        }

        public async Task<ApiResponseDto<UserDto>> UpdateUserAsync(int id, UserUpdateDto userUpdateDto)
        {
            try
            {
                var user = await _uow.Users.GetByIdAsync(id);
                if (user == null)
                    return ApiResponseDto<UserDto>.FailureResult($"User with ID {id} not found");

                _mapper.Map(userUpdateDto, user);

                _uow.Users.Update(user);
                await _uow.CompleteAsync();

                var userDto = _mapper.Map<UserDto>(user);
                return ApiResponseDto<UserDto>.SuccessResult(userDto, "User updated successfully");
            }
            catch (Exception ex)
            {
                return ApiResponseDto<UserDto>.FailureResult("Error updating user", new List<string> { ex.Message });
            }
        }

        public async Task<ApiResponseDto<bool>> ChangePasswordAsync(int userId, UserPasswordChangeDto passwordChangeDto)
        {
            try
            {
                var user = await _uow.Users.GetByIdAsync(userId);
                if (user == null)
                    return ApiResponseDto<bool>.FailureResult($"User with ID {userId} not found");

                if (!VerifyPassword(passwordChangeDto.CurrentPassword, user.Password_hash))
                    return ApiResponseDto<bool>.FailureResult("Current password is incorrect");

                user.Password_hash = HashPassword(passwordChangeDto.NewPassword);

                _uow.Users.Update(user);
                await _uow.CompleteAsync();

                return ApiResponseDto<bool>.SuccessResult(true, "Password changed successfully");
            }
            catch (Exception ex)
            {
                return ApiResponseDto<bool>.FailureResult("Error changing password", new List<string> { ex.Message });
            }
        }

        public async Task<ApiResponseDto<bool>> DeactivateUserAsync(int id)
        {
            try
            {
                var user = await _uow.Users.GetByIdAsync(id);
                if (user == null)
                    return ApiResponseDto<bool>.FailureResult($"User with ID {id} not found");

                user.IsActive = false;

                _uow.Users.Update(user);
                await _uow.CompleteAsync();

                return ApiResponseDto<bool>.SuccessResult(true, "User deactivated successfully");
            }
            catch (Exception ex)
            {
                return ApiResponseDto<bool>.FailureResult("Error deactivating user", new List<string> { ex.Message });
            }
        }

        public async Task<ApiResponseDto<bool>> ReactivateUserAsync(int id)
        {
            try
            {
                var user = await _uow.Users.GetByIdAsync(id);
                if (user == null)
                    return ApiResponseDto<bool>.FailureResult($"User with ID {id} not found");

                user.IsActive = true;

                _uow.Users.Update(user);
                await _uow.CompleteAsync();

                return ApiResponseDto<bool>.SuccessResult(true, "User reactivated successfully");
            }
            catch (Exception ex)
            {
                return ApiResponseDto<bool>.FailureResult("Error reactivating user", new List<string> { ex.Message });
            }
        }

        public async Task<ApiResponseDto<IEnumerable<UserDto>>> GetUsersByRoleAsync(string role)
        {
            try
            {
                if (!Enum.TryParse(role, true, out Domain.Enums.UserRole userRole))
                    return ApiResponseDto<IEnumerable<UserDto>>.FailureResult("Invalid role specified");

                var users = await _uow.Users.FindAsync(u => u.Role == userRole);
                var userDtos = _mapper.Map<IEnumerable<UserDto>>(users);
                return ApiResponseDto<IEnumerable<UserDto>>.SuccessResult(userDtos);
            }
            catch (Exception ex)
            {
                return ApiResponseDto<IEnumerable<UserDto>>.FailureResult("Error retrieving users by role", new List<string> { ex.Message });
            }
        }

        public async Task<ApiResponseDto<bool>> DeleteUserAsync(int id)
        {
            try
            {
                var user = await _uow.Users.GetByIdAsync(id);
                if (user == null)
                    return ApiResponseDto<bool>.FailureResult($"User with ID {id} not found");

                var userHasOrders = (await _uow.Orders.FindAsync(o => o.BuyerId == id)).Any();
                var userHasProducts = (await _uow.Products.FindAsync(p => p.SellerId == id)).Any();
                var userHasReviews = (await _uow.Reviews.FindAsync(r => r.BuyerID == id || r.SellerID == id)).Any();

                if (userHasOrders || userHasProducts || userHasReviews)
                {
                    return await DeactivateUserAsync(id);
                }

                _uow.Users.Delete(user);
                await _uow.CompleteAsync();

                return ApiResponseDto<bool>.SuccessResult(true, "User deleted successfully");
            }
            catch (Exception ex)
            {
                return ApiResponseDto<bool>.FailureResult("Error deleting user", new List<string> { ex.Message });
            }
        }

        public async Task<ApiResponseDto<bool>> UpdateSellerRatingAsync(int sellerId)
        {
            try
            {
                var seller = await _uow.Users.GetByIdAsync(sellerId);
                if (seller == null)
                    return ApiResponseDto<bool>.FailureResult($"Seller with ID {sellerId} not found");

                var reviews = await _uow.Reviews.FindAsync(r => r.SellerID == sellerId);

                if (reviews.Any())
                {
                    decimal averageRating = reviews.Average(r => r.Rating);

                    seller.Rating = averageRating;
                    _uow.Users.Update(seller);
                    await _uow.CompleteAsync();

                    return ApiResponseDto<bool>.SuccessResult(true, "Seller rating updated successfully");
                }
                else
                {
                    seller.Rating = 0;
                    _uow.Users.Update(seller);
                    await _uow.CompleteAsync();

                    return ApiResponseDto<bool>.SuccessResult(true, "Seller has no reviews, rating set to 0");
                }
            }
            catch (Exception ex)
            {
                return ApiResponseDto<bool>.FailureResult("Error updating seller rating", new List<string> { ex.Message });
            }
        }

        #region Helper Methods
        private string HashPassword(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password, BCrypt.Net.BCrypt.GenerateSalt(12));
        }

        private bool VerifyPassword(string password, string storedHash)
        {
            return BCrypt.Net.BCrypt.Verify(password, storedHash);
        }
        #endregion
    }
}
