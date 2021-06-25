using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Doit.Helper
{
    public class FileHelper
    {
        private readonly IWebHostEnvironment environment;

        public FileHelper(IWebHostEnvironment environment)
        {
            this.environment = environment;
        }

       public string UploadFile(IFormFile file)
       {
            var uploadFolderPath = Path.Combine(environment.WebRootPath, "Uploads");
            if (!Directory.Exists(uploadFolderPath))
                Directory.CreateDirectory(uploadFolderPath);

            var filename = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
            var filePath = Path.Combine(uploadFolderPath, filename);
            
            using(var stream = new FileStream(filePath, FileMode.Create))
            {
                file.CopyTo(stream);
            }
            return filename;
       }


    }
}
