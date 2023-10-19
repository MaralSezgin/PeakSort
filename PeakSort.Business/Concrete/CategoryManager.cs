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
    public class CategoryManager : ICategoryService
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CategoryManager(IUnitOfWork unitOfWork,IMapper mapper)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

  

        public async Task<IResult> Delete(int categoryId)
        {
            var result =await _unitOfWork.Categorys.AnyAsync(x => x.CategoryID == categoryId);
            if(result)
            {
                var category =await _unitOfWork.Categorys.GetAsync(x => x.CategoryID == categoryId);
                category.IsDeleted = true;
                await _unitOfWork.Categorys.DeleteAsync(category).ContinueWith(x=>_unitOfWork.SaveAsync());
                return new Result(ResultStatus.Success, $"{category.CategoryName} isimli kategori başarıyla silinmiştir");

            }
            return new Result(ResultStatus.Error,"kategori bulunamadı");
        }

        public async Task<DataResult<CategoryListDto>> GelAll()
        {
            var categories = await _unitOfWork.Categorys.GetAllAsync();
            if (categories.Count > -1)
            {
                return new DataResult<CategoryListDto>(ResultStatus.Success, new CategoryListDto
                {
                    Categories = categories,
                     ResultStatus=ResultStatus.Success,
                }) ;

            }
            return new DataResult<CategoryListDto>(ResultStatus.Success, "Kategori bulunamadı", null);
        }

        public async Task<DataResult<CategoryDto>> Get(int categoryId)
        {
            var category = await _unitOfWork.Categorys.GetAsync(x => x.CategoryID == categoryId);
            if (category != null)
            {
                return new DataResult<CategoryDto>(ResultStatus.Success, new CategoryDto
                {
                    Category = category,
                    ResultStatus=ResultStatus.Success,
                }); ;

            }
            return new DataResult<CategoryDto>(ResultStatus.Success, "Kategori bulunamadı", null);
        }

        public async Task<IResult> HardDelete(int categoryId)
        {
            var category = await _unitOfWork.Categorys.GetAsync(x => x.CategoryID == categoryId);
            if (category != null)
            {
                await _unitOfWork.Categorys.DeleteAsync(category).ContinueWith(x => _unitOfWork.SaveAsync());
                return new Result(ResultStatus.Success, $"{category.CategoryName} isimli kategori başarıyla silinmiştir");

            }
            return new Result(ResultStatus.Error, "kategori bulunamadı");
        }
        public async Task<DataResult<CategoryDto>> Add(CategoryAddDto categoryAddDto, string createdByName)
        {
            var category = _mapper.Map<Category>(categoryAddDto);

            var addedcategory = await _unitOfWork.Categorys.AddAsync(category);
            addedcategory.CreatedByName = createdByName;
            addedcategory.ModifiedByName = createdByName;
            await _unitOfWork.SaveAsync();

            return new DataResult<CategoryDto>(ResultStatus.Success, $"{category.CategoryName} adlı kategory başayıyla eklenmiştir", new CategoryDto
            {
                Category = addedcategory,
                ResultStatus = ResultStatus.Success,
                Message = $"{addedcategory.CategoryName} adlı kategory başayıyla eklenmiştir"


            }); ;

        }

        public async Task<DataResult<CategoryDto>> Update(CategoryUpdateDto categoryUpdateDto, string modifiedByName)
        {
            var category = await _unitOfWork.Categorys.GetAsync(x => x.CategoryID == categoryUpdateDto.CategoryID);
          
            if(category!=null)
            {
                category.CategoryName = categoryUpdateDto.CategoryName;
                category.Description = categoryUpdateDto.Description;
                category.Note = categoryUpdateDto.Note;
                category.IsActive = categoryUpdateDto.IsActive;
                category.ModifiedByName = categoryUpdateDto.CategoryName;
                category.ModifiedDate = categoryUpdateDto.date;

                var updateCategory = await _unitOfWork.Categorys.UpdateAsync(category);
                await _unitOfWork.SaveAsync();
                return new DataResult<CategoryDto>(ResultStatus.Success, $"{category.CategoryName} isimli kategori başarıyla güncellenmiştir", new CategoryDto
                {
                    Category = updateCategory,
                    ResultStatus = ResultStatus.Success,
                    Message = $"{updateCategory.CategoryName} adlı kategori başarıyla güncellenmiştir",

                });
            }
            return new DataResult<CategoryDto>(ResultStatus.Error,"Kategori bulunamadı", new CategoryDto
            {

                ResultStatus = ResultStatus.Success,
                Message ="Kategori bulunamdı",
                    
            });




        }

        public async Task<DataResult<CategoryUpdateDto>> GetCategoryUpdateDto(int categoryId)
        {
            var result = await _unitOfWork.Categorys.AnyAsync(x=>x.CategoryID==categoryId);
            if(result)
            {
                var category = await _unitOfWork.Categorys.GetAsync(x => x.CategoryID == categoryId);
                var categoryUpdateDto = _mapper.Map<CategoryUpdateDto>(category);
                return new DataResult<CategoryUpdateDto>(ResultStatus.Success, categoryUpdateDto);
            }
            return new DataResult<CategoryUpdateDto>(ResultStatus.Error, "Böyle bir kategori bulunamadı", null);

        }
    }
}
