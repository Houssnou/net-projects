using System.ComponentModel.DataAnnotations;

namespace API.Data.Models
{
    public class Department
    {
        public int Id { get; set; }
        [Required] 
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }

        public List<Employee> Employees { get; set; }
    }
}
