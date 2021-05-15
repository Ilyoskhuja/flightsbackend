using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using flights.Models;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Authorization;

namespace flights.Controllers
{
    [ApiController]
    [Route("api/Flights")]
    public class FlightsController : ControllerBase
    {

        public FlightsController()
        {
        }


        // POST: api/Flights
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754

     
       [HttpPost]
        [Route("Measure")]
        public  IActionResult Measure([FromForm] Flight model)
        { 
            try
            {
                
                const double EquatorialRadiusOfEarth = 6371D;
                const double DegreesToRadians = (Math.PI / 180D);

                var deltalat = (model.latTo - model.latFrom) * DegreesToRadians;
                var deltalong = (model.longTo - model.longFrom) * DegreesToRadians;
                var a = Math.Pow(
                    Math.Sin(deltalat / 2D), 2D) +
                    Math.Cos(model.latFrom * DegreesToRadians) *
                    Math.Cos(model.latTo * DegreesToRadians) *
                    Math.Pow(Math.Sin(deltalong / 2D), 2D);
                var c = 2D * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1D - a));

                var d = EquatorialRadiusOfEarth * c;
                return Ok(d);

            }
            catch (Exception ex)
            {
                return BadRequest($"Error occured while creating entity. Error: {ex.Message}. StackTrace: {ex.StackTrace}");
            }
        }

    }
}
