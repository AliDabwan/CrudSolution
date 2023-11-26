using Entities;
using ServiceContracts.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ServiceContracts.DTOS
{
    public class PersonForCreateDTO
    {
        public string? Name { get; set; }
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
