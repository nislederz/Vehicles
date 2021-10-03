﻿using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Vehicles.API.Data;
using Vehicles.API.Data.Entities;

namespace Vehicles.API.Controllers.API
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ApiController]
    [Route("api/[controller]")]
    public class VehicleTypesController : ControllerBase
    {
        private readonly DataContext _context;

        public VehicleTypesController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<VehicleType>>> GetVehicleTypes()
        {
            return await _context.VehicleTypes.OrderBy(x => x.Description).ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<VehicleType>> GetVehicleType(int id)
        {
            var vehicleType = await _context.VehicleTypes.FindAsync(id);

            if (vehicleType == null)
            {
                return NotFound();
            }

            return vehicleType;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutVehicleType(int id, VehicleType vehicleType)
        {
            if (id != vehicleType.Id)
            {
                return BadRequest();
            }

            _context.Entry(vehicleType).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
                return NoContent();
            }
            catch (DbUpdateException DbUpdateException)
            {
                if (DbUpdateException.InnerException.Message.Contains("duplicate"))
                {
                    return BadRequest("Ya existe esta tipo de vehiculo.");
                }
                else
                {
                    return BadRequest(DbUpdateException.InnerException.Message);
                }
            }
            catch (Exception exception)
            {
                return BadRequest(exception.Message);
            }

        }

        [HttpPost]
        public async Task<ActionResult<VehicleType>> PostVehicleType(VehicleType vehicleType)
        {
            _context.VehicleTypes.Add(vehicleType);
            
            try
            {
                await _context.SaveChangesAsync();
                return CreatedAtAction("GetVehicleType", new { id = vehicleType.Id }, vehicleType);
            }
            catch (DbUpdateException DbUpdateException)
            {
                if (DbUpdateException.InnerException.Message.Contains("duplicate"))
                {
                    return BadRequest("Ya existe esta tipo de vehiculo.");
                }
                else
                {
                    return BadRequest(DbUpdateException.InnerException.Message);
                }
            }
            catch (Exception exception)
            {
                return BadRequest(exception.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteVehicleType(int id)
        {
            var vehicleType = await _context.VehicleTypes.FindAsync(id);
            if (vehicleType == null)
            {
                return NotFound();
            }

            _context.VehicleTypes.Remove(vehicleType);
            await _context.SaveChangesAsync();

            return NoContent();
        }

    }
}
