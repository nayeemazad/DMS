using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace DMS.Data
{
    public class User
    {
        public User()
        {
            this.Catgories = new HashSet<Category>();
            this.Documents = new HashSet<Document>();
        }
        public static ClaimsIdentity Identity { get; set; }
        [Key]
        public int UserId { get; set; }
        [Required]
        public string UserEmail { get; set; }
        [Required]
        public string UserName { get; set; }
        [Required]
        public string password { get; set; }
        [Required]
        public string UserRole { get; set; }
        public virtual ICollection<Category> Catgories { get; set; }
        public virtual ICollection<Document> Documents { get; set; }
    }
}
