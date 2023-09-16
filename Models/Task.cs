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
    public class Task
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id;
        [Required]
        public string Title;
        public string Description;
        [NotMapped]
        [JsonIgnore]
        public virtual ICollection<User> Users { get; set; }
        public Task()
        {
                
        }
    }
}
