using System;
using System.ComponentModel.DataAnnotations;

namespace BlazorApp.Shared.Models
{
    public class AppCustomer
    {
        /// <summary>
        /// The unique identifier for the Account
        /// </summary>
        [Key]
        public Guid Id { get; private set; }
        
        [Required]
        public string Name { get; private set; }

        [Required]
        public string SuperAdminEmail { get; private set; }

        public int NumberOfUsers { get; private set; }

        private AppCustomer() { }

        public AppCustomer(Guid id, string name, string superAdminEmail )
        {
            Id = id;
            Name = name;
            SuperAdminEmail = superAdminEmail;
        }

        public void  AddUser()
        {
            NumberOfUsers ++;
        }

        public void RemoveUser()
        {
            NumberOfUsers--;
        }
    }
}
