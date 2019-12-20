using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMS.Data
{
    public class UserLogin
    {
        [Required]
        public string UserEmail { get; set; }
        [Required]
        public string password { get; set; }
    }
}
