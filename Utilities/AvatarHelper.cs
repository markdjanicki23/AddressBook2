using AddressBook2.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace AddressBook2.Utilities
{
    public class AvatarHelper
    {
        public static string GetImage(AddressBook addressBook)
        {
            var binary = Convert.ToBase64String(addressBook.ImageData);
            var ext = Path.GetExtension(addressBook.ImagePath);
            string imageData = $"data:image/{ext};base64,{binary}";
            return imageData;
        }

        public static byte[] PutImage(IFormFile image)
        {
            // this turns our image into a storable form
            var ms = new MemoryStream();
            image.CopyTo(ms);
            var output = ms.ToArray();

            ms.Close();
            ms.Dispose();
            return output;
        }
    }
}
