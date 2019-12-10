using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ApiExamples.Models;
using System.IO;
using System.Net.Http;
using static System.Net.Mime.MediaTypeNames;
using System.Net.Http.Headers;
using System.Net;

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

        [HttpGet("{id}")]
        public async Task<HttpResponseMessage> GetDocumentAsImage(int id)
        {
            var document = await _context.Documents.FindAsync(id);

            HttpResponseMessage result = new HttpResponseMessage();

            if (document != null)
            {
                var stream = new MemoryStream(document.Document);
                stream.Position = 0;
            
                result.Content = new StreamContent(stream);
                result.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment");
                result.Content.Headers.ContentDisposition.FileName = "Document.jpg";
                result.Content.Headers.ContentType = new MediaTypeHeaderValue("image/jpeg");
                result.Content.Headers.ContentLength = stream.Length;

                return result;
            }


             return new HttpResponseMessage(HttpStatusCode.NotFound);
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

            return Ok();
        }


        private bool DocumentsExists(int id)
        {
            return _context.Documents.Any(e => e.Id == id);
        }
    }
}
