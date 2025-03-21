using Microsoft.AspNetCore.Mvc;
using HotelGestionale.Services;
using HotelGestionale.ViewModels;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Threading.Tasks;
using EcommerceLiveEfCore.Services;

namespace HotelGestionale.Controllers
{
    [Authorize(Roles = "Admin")]
    public class RoomController : Controller
    {
        private readonly RoomService _roomService;
        private readonly LoggerService _loggerService;

        public RoomController(RoomService roomService, LoggerService loggerService)
        {
            _roomService = roomService;
            _loggerService = loggerService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet("room/get-all")]
        public async Task<IActionResult> ListRooms()
        {
            var roomsList = await _roomService.GetAllRoomsAsync();
            return PartialView("_RoomsList", roomsList);
        }

        public IActionResult Add()
        {
            return PartialView("_AddForm");
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddRoomViewModel addRoomViewModel)
        {
            if (!ModelState.IsValid)
            {
                return Json(new { success = false, message = "Error while saving room to database" });
            }

            var result = await _roomService.AddRoomAsync(addRoomViewModel);
            if (!result)
            {
                return Json(new { success = false, message = "Error while saving room to database" });
            }

            string logmessage = "Room saved successfully to database";
            _loggerService.LogInformation(logmessage);
            return Json(new { success = true, message = logmessage });
        }

        [HttpPost]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await _roomService.DeleteRoomByIdAsync(id);
            if (!result)
            {
                return Json(new { success = false, message = "Error while deleting room" });
            }

            string logmessage = "Room deleted successfully";
            _loggerService.LogInformation(logmessage);
            return Json(new { success = true, message = logmessage });
        }

        public async Task<IActionResult> Edit(Guid id)
        {
            var room = await _roomService.GetRoomByIdAsync(id);
            if (room == null)
            {
                return RedirectToAction("Index");
            }

            var editRoomViewModel = new EditRoomViewModel()
            {
                Id = id,
                Numero = room.Numero,
                Tipo = room.Tipo,
                Prezzo = room.Prezzo
            };

            return PartialView("_EditForm", editRoomViewModel);
        }

        [HttpPost("room/edit/save")]
        public async Task<IActionResult> Edit(EditRoomViewModel editRoomViewModel)
        {
            var result = await _roomService.UpdateRoomAsync(editRoomViewModel);
            if (!result)
            {
                return Json(new { success = false, message = "Error while updating room in database" });
            }

            string logmessage = "Room updated successfully";
            _loggerService.LogInformation(logmessage);
            return Json(new { success = true, message = logmessage });
        }
    }
}
