using Application.DTOs.Common;
using Application.DTOs.User;
using Application.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Aliexpress_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        /// <summary>
        /// Регистрирует нового пользователя
        /// </summary>
        [HttpPost("register")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<ApiResponseDto<UserDto>>> Register([FromBody] UserCreateDto userCreateDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var response = await _userService.RegisterUserAsync(userCreateDto);
            if (!response.Success)
                return BadRequest(response);

            return Ok(response);
        }

        /// <summary>
        /// Аутентифицирует пользователя
        /// </summary>
        [HttpPost("login")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<ApiResponseDto<UserDto>>> Login([FromBody] UserLoginDto userLoginDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var response = await _userService.AuthenticateAsync(userLoginDto);
            if (!response.Success)
                return BadRequest(response);

            return Ok(response);
        }

        /// <summary>
        /// Получает всех пользователей
        /// </summary>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<ApiResponseDto<IEnumerable<UserDto>>>> GetAllUsers()
        {
            var response = await _userService.GetAllUsersAsync();
            if (!response.Success)
                return BadRequest(response);

            return Ok(response);
        }

        /// <summary>
        /// Получает пользователя по ID
        /// </summary>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<ApiResponseDto<UserDto>>> GetUserById(int id)
        {
            var response = await _userService.GetUserByIdAsync(id);
            if (!response.Success)
            {
                if (response.Message.Contains("not found"))
                    return NotFound(response);

                return BadRequest(response);
            }

            return Ok(response);
        }

        /// <summary>
        /// Обновляет данные пользователя
        /// </summary>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<ApiResponseDto<UserDto>>> UpdateUser(int id, [FromBody] UserUpdateDto userUpdateDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var response = await _userService.UpdateUserAsync(id, userUpdateDto);
            if (!response.Success)
            {
                if (response.Message.Contains("not found"))
                    return NotFound(response);

                return BadRequest(response);
            }

            return Ok(response);
        }

        /// <summary>
        /// Изменяет пароль пользователя
        /// </summary>
        [HttpPut("{id}/change-password")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<ApiResponseDto<bool>>> ChangePassword(int id, [FromBody] UserPasswordChangeDto passwordChangeDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var response = await _userService.ChangePasswordAsync(id, passwordChangeDto);
            if (!response.Success)
            {
                if (response.Message.Contains("not found"))
                    return NotFound(response);

                return BadRequest(response);
            }

            return Ok(response);
        }

        /// <summary>
        /// Деактивирует пользователя
        /// </summary>
        [HttpPut("{id}/deactivate")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<ApiResponseDto<bool>>> DeactivateUser(int id)
        {
            var response = await _userService.DeactivateUserAsync(id);
            if (!response.Success)
            {
                if (response.Message.Contains("not found"))
                    return NotFound(response);

                return BadRequest(response);
            }

            return Ok(response);
        }

        /// <summary>
        /// Реактивирует пользователя
        /// </summary>
        [HttpPut("{id}/reactivate")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<ApiResponseDto<bool>>> ReactivateUser(int id)
        {
            var response = await _userService.ReactivateUserAsync(id);
            if (!response.Success)
            {
                if (response.Message.Contains("not found"))
                    return NotFound(response);

                return BadRequest(response);
            }

            return Ok(response);
        }

        /// <summary>
        /// Получает пользователей по роли
        /// </summary>
        [HttpGet("by-role/{role}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<ApiResponseDto<IEnumerable<UserDto>>>> GetUsersByRole(string role)
        {
            var response = await _userService.GetUsersByRoleAsync(role);
            if (!response.Success)
                return BadRequest(response);

            return Ok(response);
        }

        /// <summary>
        /// Удаляет пользователя по ID
        /// </summary>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<ApiResponseDto<bool>>> DeleteUser(int id)
        {
            var response = await _userService.DeleteUserAsync(id);
            if (!response.Success)
            {
                if (response.Message.Contains("not found"))
                    return NotFound(response);

                return BadRequest(response);
            }

            return Ok(response);
        }

        /// <summary>
        /// Обновляет рейтинг продавца
        /// </summary>
        [HttpPut("sellers/{sellerId}/update-rating")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<ApiResponseDto<bool>>> UpdateSellerRating(int sellerId)
        {
            var response = await _userService.UpdateSellerRatingAsync(sellerId);
            if (!response.Success)
            {
                if (response.Message.Contains("not found"))
                    return NotFound(response);

                return BadRequest(response);
            }

            return Ok(response);
        }
    }
}
