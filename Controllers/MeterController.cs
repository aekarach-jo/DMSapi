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

    public class MeterController : ControllerBase
    {
        private readonly MeterService _meterService;
        public MeterController(MeterService meterService)
        {
            _meterService = meterService;
        }

        [HttpGet]
        public ActionResult<List<Meter>> GetAllMeter() => _meterService.GetMeterAll();

        [HttpGet("{id}")]
        public ActionResult<Meter> GetMeterById(string id)
        {
            var meter = _meterService.GetMeterById(id);
            if(meter == null){
                return NotFound();
            }
            return meter;
        }

        [HttpPost]
        public Meter CreateMeter([FromBody] Meter meter)
        {
            var data = _meterService.GetMeterAlls();
            var count = data.Count();
            var id = "M00" + count.ToString();
            meter.MeterId = id;
            meter.Status = "Open";
            meter.MeterStatus = "unrecorded";
            meter.SelectMonth = DateTime.UtcNow;
            _meterService.CreateMeter(meter);
            return meter;
        }

        [HttpPut("{id}")]
        public IActionResult EditMeter([FromBody] Meter meter, string id)
        {
            var meters = _meterService.GetMeterById(id);
            if(meters == null){
                return NotFound();
            }
            meters.MeterId = id;
            meters.MeterStatus = "recorded";
            _meterService.EditMeter(id,meter);
            return NoContent();
        }

        [HttpGet("{id}")]
        public IActionResult DeleteMeter(string id)
        {
            var meter = _meterService.GetMeterById(id);
            var statusChange = meter.Status;
            if( meter == null){
                return NotFound();
            }
            if(statusChange == "Open"){
                 statusChange = "Close";
            }
            meter.Status = statusChange;
            _meterService.DeleteMeter(id, meter);
            return NoContent();
        }
        
    }
}