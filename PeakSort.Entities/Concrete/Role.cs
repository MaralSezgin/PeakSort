using Microsoft.AspNetCore.Identity;
using PeakSort.Core.Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PeakSort.Entities.Concrete
{
    public class Role : IdentityRole<int>//int primary key
    {
        //not id rolename identity saglıyor
    }
}
