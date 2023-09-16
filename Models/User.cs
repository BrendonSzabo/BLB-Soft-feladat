using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
namespace Models
{
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id;
        public string Username;
        [NotMapped]
        [JsonIgnore]
        public virtual ICollection<Task> Tasks { get; set; }
        public User()
        {
                
        }
    }
}