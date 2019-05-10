// ------------------------------------------------------------------------------------------------------------------------
// Copyright (c) ZoomAudits, LLC.
//
// Created By: Tim Vidrine
// Created On: 5/2/2018
// ------------------------------------------------------------------------------------------------------------------------

using System;
using System.Threading.Tasks;
using Apollo.Core.Contracts;
using Apollo.Core.Contracts.ApplicationServices;
using Apollo.WebApi.Messages.Responses;
using Apollo.WebApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace Apollo.WebApi.Controllers
{
    /// <summary>
    /// User Web EndPoint
    /// </summary>
    [Route("api/user")]
    public class UserController : Controller
    {
        private readonly IUserApplicationService _applicationService;
        private readonly ILogManager _logManager;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="logManager">A logger</param>
        /// <param name="applicationService">The application service containing the businesss logic and workflow.</param>
        public UserController(ILogManager logManager, IUserApplicationService applicationService)
        {
            _applicationService = applicationService;
            _logManager = logManager;
        }

        /// <summary>
        /// Create a new user
        /// </summary>
        /// <returns></returns>
        [HttpGet("new")]
        [ProducesResponseType(typeof(CreateWebResponse<UserDto>), 200)]
        public async Task<IActionResult> CreateUserAsync()
        {
            try
            {
                var response = await _applicationService.CreateAsync();
                var webResponse = new CreateWebResponse<UserDto>()
                    .From(response);

                if (webResponse.IsSuccessful)
                    return Ok(webResponse);

                return BadRequest(webResponse.Errors);
            }
            catch (Exception ex)
            {
                var message = "Unable to create a user";
                Console.WriteLine(ex);
                _logManager.LogError(ex, message);
                return BadRequest(message);
            }
        }

        /// <summary>
        /// Delete a user
        /// </summary>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(DeleteWebResponse), 200)]
        public async Task<IActionResult> DeleteUserAsync(int id)
        {
            if (id == 0)
                return BadRequest("User Id must be valid");

            try
            {
                var response = await _applicationService.DeleteAsync(id);
                var webResponse = new DeleteWebResponse()
                    .From(response);

                if (webResponse.IsSuccessful)
                    return Ok(webResponse);

                return BadRequest(webResponse.Errors);
            }
            catch (Exception ex)
            {
                var message = "Unable to delete a user";
                Console.WriteLine(ex);
                _logManager.LogError(ex, message);
                return BadRequest(message);
            }
        }

        /// <summary>
        /// Retrieves all active user
        /// </summary>
        /// <returns></returns>
        [HttpGet("")]
        [ProducesResponseType(typeof(GetWebResponse<UsersDto>), 200)]
        public async Task<IActionResult> GetAllUsersAsync()
        {
            try
            {
                var response = await _applicationService.GetAllAsync();
                var webResponse = new GetWebResponse<UsersDto>()
                    .From(response);

                if (webResponse.IsSuccessful)
                    return Ok(webResponse);

                return BadRequest(webResponse.Errors);
            }
            catch (Exception ex)
            {
                var message = "Unable to retrieve a list of users";
                Console.WriteLine(ex);
                _logManager.LogError(ex, message);
                return BadRequest(message);
            }
        }

        /// <summary>
        /// Retrieves a user
        /// </summary>
        /// <returns></returns>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(GetWebResponse<UserDto>), 200)]
        public async Task<IActionResult> GetUserAsync(int id)
        {
            if (id == 0)
                return BadRequest("A valid Id is required");

            try
            {
                var response = await _applicationService.GetAsync(id);
                var webResponse = new GetWebResponse<UserDto>()
                    .From(response);

                if (webResponse.IsSuccessful)
                    return Ok(webResponse);

                return BadRequest(webResponse.Errors);
            }
            catch (Exception ex)
            {
                var message = "Unable to retrieve user";
                Console.WriteLine(ex);
                _logManager.LogError(ex, message);
                return BadRequest(message);
            }
        }

       
        /// <summary>
        /// Saves a user
        /// </summary>
        /// <returns></returns>
        [HttpPost("")]
        [ProducesResponseType(typeof(SaveWebResponse<UserDto>), 200)]
        public async Task<IActionResult> SaveUserAsync([FromBody] UserDto userDto)
        {
            if (userDto == null)
                return BadRequest("User DTO cannot be null");

            try
            {
                var user = userDto.ToModel();
                var response = await _applicationService.SaveAsync(user);
                var webResponse = new SaveWebResponse<UserDto>()
                    .From(response);

                if (webResponse.IsSuccessful)
                    return Ok(webResponse);

                return BadRequest(webResponse.Errors);
            }
            catch (Exception ex)
            {
                var message = "Unable to save the user";
                Console.WriteLine(ex);
                _logManager.LogError(ex, message);
                return BadRequest(message);
            }
        }
    }
}
