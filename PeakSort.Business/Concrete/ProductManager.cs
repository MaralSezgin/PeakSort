using AutoMapper;
using PeakSort.Business.Abstract;
using PeakSort.Core.Utilities.ComplexType;
using PeakSort.Core.Utilities.Results.Abstract;
using PeakSort.Core.Utilities.Results.Concrete;
using PeakSort.DataAccess.Abstract;
using PeakSort.Entities.Concrete;
using PeakSort.Entities.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PeakSort.Business.Concrete
{
    public class ProductManager : IProductService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public ProductManager(IUnitOfWork unitOfWork,IMapper mapper )
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }
        public async Task<IResult> Add(ProductAddDto productAddDto, string createdByName)
        {
            var product = _mapper.Map<Product>(productAddDto);
            product.CreatedByName = createdByName;
            product.ModifiedByName = createdByName;

            await _unitOfWork.Products.AddAsync(product).ContinueWith(x => _unitOfWork.SaveAsync());
            return new Result(ResultStatus.Success, $"{product.Title} başlıklı ürün başarıyla eklendi");
          
        }

        public async Task<IResult> Delete(int productId)
        {
            var result = await _unitOfWork.Products.AnyAsync(x => x.ID == productId);
            if(result)
            {
                var product =await _unitOfWork.Products.GetAsync(x => x.ID == productId);
                product.IsDeleted = true;

                await _unitOfWork.Products.UpdateAsync(product).ContinueWith(x => _unitOfWork.SaveAsync());

                return new Result(ResultStatus.Success, $"{product.Title} isimli ürün başarıyla silinmiştir");
            }
            return new Result(ResultStatus.Error, "Ürün bulunamadı");
        }

        public async Task<DataResult<ProductListDto>> GelAll()
        {
            var product = await _unitOfWork.Products.GetAllAsync();
            if (product.Count > -1)
            {
                return new DataResult<ProductListDto>(ResultStatus.Success, new ProductListDto
                {
                    Products = product,
                    ResultStatus = ResultStatus.Success,
                });
            }
            return new DataResult<ProductListDto>(ResultStatus.Error, "Böyle bir ürün bulunamadı", null);
        }

        public async Task<DataResult<ProductDto>> Get(int productId)
        {
            var product =  await _unitOfWork.Products.GetAsync(x => x.ID == productId);
            if(product!=null)
            {
                return new DataResult<ProductDto>(ResultStatus.Success, new ProductDto
                {
                    Product = product,
                    ResultStatus = ResultStatus.Success,

                }) ;
            }
            return new DataResult<ProductDto>(ResultStatus.Error, "Böyle bir ürün bulunamadı", null);
        }

        public async Task<IResult> HardDelete(int productId)
        {
            var result = await _unitOfWork.Products.AnyAsync(x => x.ID == productId);
            if (result)
            {
                var product = await _unitOfWork.Products.GetAsync(x => x.ID == productId);

                await _unitOfWork.Products.DeleteAsync(product).ContinueWith(x => _unitOfWork.SaveAsync());

                return new Result(ResultStatus.Success, $"{product.Title} isimli ürün kalıcı olarak başarıyla silinmiştir");
            }
            return new Result(ResultStatus.Error, "Ürün bulunamadı");
        }

        public async Task<IResult> Update(ProductUpdateDto productUpdateDto, string modifiedByName)
        {
            var product = _mapper.Map<Product>(productUpdateDto);
            product.CreatedByName = modifiedByName;
            product.ModifiedByName = modifiedByName;

            await _unitOfWork.Products.AddAsync(product).ContinueWith(x => _unitOfWork.SaveAsync());
            return new Result(ResultStatus.Success, $"{product.Title} başlıklı ürün başarıyla güncellendi");
        }

    }
}
