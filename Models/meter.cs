using System;
using MongoDB.Bson.Serialization.Attributes;

namespace DMSapi.Models
{
    public class Meter
    {
        [BsonId]

        public string MeterId { get; set; }
        public int MeterWater { get; set; } // เลขมิเตอร์น้ำปัจจุบัน
        public int MeterPower { get; set; } // เลขมิเตอร์ไฟปัจจุบัน
        public int MeterPreviousWater { get; set; } //เลขมิเตอร์น้ำก่อนหน้า
        public int MeterPreviousPower { get; set; } //เลขมิเตอร์ไฟก่อนหน้า
        public int MeterUnitWater { get; set; } // จำนวนหน่วยเลขมิเตอร์น้ำ
        public int MeterUnitPower { get; set; } // จำนวนหน่วยเลขมิเตอร์ไฟ
        public string MeterStatus { get; set; } // จำนวนหน่วยเลขมิเตอร์ที่ใช้จริง
        
        public DateTime? SelectMonth { get; set; } // เลือกเดือน
        public string RoomId { get; set; }
        public string Status { get; set; }

    }
}