using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.AspNetCore.Hosting;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Snai3i_DAL.Data.service.FileService
{
    public class FileService : Ifileservice
    {
        private readonly IWebHostEnvironment environment;

        public FileService(IWebHostEnvironment env)
        {
            environment = env;
        }

        public Tuple<int, string> SaveImage(IFormFile Imagefile)
        {
            try
            {
                var contentpath = environment.ContentRootPath;
                var path = Path.Combine(contentpath, "Uploads");

                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }

                var ext = Path.GetExtension(Imagefile.FileName);
                var allowedExtension = new string[] { ".jpg", ".png", ".jpeg" , ".pdf" ,
                ".doc" , ".docs" , ".xls" };
                if (!allowedExtension.Contains(ext))
                {
                    string msg = string.Format($" only {allowedExtension} are allowed");
                    return new Tuple<int, string>(0, msg);
                }

                string uniqueString = Guid.NewGuid().ToString();

                var newfilename = uniqueString + ext;
                var filewithpath = Path.Combine(path, newfilename);
                var stream = new FileStream(filewithpath, FileMode.Create);
                Imagefile.CopyTo(stream);
                stream.Close();
                return new Tuple<int, string>(1, newfilename);

            }
            catch (Exception ex)
            {
                return new Tuple<int, string>(0, "Error has occured");
            }
        }
        public bool DeleteImage(string productImage)
        {
            try
            {
                var wwwpath = environment.WebRootPath;

                var path = Path.Combine(wwwpath, "Uploads", productImage);

                if (File.Exists(path))
                {
                    File.Delete(path);
                    return true;

                }
                return false;

            }
            catch (Exception ex)
            {
                return false;
            }


        }
    }
}
