﻿using PeakSort.Core.DataAccess.Abstract;
using PeakSort.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace PeakSort.DataAccess.Abstract
{
    public interface ICommentRepository:IEntityRepository<Comment>
    {
    }
}
