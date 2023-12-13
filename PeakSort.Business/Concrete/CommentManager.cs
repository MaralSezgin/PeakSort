using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PeakSort.Core.Utilities.Results.Abstract;
using PeakSort.Core.Utilities.Results.ComplexTypes;
using PeakSort.Core.Utilities.Results.Concrete;
using PeakSort.DataAccess.Abstract;
using ProgrammersBlog.Services.Abstract;


namespace ProgrammersBlog.Services.Concrete
{
    public class CommentManager:ICommentService
    {
        private readonly IUnitOfWork _unitOfWork;

        public CommentManager(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IDataResult<int>> CountAsync()
        {
            var commentsCount = await _unitOfWork.Comments.CountAsync();
            if (commentsCount > -1)
            {
                return new DataResult<int>(ResultStatus.Success, commentsCount);
            }
            else
            {
                return new DataResult<int>(ResultStatus.Error, $"Beklenmeyen bir hata ile karşılaşıldı.", -1);
            }
        }

        public async Task<IDataResult<int>> CountByNonDeletedAsync()
        {
            var commentsCount = await _unitOfWork.Comments.CountAsync(c=>!c.IsDeleted);
            if (commentsCount > -1)
            {
                return new DataResult<int>(ResultStatus.Success, commentsCount);
            }
            else
            {
                return new DataResult<int>(ResultStatus.Error, $"Beklenmeyen bir hata ile karşılaşıldı.", -1);
            }
        }
    }
}
