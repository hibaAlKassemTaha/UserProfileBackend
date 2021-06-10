using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net.Http.Headers;
using UserProfile.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace UserProfile.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UploadProfileImageController : ControllerBase
    {
        private IUserProfileService _userService;

        // GET: api/<UploadProfileImageController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<UploadProfileImageController>/5
     
      
        // POST api/<UploadProfileImageController>
        [HttpPost, DisableRequestSizeLimit]
        public IActionResult Upload()
        {
            try
            {
                var file = Request.Form.Files[0];
                var folderName = Path.Combine("Resources", "Images");
                var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), folderName);
                if (file.Length > 0)
                {
                    var fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
                    var fullPath = Path.Combine(pathToSave, fileName);
                    var dbPath = Path.Combine(folderName, fileName);
                    using (var stream = new FileStream(fullPath, FileMode.Create))
                    {
                        file.CopyTo(stream);
                    }
                    return Ok(new { fullPath });
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex}");
            }
        }

        // PUT api/<UploadProfileImageController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<UploadProfileImageController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
