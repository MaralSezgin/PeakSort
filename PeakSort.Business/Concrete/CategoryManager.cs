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

        public CategoryManager(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<IResult> Add(CategoryAddDto categoryAddDto, string createdByName)
        {
            await _unitOfWork.Categorys.AddAsync(new Category
            {
                 CategoryName=categoryAddDto.CategoryName,
                 Description=categoryAddDto.Description,
                 Note=categoryAddDto.Note,
                 IsActive=categoryAddDto.IsActive,
                 CreatedByName=createdByName,
                 CreatedDate=DateTime.Now,
                 ModifiedByName=createdByName,
                 ModifiedDate=DateTime.Now,
                 IsDeleted=false,

            }).ContinueWith(x=>_unitOfWork.SaveAsync());

            return new Result(ResultStatus.Success, $"{categoryAddDto.CategoryName} adlı kategori başarıyla eklenmiştir");
        }

        public Task<IResult> Delete(int categoryId)
        {
            throw new NotImplementedException();
        }

        public async Task<DataResult<IList<Category>>> GelAll()
        {
            var categories = await _unitOfWork.Categorys.GetAllAsync();
            if(categories.Count>-1)
            {
                return new DataResult<IList<Category>>(ResultStatus.Success, categories);
            }
            return new DataResult<IList<Category>>(ResultStatus.Error, categories);
        }

        public async Task<DataResult<Category>> Get(int categoryId)
        {
            var category= await _unitOfWork.Categorys.GetAsync(c => c.CategoryID == categoryId);
            if(category!=null)
            {
                return new DataResult<Category>(ResultStatus.Success, category);
            }
            return new DataResult<Category>(ResultStatus.Error, "Böyle bir katagori bulunamadı.",null);
            
        }

        public Task<IResult> HardDelete(int categoryId)
        {
            throw new NotImplementedException();
        }

        public async Task<IResult> Update(CategoryUpdateDto categoryUpdateDto, string modifiedByName)
        {
            var category = await _unitOfWork.Categorys.GetAsync(c => c.CategoryID == categoryUpdateDto.ID);
             if(category!=null)
            {
                category.CategoryName = categoryUpdateDto.CategoryName;
                category.Description = categoryUpdateDto.Description;
                category.Note = categoryUpdateDto.Note;
                category.IsActive = categoryUpdateDto.IsActive;
                category.ModifiedByName = modifiedByName;
                category.ModifiedDate = DateTime.Now;

                await _unitOfWork.Categorys.UpdateAsync(category).ContinueWith(x=>_unitOfWork.SaveAsync());
                return new Result(ResultStatus.Success, $"{categoryUpdateDto.CategoryName} başarıyla güncellenmiştir.");
            }
             return new Result(ResultStatus.Error,"Kategori bulunamadı");
        }
    }
}
