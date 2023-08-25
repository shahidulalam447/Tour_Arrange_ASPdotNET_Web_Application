using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Xml.Linq;

namespace Tour_Arrange.Models
{
    public class Client
    {
        public Client()
        {
            this.BookingEntries = new List<BookingEntry>();
        }
        public int ClientId { get; set; }
        [Required, StringLength(50), Display(Name = "Client Name")]
        public string ClientName { get; set; }
        [Required, Display(Name = "Date of Birth"), DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime BirthDate { get; set; }
        public int Age { get; set; }
        public string Picture { get; set; }
        [Display(Name = "Marital Status")]
        public bool MaritalStatus { get; set; }
        //nev
        public ICollection<BookingEntry> BookingEntries { get; set; }
    }
    public class Spot
    {
        public Spot()
        {
            this.BookingEntries = new List<BookingEntry>();
        }
        public int SpotId { get; set; }
        [Required, StringLength(50), Display(Name = "Spot Name")]
        public string SpotName { get; set; }
        [Display(Name = "Spot View")]
        public string SpotPicture { get; set; }
        //nev
        public ICollection<BookingEntry> BookingEntries { get; set; }
    }
    public class BookingEntry
    {
        public int BookingEntryId { get; set; }
        [ForeignKey("Client")]
        public int ClientId { get; set; }
        [ForeignKey("Spot")]
        public int SpotId { get; set; }
        //nev
        public virtual Client Client { get; set; }
        public virtual Spot Spot { get; set; }

    }

    public class Department
    {
        public Department()
        {
            this.ManagementEntries = new List<ManagementEntry>();
        }
        public int DepartmentId { get; set; }
        [Required, StringLength(50), Display(Name = "Department")]
        public string DepartmentName { get; set; }
        //nev
        public ICollection<ManagementEntry> ManagementEntries { get; set; }
    }
    public class Employee
    {
        public Employee()
        {
            this.ManagementEntries = new List<ManagementEntry>();
        }
        public int EmployeeId { get; set; }
        [Required, StringLength(50), Display(Name = "Employee Name")]
        public string EmployeeName { get; set; }
        [Required, Display(Name = "Date of Birth"), DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime BirthDate { get; set; }
        [Required, Column(TypeName = "date"), DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Join Date")]
        public DateTime JoinDate { get; set; }
        [Required, DataType(DataType.EmailAddress)]
        [Display(Name = "Email Address")]
        public string Email { get; set; }
        [Display(Name = "Employee Picture")]
        public string EmployeePicture { get; set; }
        //nev
        public ICollection<ManagementEntry> ManagementEntries { get; set; }
    }
    public class ManagementEntry
    {
        public int ManagementEntryId { get; set; }
        [ForeignKey("Employee")]
        public int EmployeeId { get; set; }
        [ForeignKey("Department")]
        public int DepartmentId { get; set; }
        //nev
        public virtual Employee Employee { get; set; }
        public virtual Department Department { get; set; }

    }
    public class TourArrangeDbContext : DbContext
    {
        public DbSet<Client> Clients { get; set; }
        public DbSet<Spot> Spots { get; set; }
        public DbSet<BookingEntry> BookingEntries { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<ManagementEntry> ManagementEntries { get; set; }
    }
}