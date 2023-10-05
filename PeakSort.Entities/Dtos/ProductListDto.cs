using PeakSort.Core.Entities.Abstract;
using PeakSort.Core.Utilities.ComplexType;
using PeakSort.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PeakSort.Entities.Dtos
{
   public class ProductListDto:DtoGetBase
    {
        public IList<Product> Products { get; set; }
        public override ResultStatus ResultStatus { get; set; } = ResultStatus.Success;
    }
}
