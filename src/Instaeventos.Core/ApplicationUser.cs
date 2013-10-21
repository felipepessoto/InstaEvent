using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Data.Entity;

namespace Instaeventos.Core
{
    // You can add profile data for the user by adding more properties to your User class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : User
    {
        public ApplicationUser() : base() { }
        public ApplicationUser(string userName) : base(userName) { }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public string Cpf { get; set; }
        public string Email { get; set; }
        public DateTime CreatedDate { get; set; }
    }    
}