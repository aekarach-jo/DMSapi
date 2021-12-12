using MongoDB.Driver;
using System.Collections.Generic;
using System;
using System.Linq;
using DMSapi.Models;

namespace DMSapi.Services
{
    public class MeterService
    {
        private readonly IMongoCollection<Meter> _Meter;
        public MeterService(DatabaseSetting settings)
        {
            var Client = new MongoClient(settings.ConnectionString);
            var database = Client.GetDatabase(settings.DatabaseName);
            var filter = Builders<Meter>.Filter.Where(meter => meter.Status == "Open");
            _Meter = database.GetCollection<Meter>(settings.MeterCollection);
        }

       public List<Meter> GetMeterAll() => _Meter.Find(meter => meter.Status == "Open").ToList();
       public List<Meter> GetMeterAlls() => _Meter.Find(meter => true).ToList();
       public Meter GetMeterById(string id) => _Meter.Find<Meter>(meter => meter.MeterId == id).FirstOrDefault();
        
       public Meter CreateMeter(Meter meter){
           _Meter.InsertOne(meter);
           return meter;
       } 
       public void EditMeter(string id, Meter meterIn) => _Meter.ReplaceOne(meter => meter.MeterId == id, meterIn);
       public string DeleteMeter(string id, Meter meterIn) {
            _Meter.ReplaceOne(meter => meter.MeterId == id, meterIn);
            return "Delete Succeess";
            }
    }
}