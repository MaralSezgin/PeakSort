﻿using Microsoft.AspNetCore.Identity;
using PeakSort.Core.Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PeakSort.Entities.Concrete
{
   public class User : IdentityUser<int>
    {
   
        public string Picture { get; set; }
        //not id user identity saglıyor
    }
}
