using EcommerceLiveEfCore.Services;
using HotelGestionale.Data;
using HotelGestionale.Models;
using HotelGestionale.ViewModels;
using Microsoft.EntityFrameworkCore;
using System;

namespace HotelGestionale.Services
{
    public class RoomService
    {
        private readonly ApplicationDbContext _context;
        private readonly LoggerService _loggerService;

        public RoomService(ApplicationDbContext context, LoggerService loggerService)
        {
            this._context = context;
            this._loggerService = loggerService;
        }

        private async Task<bool> SaveAsync()
        {
            try
            {
                var rowsAffected = await _context.SaveChangesAsync();
                return rowsAffected > 0;
            }
            catch (Exception ex)
            {
                _loggerService.LogError(ex.Message);
                return false;
            }
        }

        public async Task<List<Rooms>> GetAllRoomsAsync()
        {
            try
            {
                var rooms = await _context.Camere.ToListAsync();
                _loggerService.LogInformation("Rooms list requested by admin");
                return rooms;
            }
            catch (Exception ex)
            {
                _loggerService.LogError(ex.Message);
                return new List<Rooms>();
            }
        }

        public async Task<bool> AddRoomAsync(AddRoomViewModel addRoomViewModel)
        {
            try
            {
                var room = new Rooms()
                {
                    CameraId = new Random().Next(1, 1000), 
                    Numero = addRoomViewModel.Numero,
                    Tipo = addRoomViewModel.Tipo,
                    Prezzo = addRoomViewModel.Prezzo
                };

                _context.Camere.Add(room);

                return await SaveAsync();
            }
            catch (Exception ex)
            {
                _loggerService.LogError(ex.Message);
                return false;
            }
        }

        public async Task<Rooms?> GetRoomByIdAsync(Guid id)
        {
            try
            {
                var room = await _context.Camere.FindAsync(id);
                return room ?? null;
            }
            catch (Exception ex)
            {
                _loggerService.LogError(ex.Message);
                return null;
            }
        }

        public async Task<bool> DeleteRoomByIdAsync(Guid id)
        {
            try
            {
                var room = await _context.Camere.FindAsync(id);

                if (room == null)
                {
                    _loggerService.LogWarning($"Room not found");
                    return false;
                }

                _context.Camere.Remove(room);
                return await SaveAsync();
            }
            catch (Exception ex)
            {
                _loggerService.LogError(ex.Message);
                return false;
            }
        }

        public async Task<bool> UpdateRoomAsync(EditRoomViewModel editRoomViewModel)
        {
            try
            {
                var room = await _context.Camere.FindAsync(editRoomViewModel.Id);

                if (room == null)
                {
                    return false;
                }

                room.Numero = editRoomViewModel.Numero;
                room.Tipo = editRoomViewModel.Tipo;
                room.Prezzo = editRoomViewModel.Prezzo;

                return await SaveAsync();
            }
            catch (Exception ex)
            {
                _loggerService.LogError(ex.Message);
                return false;
            }
        }
    }
}
