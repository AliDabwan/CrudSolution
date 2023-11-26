using Entities;
using ServiceContracts.DTOS;
using ServiceContracts.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class PersonService : IPersonService
    {
        private readonly List<Person> _persons;
        private readonly ICountriesService _countriesService;

        public PersonService() { 
        _persons=new List<Person>();
            _countriesService = new CountriesService();

        }
        public PersonForReturnDTO AddPerson(PersonForCreateDTO personForCreateDTO)
        {
            //check if PersonForCreateDTO is not null
            if (personForCreateDTO == null)
            {
                throw new ArgumentNullException(nameof(personForCreateDTO));
            }

            //Validate PersonName
            if (string.IsNullOrEmpty(personForCreateDTO.Name))
            {
                throw new ArgumentException("Person Name can't be blank");
            }

            //convert personForCreateDTO into Person type
            Person person = personForCreateDTO.ToPerson();

            //generate Person Id
            person.Id = Guid.NewGuid();

            //add person object to persons list
            _persons.Add(person);

            //convert the Person object into PersonForReturnDTO type
            PersonForReturnDTO personForReturnDTO = person.ToPersonForReturn();
            personForReturnDTO.Country = _countriesService.GetCountryById(person.CountryId)?.Name;
            return personForReturnDTO;
        }

        public List<PersonForReturnDTO> GetAllPersons()
        {
            throw new NotImplementedException();
        }

        public PersonForReturnDTO? GetPersonById(Guid? id)
        {
            throw new NotImplementedException();
        }
    }
}
