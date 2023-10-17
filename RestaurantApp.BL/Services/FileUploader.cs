using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantApp.BL.Services
{
    public class FileUploader
    {
        public static string upload(string foldername,IFormFile file)
        {
            try
            {
                var path = Directory.GetCurrentDirectory() + "/wwwroot/" + foldername;
                string filename = Guid.NewGuid() + Path.GetFileName(file.FileName);
                var finalpath = Path.Combine(path, filename);
                using (var stream = new FileStream(finalpath, FileMode.Create))
                {
                    file.CopyTo(stream);
                }
                return filename;
            }catch(Exception ex)
            {
                return ex.Message;
            }
        

        }
        public static void delete(string foldername, string filename)
        {
            try
            {
                var path = Directory.GetCurrentDirectory() + "/wwwroot/" + foldername + filename;
                if (File.Exists(path))
                {
                    File.Delete(path);
                }

            }
            catch (Exception e)
            {

            }
        }
    }
}
