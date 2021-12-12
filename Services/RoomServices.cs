using MongoDB.Driver;
using System.Collections.Generic;
using System;
using System.Linq;
using DMSapi.Models;

namespace DMSapi.Services
{
    public class RoomService
    {
        private readonly IMongoCollection<Room> _Room;
        public RoomService(DatabaseSetting settings)
        {
            var Client = new MongoClient(settings.ConnectionString);
            var database = Client.GetDatabase(settings.DatabaseName);
            var filter = Builders<Room>.Filter.Where(room => room.Status == "Open");
            _Room = database.GetCollection<Room>(settings.RoomCollection);
        }

       public List<Room> GetRoomAll() => _Room.Find(room => room.Status == "Open").ToList();
       public List<Room> GetRoomAlls() => _Room.Find(room => true).ToList();
       public Room GetRoomById(string id) => _Room.Find<Room>(room => room.RoomId == id).FirstOrDefault();
        
       public Room CreateRoom(Room room){
           _Room.InsertOne(room);
           return room;
       } 
       public void EditRoom(string id, Room roomIn) => _Room.ReplaceOne(room => room.RoomId == id, roomIn);
       public string DeleteRoom(string id, Room roomIn) {
            _Room.ReplaceOne(room => room.RoomId == id, roomIn);
            return "Delete Succeess";
            }
    }
}