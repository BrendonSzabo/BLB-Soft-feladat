using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Models
{
    public class Users
    {
        public int Id;
        public string Username;
        public virtual ICollection<Task> Tasks { get; set; }
    }
}