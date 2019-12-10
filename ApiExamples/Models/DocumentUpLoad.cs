using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiExamples.Models
{
    public class DocumentUpLoad
    {
        public int UserId { get; set; }
        public IFormFile Document { get; set; }

    }
}
