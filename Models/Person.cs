namespace OtroProyecto.Models;
using System.ComponentModel.DataAnnotations;


    public class Person
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string LastName { get; set; }

    public DateTime Birthday { get; set; }


    }

