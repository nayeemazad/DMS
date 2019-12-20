using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Net.Http.Headers;
using System.IO;
namespace DMS.Data
{
   
        public static class IFormFileExtensions
        {
            public static string GetFilename(this IFormFile file)
            {
                return ContentDispositionHeaderValue.Parse(
                                file.ContentDisposition).FileName.ToString().Trim('"');
            }

            public static async Task<MemoryStream> GetFileStream(this IFormFile file)
            {
                MemoryStream filestream = new MemoryStream();
                await file.CopyToAsync(filestream);
                return filestream;
            }

            public static async Task<byte[]> GetFileArray(this IFormFile file)
            {
                MemoryStream filestream = new MemoryStream();
                await file.CopyToAsync(filestream);
                return filestream.ToArray();
            }
        }
    }
