﻿using PeakSort.Core.Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PeakSort.Entities.Concrete
{
   public class Project : EntityBase, IEntity
    {
        public int ID { get; set; }
        public string Description { get; set; }
        public string Title { get; set; }
        public string  Image { get; set; }
       
    }
}
