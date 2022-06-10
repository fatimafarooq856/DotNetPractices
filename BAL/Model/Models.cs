using NodaTime;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.Model
{
    public class UserDto
    {
        //public int Id { get; set; }
        public string? FullName { get; set; }
        public string? Image { get; set; }
        public bool IsAdmin { get; set; }
        public bool IsActive { get; set; }
        public string? UserName { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }

    }
    public class Student
    {
        public string? Id { get; set; }
        public string? Name { get; set; }
        public string? Class { get; set; }
        public string? NIC { get; set; }
    }
    public class ProductDto
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Class { get; set; }
        public string NIC { get; set; }
        //public LocalDate FromDate { get; set; }
        //public LocalDate ToDate { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
    }
}
