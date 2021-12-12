using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using DMSapi.Models;
using DMSapi.Services;
using System.Net.Http.Headers;
using System.IO;
using System.Linq;

namespace DMSapi.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]

    public class RoomController : ControllerBase
    {
        private readonly RoomService _roomService;
        public RoomController(RoomService roomService)
        {
            _roomService = roomService;
        }

        [HttpGet]
        public ActionResult<List<Room>> GetAllRoom() => _roomService.GetRoomAll();

        [HttpGet("{id}")]
        public ActionResult<Room> GetRoomById(string id)
        {
            var room = _roomService.GetRoomById(id);
            if(room == null){
                return NotFound();
            }
            return room;
        }

        [HttpPost]
        public Room CreateRoom([FromBody] Room room)
        {
            var data = _roomService.GetRoomAlls();
            var count = data.Count();
            var id = "R00" + count.ToString();
            room.RoomId = id;
            room.Status = "Open";
            _roomService.CreateRoom(room);
            return room;
        }

        [HttpPut("{id}")]
        public IActionResult EditRoom([FromBody] Room room, string id)
        {
            var rooms = _roomService.GetRoomById(id);
            if(rooms == null){
                return NotFound();
            }
            rooms.RoomId = id;
            _roomService.EditRoom(id,room);
            return NoContent();
        }

        [HttpGet("{id}")]
        public IActionResult DeleteRoom(string id)
        {
            var room = _roomService.GetRoomById(id);
            var statusChange = room.Status;
            if( room == null){
                return NotFound();
            }
            if(statusChange == "Open"){
                 statusChange = "Close";
            }
            room.Status = statusChange;
            _roomService.DeleteRoom(id, room);
            return NoContent();
        }
        
    }
}