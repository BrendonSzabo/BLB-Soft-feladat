using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Models
{
    public class Task
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        public string? Description { get; set; }
        [NotMapped]
        [JsonIgnore]
        public virtual ICollection<User> Users { get; set; }
        public string? Date { get; set; }
        public Task()
        {
            Users = new List<User>();
        }

        public Task(int id, string title)
        {
            Id = id;
            Title = title;
            Users = new List<User>();
        }
    }
}
