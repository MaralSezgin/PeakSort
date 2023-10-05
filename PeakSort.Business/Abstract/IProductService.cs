using PeakSort.Core.Utilities.Results.Abstract;
using PeakSort.Core.Utilities.Results.Concrete;
using PeakSort.Entities.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PeakSort.Business.Abstract
{
    public interface IProductService
    {

        public Task<DataResult<ProductDto>> Get(int productId);
        public Task<DataResult<ProductListDto>> GelAll();

        public Task<IResult> Add(ProductAddDto productAddDto, string createdByName);
        public Task<IResult> Update(ProductUpdateDto  productUpdateDto, string modifiedByName);
        public Task<IResult> Delete(int productId);
        public Task<IResult> HardDelete(int productId);
    }
}
