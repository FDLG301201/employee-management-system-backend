using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Employee
    {
        [Key] // <--- ESTO LE GRITA A EF: "¡YO SOY EL ID!"
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] // Asegura que sea autoincremental
        public int Id { get; set; }
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public required string Email { get; set; }
        public required string JobTitle { get; set; }
        public DateTime HireDate { get; set; }

    }
}
