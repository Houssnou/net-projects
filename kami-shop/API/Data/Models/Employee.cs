using System.ComponentModel.DataAnnotations;

namespace API.Data.Models
{
    public class Employee
    {
        public int Id { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string FirstName { get; set; }
        public string Title { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string PhoneNumber { get; set; }
        public decimal Salary { get; set; }
        public string PictureUrl { get; set; }
        public string Bio { get; set; }

        public int DepartmentId { get; set; }
        public Department Department { get; set; }
    }
}
