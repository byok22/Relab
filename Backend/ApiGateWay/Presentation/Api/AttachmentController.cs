using Domain.Models;
using Domain.Services;
using Microsoft.AspNetCore.Mvc;

namespace ApiGateWay.Presentation.Api
{
    [ApiController]
    [Route("api/[controller]")]
    public class AttachmentController : ControllerBase
    {

        private readonly IFDataService attachmentService;

        public AttachmentController(IFDataService attachmentService)
        {
            this.attachmentService = attachmentService;
            
        }

       [HttpPost]
        [Route("uploadfile")]
        public async Task<IActionResult> UploadFile([FromForm] IFormFile file)
        {
        

            //convert file to byte array
            byte[] fileBytes;
            using (var ms = new MemoryStream())
            {
                file.CopyTo(ms);
                fileBytes = ms.ToArray();
            }
            Attachment attachment = new Attachment();
            attachment.File = fileBytes;
            attachment.Name = file.FileName;
                      
         var attachmentObject = await attachmentService.Save(attachment);
          return Ok(attachmentObject);
           
        }
 
    }
}