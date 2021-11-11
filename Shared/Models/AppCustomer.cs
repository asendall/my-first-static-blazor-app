using GAR.Events.Shared.IntegrationEvents;
using System.ComponentModel.DataAnnotations;

namespace BlazorApp.Shared.Models
{
    public class AppCustomer
    {
        /// <summary>
        /// The unique identifier for the Account
        /// </summary>
        [Key]
        public string Id { get; set; }
        
        [Required]
        public string Name { get; set; }

        [Required]
        public string SuperAdminEmail { get; set; }

        public int NumberOfUsers { get; set; }
        public Plan Plan { get; set; }
        public CustomerStatus CustomerStatus { get; set; }

    }
}
