using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ApiExamples.Models;
using System.IO;

namespace ApiExamples.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DocumentsController : ControllerBase
    {
        private readonly ApiExamplesContext _context;

        public DocumentsController(ApiExamplesContext context)
        {
            _context = context;
        }

        // GET: api/Documents
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Documents>>> GetDocuments()
        {
            return await _context.Documents.ToListAsync();
        }

        // GET: api/Documents/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Documents>> GetDocuments(int id)
        {
            var documents = await _context.Documents.FindAsync(id);

            if (documents == null)
            {
                return NotFound();
            }

            return documents;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromForm] DocumentUpLoad documentUpload)
        {
            // Find User

           


            if (documentUpload != null)
            {
                var document = documentUpload.Document;

                if (document != null)
                {
                    var user =  _context.Users.Where(x => x.Id == documentUpload.UserId).Include(user => user.Profile).ThenInclude(profile => profile.Documents).FirstOrDefault();


                    if (user != null)
                    {
                        using (var binaryReader = new BinaryReader(document.OpenReadStream()))
                        {
                            var imageData = binaryReader.ReadBytes((int)document.Length);

                            var documentData = new Documents();
                            documentData.Document = imageData;

                            user.Profile.Documents.Add(documentData);

                            _context.SaveChanges();
                        }
                    }
                }


            }


            /* var data = vm.Data;

             if (vm.Images != null)
             {
                 foreach (var image in vm.Images)
                 {
                     byte[] fileData = null;

                     // read file to byte array
                     using (var binaryReader = new BinaryReader(image.OpenReadStream()))
                     {
                         fileData = binaryReader.ReadBytes((int)image.Length);
                     }
                 }
             }*/



            return Ok();
        }


        private bool DocumentsExists(int id)
        {
            return _context.Documents.Any(e => e.Id == id);
        }
    }
}
