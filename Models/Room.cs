using System;
using MongoDB.Bson.Serialization.Attributes;


namespace DMSapi.Models
{
    public class Room
    {
        [BsonId]
        public string RoomId { get; set; }
        public string RoomNumber { get; set; }
        public string RoomStatus { get; set; }
        public string RoomFloorNumber { get; set; }
        public string UserId { get; set; }
        public string MeterId { get; set; }
        public string Status { get; set; }
    }
}