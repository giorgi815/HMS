using HMS.Application.Contracts.Services;
using HMS.Application.Models.Common;
using HMS.Application.Models.Room;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace HMS.Controllers
{
    [Route("api/room")]
    [ApiController]
    public class RoomController(IRoomService roomService) : ControllerBase
    {

        [HttpGet]
        public async Task<IActionResult> GetRooms([FromQuery] PagedRequestDto parameters)
        {
            var result = await roomService.GetAllRoomsAsync(parameters);
            var response = new CommonResponse()
            {
                Message = "Rooms retrieved successfully",
                Result = result,
                IsSuccess = true,
                HttpStatusCode = Convert.ToInt32(HttpStatusCode.OK)
            };
            return StatusCode(response.HttpStatusCode, response);
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetRoomById(int id)
        {
            var result = await roomService.GetRoomByIdAsync(id);
            var response = new CommonResponse()
            {
                Message = "Room retrieved successfully",
                Result = result,
                IsSuccess = true,
                HttpStatusCode = Convert.ToInt32(HttpStatusCode.OK)
            };
            return StatusCode(response.HttpStatusCode, response);
        }

        [HttpGet("search")]
        public async Task<IActionResult> SearchAvailableRooms([FromQuery] RoomSearchRequestDto parameters)
        {
            var result = await roomService.SearchAvailableRoomsAsync(parameters);
            var response = new CommonResponse()
            {
                Message = "Available rooms retrieved successfully",
                Result = result,
                IsSuccess = true,
                HttpStatusCode = Convert.ToInt32(HttpStatusCode.OK)
            };
            return StatusCode(response.HttpStatusCode, response);
        }

        [HttpPost]
        public async Task<IActionResult> CreateRoom([FromBody] RoomForCreatingDto model)
        {
            var result = await roomService.CreateRoomAsync(model);
            var response = new CommonResponse()
            {
                Message = "Room created successfully",
                Result = result,
                IsSuccess = true,
                HttpStatusCode = Convert.ToInt32(HttpStatusCode.Created)
            };
            return StatusCode(response.HttpStatusCode, response);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateRoom([FromBody] RoomForUpdatingDto model)
        {
            var result = await roomService.UpdateRoomAsync(model);
            var response = new CommonResponse()
            {
                Message = "Room updated successfully",
                Result = result,
                IsSuccess = true,
                HttpStatusCode = Convert.ToInt32(HttpStatusCode.OK)
            };
            return StatusCode(response.HttpStatusCode, response);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRoom(int id)
        {
            await roomService.DeleteRoomAsync(id);
            var response = new CommonResponse()
            {
                Message = "Room deleted successfully",
                IsSuccess = true,
                HttpStatusCode = Convert.ToInt32(HttpStatusCode.OK)
            };
            return StatusCode(response.HttpStatusCode, response);


        }
    }
}
