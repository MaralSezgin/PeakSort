﻿using Microsoft.AspNetCore.Mvc;
using PeakSort.Business.Abstract;
using PeakSort.Core.Utilities.ComplexType;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PeakSort.MVCWebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoryController : Controller
    {
        private readonly ICategoryService _categoryService;
        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        public async Task<IActionResult> Index()
        {
            var result = await _categoryService.GelAll(); 
            if(result.ResultStatus==ResultStatus.Success)
            {
                return View(result.Data);
            }
            return View();
        }
    }
}