using PeakSort.Core.Entities.Abstract;
using PeakSort.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace PeakSort.Entities.Dtos
{
    public class ArticleDto:DtoGetBase
    {
        public Article Article { get; set; }
    }
}
