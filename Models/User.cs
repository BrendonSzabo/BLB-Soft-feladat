using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using System.Threading.Tasks;
namespace Models
{
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Username { get; set; }
        [NotMapped]
        [JsonIgnore]
        public virtual ICollection<Task> Tasks { get; set; }
        public User()
        {

            Tasks = new List<Task>();
        }

        public User(int id, string username)
        {
            Id = id;
            Username = username;
            Tasks = new List<Task>();
        }
    }
}