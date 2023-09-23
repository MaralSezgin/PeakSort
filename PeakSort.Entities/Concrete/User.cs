using PeakSort.Core.Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PeakSort.Entities.Concrete
{
   public class User : EntityBase, IEntity
    {
        public int ID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public byte[] PasswordHash { get; set; }
        public string  UserName { get; set; }
       public Role Role { get; set; }
        public int RoleID { get; set; }
        public string Picture { get; set; }

    }
}
