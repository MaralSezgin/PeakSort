﻿using PeakSort.Core.Utilities.Results.Abstract;
using PeakSort.Entities.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace PeakSort.Business.Abstract
{
    public interface IArticleService
    {
        Task<IDataResult<ArticleDto>> GetAsync(int articleId);
        Task<IDataResult<ArticleListDto>> GetAllAsync();
        Task<IDataResult<ArticleListDto>> GetAllByNonDeletedAsync();
        Task<IDataResult<ArticleListDto>> GetAllByNonDeletedAndActiveAsync();
        Task<IDataResult<ArticleListDto>> GetAllByCategoryAsync(int categoryId);
        Task<IResult> AddAsync(ArticleAddDto articleAddDto, string createdByName);
        Task<IResult> UpdateAsync(ArticleUpdateDto articleUpdateDto, string modifiedByName);
        Task<IResult> DeleteAsync(int articleId, string modifiedByName);
        Task<IResult> HardDeleteAsync(int articleId);
        Task<IDataResult<int>> CountAsync();
        Task<IDataResult<int>> CountByNonDeletedAsync();
    }
}
