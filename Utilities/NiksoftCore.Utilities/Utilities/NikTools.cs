﻿using NiksoftCore.ViewModel;
using System.IO;
using System.Threading.Tasks;

namespace NiksoftCore.Utilities
{
    public static class NikTools
    {
        public static async Task<SaveFileResponse> SaveFileAsync(SaveFileRequest request)
        {
            var result = new SaveFileResponse();
            if (request.File == null)
            {
                result.Success = false;
                result.Message = "File request is empty";
                return result;
            }

            if (string.IsNullOrWhiteSpace(request.RootPath))
            {
                result.Success = false;
                result.Message = "Root Path is empty";
                return result;
            }

            if (request.File.Length > 0)
            {
                var fileName = Path.GetFileName(request.File.FileName);
                var folderPath = request.RootPath + "/" + fileName;

                using (var stream = System.IO.File.Create(folderPath))
                {
                    await request.File.CopyToAsync(stream);
                    result.Path = folderPath;
                }
            }

            result.Success = true;
            result.Message = "Save Successed";

            return result;
        }
    }
}
