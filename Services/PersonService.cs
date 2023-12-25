using Entities;
using ServiceContracts.DTOS;
using ServiceContracts.Enums;
using ServiceContracts.Interfaces;
using Services.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class PersonService : IPersonService
    {
        //private readonly List<Person> _persons;
        private readonly CRUDDbContext _dbContext;
        private readonly ICountriesService _countriesService;

        public PersonService(CRUDDbContext dbContext , ICountriesService countriesService) { 
        //_persons=new List<Person>();
            //_countriesService = new CountriesService();
            _dbContext = dbContext;
            _countriesService = countriesService;
            //if (initialize)
            //{
            //    _persons.Add(new() { Id = Guid.Parse("8082ED0C-396D-4162-AD1D-29A13F929824"), Name = "Muhammad Awadallah", Email = "mo@email.com", DateOfBirth = DateTime.Parse("1981-01-02"), Gender = "Male", ReceiveEmails = true, CountryId = Guid.Parse("000C76EB-62E9-4465-96D1-2C41FDB64C3B") });

            //    _persons.Add(new() { Id = Guid.Parse("06D15BAD-52F4-498E-B478-ACAD847ABFAA"), Name = "Amany Muhammad", Email = "amany@email.com", DateOfBirth = DateTime.Parse("1991-06-24"), Gender = "Female", ReceiveEmails = true, CountryId = Guid.Parse("32DA506B-3EBA-48A4-BD86-5F93A2E19E3F") });

            //    _persons.Add(new() { Id = Guid.Parse("D3EA677A-0F5B-41EA-8FEF-EA2FC41900FD"), Name = "Khaled Jaber", Email = "kha@email.com", DateOfBirth = DateTime.Parse("1993-08-13"), Gender = "Male", ReceiveEmails = false, CountryId = Guid.Parse("32DA506B-3EBA-48A4-BD86-5F93A2E19E3F") });

            //    _persons.Add(new() { Id = Guid.Parse("89452EDB-BF8C-4283-9BA4-8259FD4A7A76"), Name = "Galal Ali", Email = "galal@email.com", DateOfBirth = DateTime.Parse("1985-06-17"), Gender = "Male", ReceiveEmails = true, CountryId = Guid.Parse("DF7C89CE-3341-4246-84AE-E01AB7BA476E") });

            //    _persons.Add(new() { Id = Guid.Parse("F5BD5979-1DC1-432C-B1F1-DB5BCCB0E56D"), Name = "Fatima Ahmad", Email = "fatima@email.com", DateOfBirth = DateTime.Parse("1996-09-02"), Gender = "Female", CountryId = Guid.Parse("DF7C89CE-3341-4246-84AE-E01AB7BA476E") });

            //    _persons.Add(new() { Id = Guid.Parse("A795E22D-FAED-42F0-B134-F3B89B8683E5"), Name = "Batool Ali", Email = "batool@email.com", DateOfBirth = DateTime.Parse("1993-10-23"), Gender = "Female", CountryId = Guid.Parse("15889048-AF93-412C-B8F3-22103E943A6D") });

            //    _persons.Add(new() { Id = Guid.Parse("3C12D8E8-3C1C-4F57-B6A4-C8CAAC893D7A"), Name = "Amir Muhammad", Email = "amir@email.com", DateOfBirth = DateTime.Parse("1996-02-14"), Gender = "Male", ReceiveEmails = true, CountryId = Guid.Parse("80DF255C-EFE7-49E5-A7F9-C35D7C701CAB") });

            //    _persons.Add(new() { Id = Guid.Parse("7B75097B-BFF2-459F-8EA8-63742BBD7AFB"), Name = "Basel Mahmoud", Email = "basel@email.com", DateOfBirth = DateTime.Parse("1982-05-31"), Gender = "Male", ReceiveEmails = false, CountryId = Guid.Parse("000C76EB-62E9-4465-96D1-2C41FDB64C3B") });

            //    _persons.Add(new() { Id = Guid.Parse("6717C42D-16EC-4F15-80D8-4C7413E250CB"), Name = "Saad Muhammad", Email = "saad@email.com", DateOfBirth = DateTime.Parse("1999-02-02"), Gender = "Male", ReceiveEmails = false, CountryId = Guid.Parse("80DF255C-EFE7-49E5-A7F9-C35D7C701CAB") });

            //    _persons.Add(new() { Id = Guid.Parse("6E789C86-C8A6-4F18-821C-2ABDB2E95982"), Name = "Fareed Said", Email = "fareed@email.com", DateOfBirth = DateTime.Parse("1996-04-27"), Gender = "Male", ReceiveEmails = false, CountryId = Guid.Parse("000C76EB-62E9-4465-96D1-2C41FDB64C3B") });
            //}
        }

        private PersonForReturnDTO ConvertPersonToPersonForReturnDTO(Person person)
        {
            PersonForReturnDTO personForReturnDTO = person.ToPersonForReturn();
            personForReturnDTO.Country = _countriesService.GetCountryById(person.CountryId)?.Name;
            return personForReturnDTO;
        }
        public PersonForReturnDTO AddPerson(PersonForCreateDTO personForCreateDTO)
        {
            //check if PersonForCreateDTO is not null
            if (personForCreateDTO == null)
            {
                throw new ArgumentNullException(nameof(personForCreateDTO));
            }

            //Validate PersonName
            //if (string.IsNullOrEmpty(personForCreateDTO.Name))
            //{
            //    throw new ArgumentException("Person Name can't be blank");
            //}

           
            // helper method for validation

            Helper.ValidateModel(personForCreateDTO);

            //convert personForCreateDTO into Person type
            Person person = personForCreateDTO.ToPerson();

            //generate Person Id
            person.Id = Guid.NewGuid();

            //add person object to persons list
            _dbContext.Persons.Add(person);
            _dbContext.SaveChanges();

            //convert the Person object into PersonForReturnDTO type
            PersonForReturnDTO personForReturnDTO = person.ToPersonForReturn();
            personForReturnDTO.Country = _countriesService.GetCountryById(person.CountryId)?.Name;
            return personForReturnDTO;
        }

        public List<PersonForReturnDTO> GetAllPersons()
        {
            //return _persons.Select(c => c.ToPersonForReturn()).ToList();
            return _dbContext.Persons.ToList().Select(c => 
            ConvertPersonToPersonForReturnDTO(c)).ToList();

        }

        public PersonForReturnDTO? GetPersonById(Guid? id)
        {
            if (id == null)
            {
                return null;
            }
            Person? person = _dbContext.Persons.FirstOrDefault(c => c.Id == id);
            if (person == null) { return null; }
            //return person.ToPersonForReturn();
            return ConvertPersonToPersonForReturnDTO( person);
        }

        public List<PersonForReturnDTO> GetFilteredPersons(string searchBy, string? searchString)
        {
            List<PersonForReturnDTO> allPersons = GetAllPersons();
            List<PersonForReturnDTO> matchingPersons = allPersons;

            if (string.IsNullOrEmpty(searchBy) || string.IsNullOrEmpty(searchString))
                return matchingPersons;

            switch (searchBy)
            {
                case nameof(PersonForReturnDTO.Name):
                    matchingPersons = allPersons.Where(p =>
                    !string.IsNullOrEmpty(p.Name) && p.Name.Contains(searchString, StringComparison.OrdinalIgnoreCase)).ToList();
                    break;

                case nameof(PersonForReturnDTO.Email):
                    matchingPersons = allPersons.Where(p =>
                    !string.IsNullOrEmpty(p.Email) && p.Email.Contains(searchString, StringComparison.OrdinalIgnoreCase)).ToList();
                    break;


                case nameof(PersonForReturnDTO.DateOfBirth):
                    matchingPersons = allPersons.Where(p =>
                    p.DateOfBirth != null && p.DateOfBirth.Value.ToString("dd MM yyyy").Contains(searchString, StringComparison.OrdinalIgnoreCase)).ToList();
                    break;

                case nameof(PersonForReturnDTO.Gender):
                    matchingPersons = allPersons.Where(p =>
                    !string.IsNullOrEmpty(p.Gender) && p.Gender.Contains(searchString, StringComparison.OrdinalIgnoreCase)).ToList();
                    break;

                case nameof(PersonForReturnDTO.Country):
                    matchingPersons = allPersons.Where(p =>
                    !string.IsNullOrEmpty(p.Country) && p.Country.Contains(searchString, StringComparison.OrdinalIgnoreCase)).ToList();
                    break;

                default: matchingPersons = allPersons; break;
            }
            return matchingPersons;

        }

        public List<PersonForReturnDTO> GetSortedPersons(List<PersonForReturnDTO> allPersons, string sortBy, SortOrderOptions sortOrder)
        {
            if (string.IsNullOrEmpty(sortBy))
                return allPersons;

            List<PersonForReturnDTO> sortedPersons = (sortBy, sortOrder) switch
            {
                (nameof(PersonForReturnDTO.Name), SortOrderOptions.ASC) => allPersons.OrderBy(p => p.Name, StringComparer.OrdinalIgnoreCase).ToList(),

                (nameof(PersonForReturnDTO.Name), SortOrderOptions.DESC) => allPersons.OrderByDescending(p => p.Name, StringComparer.OrdinalIgnoreCase).ToList(),

                (nameof(PersonForReturnDTO.Email), SortOrderOptions.ASC) => allPersons.OrderBy(p => p.Email, StringComparer.OrdinalIgnoreCase).ToList(),

                (nameof(PersonForReturnDTO.Email), SortOrderOptions.DESC) => allPersons.OrderByDescending(p => p.Email, StringComparer.OrdinalIgnoreCase).ToList(),

                (nameof(PersonForReturnDTO.DateOfBirth), SortOrderOptions.ASC) => allPersons.OrderBy(p => p.DateOfBirth).ToList(),

                (nameof(PersonForReturnDTO.DateOfBirth), SortOrderOptions.DESC) => allPersons.OrderByDescending(p => p.DateOfBirth).ToList(),

                (nameof(PersonForReturnDTO.Age), SortOrderOptions.ASC) => allPersons.OrderBy(p => p.Age).ToList(),

                (nameof(PersonForReturnDTO.Age), SortOrderOptions.DESC) => allPersons.OrderByDescending(p => p.Age).ToList(),

                (nameof(PersonForReturnDTO.Gender), SortOrderOptions.ASC) => allPersons.OrderBy(p => p.Gender, StringComparer.OrdinalIgnoreCase).ToList(),

                (nameof(PersonForReturnDTO.Gender), SortOrderOptions.DESC) => allPersons.OrderByDescending(p => p.Gender, StringComparer.OrdinalIgnoreCase).ToList(),

                (nameof(PersonForReturnDTO.Country), SortOrderOptions.ASC) => allPersons.OrderBy(p => p.Country, StringComparer.OrdinalIgnoreCase).ToList(),

                (nameof(PersonForReturnDTO.Country), SortOrderOptions.DESC) => allPersons.OrderByDescending(p => p.Country, StringComparer.OrdinalIgnoreCase).ToList(),

                _ => allPersons
            };

            return sortedPersons;
        }

        public PersonForReturnDTO UpdatePerson(PersonForUpdateDto personForUpdateDto)
        {
            if (personForUpdateDto == null)
                throw new ArgumentNullException(nameof(Person));

            //validation
            Helper.ValidateModel(personForUpdateDto);

            //get matching person object to update
            Person? personForUpdate = _dbContext.Persons.FirstOrDefault(p => p.Id == personForUpdateDto.Id);
            if (personForUpdate == null)
            {
                throw new ArgumentException("Given person id doesn't exist");
            }

            //update all details
            personForUpdate.Name = personForUpdateDto.Name;
            personForUpdate.Email = personForUpdateDto.Email;
            personForUpdate.DateOfBirth = personForUpdateDto.DateOfBirth;
            personForUpdate.Gender = personForUpdateDto.Gender.ToString();
            personForUpdate.CountryId = personForUpdateDto.CountryId;
            personForUpdate.ReceiveEmails = personForUpdateDto.ReceiveEmails;

            return personForUpdate.ToPersonForReturn();
        }

        public bool DeletePerson(Guid? id)
        {
            if (id == null)
            {
                throw new ArgumentNullException(nameof(id));
            }

            Person? person = _dbContext.Persons.FirstOrDefault(p => p.Id == id);
            if (person == null)
                return false;

            _dbContext.Persons.Remove(person);
            _dbContext.SaveChanges();
            return true;
        }
    }
}
