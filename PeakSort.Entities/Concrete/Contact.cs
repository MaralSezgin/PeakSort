using PeakSort.Core.Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PeakSort.Entities.Concrete
{
    public class Contact:EntityBase,IEntity
    {
        public int ID { get; set; }
        public string Address { get; set; }
        public string Address1 { get; set; }

        public string Phone { get; set; }
        public string Fax { get; set; }
        public string MobilePhone { get; set; }
        public string MobilePhone1 { get; set; }
        public string Email { get; set; }
        public string MapLink { get; set; }
        public string Image { get; set; }
    }
}
