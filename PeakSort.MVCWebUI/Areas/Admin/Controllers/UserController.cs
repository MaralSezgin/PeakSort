using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PeakSort.Core.Utilities.ComplexType;
using PeakSort.Core.Utilities.Extensions;
using PeakSort.Entities.Concrete;
using PeakSort.Entities.Dtos;
using PeakSort.MVCWebUI.Areas.Admin.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace PeakSort.MVCWebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    //[Authorize] buraya eklemiyoruz giriş mi yapıcak yoksa bu sayfa bir türlü giremezsin her metodun üstüne eklenir login hariç
    public class UserController : Controller
    {
        private readonly SignInManager<User> _signInManager;
        private readonly UserManager<User> _userManager;
        private readonly IWebHostEnvironment _env;
        private readonly IMapper _mapper;

        public UserController(UserManager<User> userManager, IWebHostEnvironment env,IMapper mapper, SignInManager<User> signInManager)
        {
            _userManager = userManager;
            _env = env;
            _mapper = mapper;
            _signInManager = signInManager;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View("UserLogin");//view de ismi UserLogin istersen ismi Login yap 
        }
        [HttpPost]

        public async Task<IActionResult> Login(UserLoginDto userLoginDto)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(userLoginDto.Email);
                if (user != null)
                {
                    var result = await _signInManager.PasswordSignInAsync(user, userLoginDto.Password, userLoginDto.RememberMe, false);
                    if (result.Succeeded)//kullanıcı giriş yapabil miş mi
                    {
                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        ModelState.AddModelError("", "E-posta veya şifreniz yanlıştır.");
                        return View("UserLogin");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "E-posta veya şifreniz yanlıştır.");
                    return View("UserLogin");
                }
            }
            else
            {
                return View("UserLogin");
            }
        }
        [Authorize]
        public IActionResult Index()
        {
     
            var users = _userManager.Users.ToList();
            return View(new UserListDto
            {

                Users = users,
                ResultStatus = ResultStatus.Success,
            });
        }

        [HttpGet]
        [Authorize]
        public IActionResult Add()
        {
            return PartialView("_UserAddPartial");
        }

        [HttpPost]
        public async Task<IActionResult> Add(UserAddDto userAddDto)
        {
           if(ModelState.IsValid)
            {
                userAddDto.Picture = await ImageUpload(userAddDto.UserName,userAddDto.PictureFile);
                var user = _mapper.Map<User>(userAddDto);
                var result = await _userManager.CreateAsync(user, userAddDto.Password);
                if(result.Succeeded)
                {
                    var userAjaxModel = JsonSerializer.Serialize(new UserAddAjaxViewModel
                    {
                        UserDto = new UserDto
                        {
                            ResultStatus = ResultStatus.Success,
                            Message = $"{userAddDto.UserName} adlı kullanıcı başarıyla eklenmiştir.",
                            User = user
                        },
                        UserAddPartial = await this.RenderViewToStringAsync("_UserAddPartial", userAddDto)


                    });

                }
                else
                {
                    foreach (var item in result.Errors)
                    {
                        ModelState.AddModelError("", item.Description);
                    }
                    var userAjaxErrorModel = JsonSerializer.Serialize(new UserAddAjaxViewModel
                    {
                        UserAddDto = userAddDto,
                        UserAddPartial = await this.RenderViewToStringAsync("_UserAddPartial", userAddDto)
                    });
                    return Json(userAjaxErrorModel);
                }
            }
           
                var userAjaxErrorModell = JsonSerializer.Serialize(new UserAddAjaxViewModel
                {
                    UserAddDto = userAddDto,
                    UserAddPartial = await this.RenderViewToStringAsync("_UserAddPartial", userAddDto)
                });
                return Json(userAjaxErrorModell);

            
        }
        [Authorize]
        public async Task<string> ImageUpload(string userName, IFormFile  pictureFile)
        {
            string wwwroot = _env.WebRootPath;//wwwroot yolunu verir
            string fileName2 = Path.GetFileNameWithoutExtension(pictureFile.FileName);//resmin uzantısına takılmadan ismini alır sezgin.png >sezgin
            string fileExtension = Path.GetExtension(pictureFile.FileName);//sadece uzantısını al
            DateTime dateTime = DateTime.Now;
            string fileName = $"{userName}_{dateTime.FullDateAndTimeStringWithUnderscore()}{fileExtension}";
            var path = Path.Combine($"{wwwroot}/img", fileName);//resmi img klasorune append et

            await using(var stream=new FileStream(path,FileMode.Create))
            {
                await pictureFile.CopyToAsync(stream);
            }
            return fileName;
           
        }
        [Authorize]
        public bool ImageDelete(string pictureName)
        {
            string wwwroot = _env.WebRootPath;
            var a = $"{wwwroot}/img";
            var fileDelete = Path.Combine("wwwroot/img", pictureName);
            if (System.IO.File.Exists(fileDelete))
            {
                System.IO.File.Delete(fileDelete);
                return true;
            }
            else
                return false;
        }
        [Authorize]
        public async Task<JsonResult> Delete(int userId)
        {
            var user = await _userManager.FindByIdAsync(userId.ToString());
            var result = await _userManager.DeleteAsync(user);
           
            if (result.Succeeded)
            {
                var deleteduser = JsonSerializer.Serialize(new UserDto
                {
                    ResultStatus = ResultStatus.Success,
                     User=user,
                     Message=$"{user.UserName} adlı kullanıcı başarıyla silindi"
                });
                ImageDelete(user.Picture);
                return Json(deleteduser);
            }
            else
            {
                string errorMessages = null;
                foreach (var item in result.Errors)
                {
                    errorMessages = $"*{item.Description}\n";
                }
                var deleteduserError = JsonSerializer.Serialize(new UserDto
                {
                    ResultStatus = ResultStatus.Error,
                    User = user,
                    Message = $"{user.UserName} adlı kullanıcı silinemedi.\n {errorMessages}"
                });
                return Json(deleteduserError);
            }
        }
        [Authorize]
        public async Task<PartialViewResult> Update(int userId)
        {
            var user =await _userManager.FindByIdAsync(userId.ToString());
            var userUpdateDto = _mapper.Map<UserUpdateDto>(user);
            return PartialView("_UserUpdatePartial", userUpdateDto);

        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Update(UserUpdateDto userUpdateDto)
        {
            if(ModelState.IsValid)
            {
                bool isNewPictureUploded = false;
                var oldUser = await _userManager.FindByIdAsync(userUpdateDto.Id.ToString());
                string oldPicture = oldUser.Picture;

                if(userUpdateDto.PictureFile!=null)
                {
                    userUpdateDto.Picture = await ImageUpload(userUpdateDto.UserName,userUpdateDto.PictureFile);
                    isNewPictureUploded = true;
                }
                var updatedUser = _mapper.Map<UserUpdateDto, User>(userUpdateDto, oldUser);
                var result = await _userManager.UpdateAsync(updatedUser);
                if(result.Succeeded)
                {
                    if(isNewPictureUploded)
                    {
                        ImageDelete(oldPicture);
                    }
                    var userUpdateAjaxModel = JsonSerializer.Serialize(new UserUpdateAjaxViewModel
                    {
                        UserDto = new UserDto
                        {
                            User = updatedUser,
                            Message = $"{ userUpdateDto.UserName} kullanıcı başarıyla güncellendi",
                            ResultStatus = ResultStatus.Success,
                        },
                        UserUpdatePartial=await this.RenderViewToStringAsync("_UserUpdatePartial",userUpdateDto),

                    });
                    return Json(userUpdateAjaxModel);

                }
                else
                {
                    foreach (var item in result.Errors)
                    {
                        ModelState.AddModelError("", item.Description);

                    }
                    var userUpdateAjaxModelError = JsonSerializer.Serialize(new UserUpdateAjaxViewModel
                    {
                        UserUpdateDto = userUpdateDto,
                        UserUpdatePartial = await this.RenderViewToStringAsync("_UserUpdatePartial", userUpdateDto),

                    }); ;
                    return Json(userUpdateAjaxModelError);
                }

            }
            else
            {
                var userUpdateAjaxModelStateError = JsonSerializer.Serialize(new UserUpdateAjaxViewModel
                {
                    UserUpdateDto = userUpdateDto,
                    UserUpdatePartial = await this.RenderViewToStringAsync("_UserUpdatePartial", userUpdateDto),

                }); 
                return Json(userUpdateAjaxModelStateError);
            }

        
        }




    }
}
