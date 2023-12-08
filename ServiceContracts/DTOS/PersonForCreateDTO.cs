using Entities;
using ServiceContracts.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ServiceContracts.DTOS
{
    public class PersonForCreateDTO
    {
        [Required(ErrorMessage ="Person is required")]
        public string? Name { get; set; }
        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage ="Email is invalid")]
        public string? Email { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public GenderOptions? Gender { get; set; }
        public Guid? CountryId { get; set; }
        public bool ReceiveEmails { get; set; }


        public Person ToPerson()
        {
            return new() { Name = Name, Email = Email, DateOfBirth = DateOfBirth, Gender = Gender.ToString(), CountryId = CountryId, ReceiveEmails = ReceiveEmails };
        }
    }
}
