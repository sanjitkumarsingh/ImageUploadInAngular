using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    public class ImageController : ApiController
    {

        [HttpPost]
        [Route("api/UploadImage")]
        public HttpResponseMessage UploadImage()
        {
            string imageName = null;
            var httpRequest = HttpContext.Current.Request;
            //Upload Image
            var postedFile = httpRequest.Files["Image"];
            //Create custom filename
            imageName = new String(Path.GetFileNameWithoutExtension(postedFile.FileName).Take(10).ToArray()).Replace(" ", "-");
            imageName = imageName + DateTime.Now.ToString("yymmssfff") + Path.GetExtension(postedFile.FileName);
            var filePath = HttpContext.Current.Server.MapPath("~/Image/" + imageName);
            postedFile.SaveAs(filePath);
            System.Drawing.Image img =  System.Drawing.Image.FromFile(filePath);
            //Save to DB
            using (DBModel db = new DBModel())
            {
                Image image = new Image()
                {
                    ImageCaption = httpRequest["ImageCaption"],
                    USerName = httpRequest["ImageCaption2"],
                    ImageName = imageName,
                    Content = ImageToByteArray(img)
                };
                db.Images.Add(image);
                db.SaveChanges();
            }
            return Request.CreateResponse(HttpStatusCode.Created);
        }


        public byte[] ImageToByteArray(System.Drawing.Image imageIn)
        {
            MemoryStream ms = new MemoryStream();
            imageIn.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
            return ms.ToArray();
        }

        public System.Drawing.Image byteArrayToImage(byte[] byteArrayIn)
        {
            MemoryStream ms = new MemoryStream(byteArrayIn);
            System.Drawing.Image returnImage = System.Drawing.Image.FromStream(ms);
            return returnImage;
        }
    }
}
