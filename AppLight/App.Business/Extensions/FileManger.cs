using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace App.Business.Extensions
{
    public static class FileManger
    {
        public static string Savefile(this IFormFile file,string rootPath,string folderName)
        {
            string filename = file.FileName.Length > 64 ?
                file.FileName.Substring(file.FileName.Length - 64, 64): file.FileName;
            filename=Guid.NewGuid().ToString()+file.FileName;
            string path=Path.Combine(rootPath,folderName,filename);
            using (FileStream stream=new FileStream(path,FileMode.Create))
            {
                file.CopyTo(stream);
            }
            return filename;

        }
        public static void DeleteFile(string rootPath,string folderName, string ImageUrl)
        {
            string deletePath=Path.Combine(rootPath,folderName,ImageUrl);
            if (File.Exists(deletePath))
            {
                File.Delete(deletePath);
            }
        }
    }
}
