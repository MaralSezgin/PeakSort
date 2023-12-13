using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using PeakSort.Core.Utilities.Results.Abstract;
using PeakSort.Entities.Dtos;

namespace PeakSort.MVCWebUI.Helpers.Abstract
{
    public interface IImageHelper
    {
        Task<IDataResult<ImageUploadedDto>> UploadUserImage(string userName, IFormFile pictureFile, string folderName = "userImages");
        IDataResult<ImageDeletedDto> Delete(string pictureName);
    }
}
