using HMS.Application.Contracts.Services;
using HMS.Application.Models.Common;
using HMS.Application.Models.Hotel;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace HMS.Controllers
{
    [Route("api/hotel")]
    [ApiController]
    public class HotelController(IHotelService hotelService) : ControllerBase
    {

        [HttpGet]
        public async Task<IActionResult> GetHotels([FromQuery] PagedRequestDto parameters)
        {
            var result = await hotelService.GetAllHotelsAsync(parameters);

            var response = new CommonResponse()
            {
                Message = "Hotels retrieved successfully",
                Result = result,
                IsSuccess = true,
                HttpStatusCode = Convert.ToInt32(HttpStatusCode.OK)
            };

            return StatusCode(response.HttpStatusCode, response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetHotelById(int id)
        {
            var result = await hotelService.GetHotelByIdAsync(id);
            var response = new CommonResponse()
            {
                Message = "Hotel retrieved successfully",
                Result = result,
                IsSuccess = true,
                HttpStatusCode = Convert.ToInt32(HttpStatusCode.OK)
            };
            return StatusCode(response.HttpStatusCode, response);
        }

        [HttpPost]
        public async Task<IActionResult> CreateHotel([FromBody] HotelForCreatingDto model)
        {
            var result = await hotelService.CreateHotelsAsync(model);
            var response = new CommonResponse()
            {
                Message = "Hotel created successfully",
                Result = result,
                IsSuccess = true,
                HttpStatusCode = Convert.ToInt32(HttpStatusCode.Created)
            };
            return StatusCode(response.HttpStatusCode, response);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateHotel([FromBody] HotelForUpdatingDto model)
        {
            var result = await hotelService.UpdateHotelAsync(model);
            var response = new CommonResponse()
            {
                Message = "Hotel updated successfully",
                Result = result,
                IsSuccess = true,
                HttpStatusCode = Convert.ToInt32(HttpStatusCode.OK)
            };
            return StatusCode(response.HttpStatusCode, response);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteHotel(int id)
        {
            await hotelService.DeleteHotelAsync(id);

            var response = new CommonResponse()
            {
                Message = "Hotel deleted successfully",
                IsSuccess = true,
                HttpStatusCode = Convert.ToInt32(HttpStatusCode.OK)
            };
            return StatusCode(response.HttpStatusCode, response);
        }

    }
}
