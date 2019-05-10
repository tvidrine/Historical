// ------------------------------------------------------------------------------------------------------------------------
// Copyright (c) ZoomAudits, LLC.
//
// Created By: Tim Vidrine
// Created On: 4/13/2018
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
    /// Client Web EndPoint
    /// </summary>
    [Route("api/client")]
    public class ClientController : Controller
    {
        private readonly IClientApplicationService _applicationService;
        private readonly ILogManager _logManager;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="logManager">A logger</param>
        /// <param name="applicationService">The application service containing the businesss logic and workflow.</param>
        public ClientController(ILogManager logManager, IClientApplicationService applicationService)
        {
            _applicationService = applicationService;
            _logManager = logManager;
        }

        /// <summary>
        /// Create a new client
        /// </summary>
        /// <returns></returns>
        [HttpGet("create")]
        [ProducesResponseType(typeof(CreateWebResponse<ClientDto>), 200)]
        public async Task<IActionResult> CreateClientAsync()
        {
            try
            {
                var response = await _applicationService.CreateAsync();
                var webResponse = new CreateWebResponse<ClientDto>()
                    .From(response);

                if (webResponse.IsSuccessful)
                    return Ok(webResponse);

                return BadRequest(webResponse.Errors);
            }
            catch (Exception ex)
            {
                var message = "Unable to create a client";
                Console.WriteLine(ex);
                _logManager.LogError(ex, message);
                return BadRequest(message);
            }
        }

        /// <summary>
        /// Delete a client
        /// </summary>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(DeleteWebResponse), 200)]
        public async Task<IActionResult> DeleteClientAsync(int id)
        {
            if (id == 0)
                return BadRequest("Client Id cannot be 0");

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
                var message = "Unable to delete a client";
                Console.WriteLine(ex);
                _logManager.LogError(ex, message);
                return BadRequest(message);
            }
        }

        /// <summary>
        /// Retrieves all active client
        /// </summary>
        /// <returns></returns>
        [HttpGet("all")]
        [ProducesResponseType(typeof(GetWebResponse<ClientsDto>), 200)]
        public async Task<IActionResult> GetAllClientsAsync()
        {
            try
            {
                var response = await _applicationService.GetAllAsync();

                var webResponse = new GetWebResponse<ClientsDto>()
                    .From(response);

                if (webResponse.IsSuccessful)
                    return Ok(webResponse);

                return BadRequest(webResponse.Errors);
            }
            catch (Exception ex)
            {
                var message = "Unable to retrieve a list of clients";
                Console.WriteLine(ex);
                _logManager.LogError(ex, message);
                return BadRequest(message);
            }
        }

        /// <summary>
        /// Retrieves a client
        /// </summary>
        /// <returns></returns>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(GetWebResponse<ClientDto>), 200)]
        public async Task<IActionResult> GetClientAsync(int id)
        {
            if (id == 0)
                return BadRequest("A valid Id is required");

            try
            {
                var response = await _applicationService.GetAsync(id);
                var webResponse = new GetWebResponse<ClientDto>()
                    .From(response);

                if (webResponse.IsSuccessful)
                    return Ok(webResponse);

                return BadRequest(webResponse.Errors);
            }
            catch (Exception ex)
            {
                var message = "Unable to retrieve client";
                Console.WriteLine(ex);
                _logManager.LogError(ex, message);
                return BadRequest(message);
            }
        }

        /// <summary>
        /// Saves a client
        /// </summary>
        /// <returns></returns>
        [HttpPost("")]
        [ProducesResponseType(typeof(SaveWebResponse<ClientDto>), 200)]
        public async Task<IActionResult> SaveClientAsync([FromBody] ClientDto clientDto)
        {
            try
            {
                var client = clientDto.ToModel();
                var response = await _applicationService.SaveAsync(client);
                var webResponse = new SaveWebResponse<ClientDto>()
                    .From(response);

                if (webResponse.IsSuccessful)
                    return Ok(webResponse);

                return BadRequest(webResponse.Errors);
            }
            catch (Exception ex)
            {
                var message = "Unable to save the client";
                Console.WriteLine(ex);
                _logManager.LogError(ex, message);
                return BadRequest(message);
            }
        }
    }
}
