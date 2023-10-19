﻿using PeakSort.Core.Utilities.Results.Abstract;
using PeakSort.Core.Utilities.Results.Concrete;
using PeakSort.Entities.Concrete;
using PeakSort.Entities.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PeakSort.Business.Abstract
{
    public interface ICategoryService
    {
        public Task<DataResult<CategoryDto>> Get(int categoryId);
        public Task<DataResult<CategoryUpdateDto>> GetCategoryUpdateDto(int categoryId);
        public Task<DataResult<CategoryListDto>> GelAll();

        public Task<DataResult<CategoryDto>> Add(CategoryAddDto categoryAddDto, string createdByName);
        public Task<DataResult<CategoryDto>> Update(CategoryUpdateDto categoryUpdateDto, string modifiedByName);
        public Task<IResult> Delete(int categoryId);
        public Task<IResult> HardDelete(int categoryId);
    }
}
