using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace DMSapi.Models
{
    public class User
    {
        [BsonId]
        public string UserId { get; set; }
        public string UserName { get; set; }
        public int UserAge { get; set; }
        public int UserIdCard { get; set; }
        public string UserRoomNumber { get; set; }
        public string UserTel { get; set; }
        public string UserAddress { get; set; }
        public string UserEmail { get; set; }
        public string Status { get; set; }
        public int UserFloorNumber { get; set; }
        public string RoomId { get; set; }
    }
}