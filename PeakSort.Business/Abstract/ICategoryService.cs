using PeakSort.Core.Utilities.Results.Abstract;
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
        public Task<DataResult<Category>> Get(int categoryId);
        public Task<DataResult<IList<Category>>> GelAll();

        public Task<IResult> Add(CategoryAddDto categoryAddDto, string createdByName);
        public Task<IResult> Update(CategoryUpdateDto categoryUpdateDto, string modifiedByName);
        public Task<IResult> Delete(int categoryId);
        public Task<IResult> HardDelete(int categoryId);
    }
}
