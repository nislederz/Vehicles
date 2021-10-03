using Microsoft.AspNetCore.Authentication.JwtBearer;
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
    public class DocumentTypesController : ControllerBase
    {
        private readonly DataContext _context;

        public DocumentTypesController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<DocumentType>>> GetDocumentTypes()
        {
            return await _context.DocumentTypes.OrderBy(x => x.Description).ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<DocumentType>> GetDocumentType(int id)
        {
            var documentType = await _context.DocumentTypes.FindAsync(id);

            if (documentType == null)
            {
                return NotFound();
            }

            return documentType;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutDocumentType(int id, DocumentType documentType)
        {
            if (id != documentType.Id)
            {
                return BadRequest();
            }

            _context.Entry(documentType).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
                return NoContent();
            }
            catch (DbUpdateException DbUpdateException)
            {
                if (DbUpdateException.InnerException.Message.Contains("duplicate"))
                {
                    return BadRequest("Ya existe esta tipo de documento.");
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
        public async Task<ActionResult<DocumentType>> PostDocumentType(DocumentType documentType)
        {
            _context.DocumentTypes.Add(documentType);
            try
            {
                await _context.SaveChangesAsync();
                return CreatedAtAction("GetDocumentType", new { id = documentType.Id }, documentType);
            }
            catch (DbUpdateException DbUpdateException)
            {
                if (DbUpdateException.InnerException.Message.Contains("duplicate"))
                {
                    return BadRequest("Ya existe esta tipo de documento.");
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
        public async Task<IActionResult> DeleteDocumentType(int id)
        {
            var documentType = await _context.DocumentTypes.FindAsync(id);
            if (documentType == null)
            {
                return NotFound();
            }

            _context.DocumentTypes.Remove(documentType);
            await _context.SaveChangesAsync();

            return NoContent();
        }

    }
}
