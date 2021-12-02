using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Web;

namespace GameHeavenAPI.Controllers
{
    public class FileController : Controller
    {

        [HttpGet]
        [Route("GetImage/{path}")]
        public IActionResult GetImage(string path)
        {
            var decodedPath = HttpUtility.UrlDecode(path);
            try
            {
                byte[] b = System.IO.File.ReadAllBytes(decodedPath);
                return File(b, "image/*");
            }
            catch (Exception ex)
            {
                return BadRequest(new Response
                {
                    Success = false,
                    Errors = new()
                    {
                        "Image not found"
                    }
                });
            }
        }
        [HttpGet]
        [Route("GetImages/{path}")]
        public IList<byte[]> GetImages(string path)
        {
            var decodedPath = HttpUtility.UrlDecode(path);
            string[] files =
                    Directory.GetFiles(decodedPath);
            try
            {
                List<byte[]> images = new();
                foreach (string file in files)
                {
                    string fileName = Path.GetFileName(file);

                    byte[] b = System.IO.File.ReadAllBytes(decodedPath + "/" + fileName);
                    images.Add(b);
                }
                return images;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        [HttpGet]
        [Route("GetVideo/{path}")]
        public IActionResult GetVideo(string path)
        {
            var decodedPath = HttpUtility.UrlDecode(path);
            try
            {
                byte[] b = System.IO.File.ReadAllBytes(decodedPath);
                return File(b, "video/*");
            }
            catch (Exception ex)
            {
                return BadRequest(new Response
                {
                    Success = false,
                    Errors = new()
                    {
                        "Video not found"
                    }
                });
            }
        }
    }
}
