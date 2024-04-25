using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Labb_3_API_v2.Models
{
    public class Person
    {
        [Key]
        public int PersonId { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        [JsonIgnore]
        public ICollection<Interest> Interests { get; set; }
        [JsonIgnore]
        public ICollection<Link> Links { get; set; }
    }
}
