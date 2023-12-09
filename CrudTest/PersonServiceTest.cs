using ServiceContracts.DTOS;
using ServiceContracts.Enums;
using ServiceContracts.Interfaces;
using Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit.Abstractions;

namespace CrudTest
{
    public class PersonServiceTest
    {
        private readonly IPersonService _personsService;
        private readonly ICountriesService _countriesService;
        private readonly ITestOutputHelper _testOutputHelper;

        public PersonServiceTest(ITestOutputHelper testOutputHelper)
        {
            _personsService=new PersonService(false);
            _countriesService=new CountriesService(false);
            _testOutputHelper = testOutputHelper;
        }
        #region AddPerson

        //When we supply null value as PersonForCreateDTO, it should throw ArgumentNullException
        [Fact]
        public void AddPerson_NullPerson()
        {
            //Arrange
            PersonForCreateDTO? personForCreateDTO = null;

            //Act
            Assert.Throws<ArgumentNullException>(() =>
            {
                _personsService.AddPerson(personForCreateDTO);
            });
        }


        //When we supply null value as Person Name, it should throw ArgumentException
        [Fact]
        public void AddPerson_PersonNameIsNull()
        {
            //Arrange
            PersonForCreateDTO? personForCreateDTO = new () { Name = null };

            //Act
            Assert.Throws<ArgumentException>(() =>
            {
                _personsService.AddPerson(personForCreateDTO);
            });
        }

        //When we supply proper person details, it should insert the person into the persons list; 	and it should return an object of PersonForReturnDTO, which includes with the newly 	generated person id
        [Fact]
        public void AddPerson_ProperPersonDetails()
        {
            //Arrange
            PersonForCreateDTO? personForCreateDTO = new ()
            { Name = "Person 	name...", Email = "person@example.com", CountryId = Guid.NewGuid(), Gender = GenderOptions.Male, DateOfBirth = DateTime.Parse("2000-01-01"), ReceiveEmails = true };

            //Act
            PersonForReturnDTO personForReturnDTO_from_add = _personsService.AddPerson(personForCreateDTO);

            List<PersonForReturnDTO> persons_list = _personsService.GetAllPersons();

            //Assert
            Assert.True(personForReturnDTO_from_add.Id != Guid.Empty);

            Assert.Contains(personForReturnDTO_from_add, persons_list);
        }

        #endregion

        #region GetAllPersons

        //The GetAllPersons() should return an empty list by default
        [Fact]
        public void GetAllPersons_EmptyList()
        {
            //Act
            List<PersonForReturnDTO> persons_from_get = _personsService.GetAllPersons();

            //Assert
            Assert.Empty(persons_from_get);
        }


        //First, we will add few persons; and then when we call GetAllPersons(), it should return the same persons that were added
        [Fact]
        public void GetAllPersons_AddFewPersons()
        {
            //Arrange
            CountryForCreateDto countryForCreate_1 = new() { Name = "Egypt" };
            CountryForCreateDto countryForCreate_2 = new() { Name = "Jordan" };

            CountryForReturnDto countryForReturn_1 = _countriesService.AddCountry(countryForCreate_1);
            CountryForReturnDto countryForReturn_2 = _countriesService.AddCountry(countryForCreate_2);

            PersonForCreateDTO personForCreate_1 = new() { Name = "Saad", Email = "sa@email.com", Gender = GenderOptions.Male, CountryId = countryForReturn_2.Id, DateOfBirth = DateTime.Parse("1979-01-01"), ReceiveEmails = true };

            PersonForCreateDTO personForCreate_2 = new() { Name = "Muhammad", Email = "ma@email.com", Gender = GenderOptions.Male, CountryId = countryForReturn_1.Id, DateOfBirth = DateTime.Parse("1981-01-01"), ReceiveEmails = true };

            PersonForCreateDTO personForCreate_3 = new() { Name = "Amany", Email = "am@email.com", Gender = GenderOptions.Female, CountryId = countryForReturn_1.Id, DateOfBirth = DateTime.Parse("1982-01-01"), ReceiveEmails = true };

            List<PersonForCreateDTO> personForCreate_list = new() { personForCreate_1, personForCreate_2, personForCreate_3 };

            List<PersonForReturnDTO> personForReturn_list_from_add = new();

            foreach (PersonForCreateDTO personForCreate in personForCreate_list)
            {
                PersonForReturnDTO personForReturnDTO = _personsService.AddPerson(personForCreate);
                personForReturn_list_from_add.Add(personForReturnDTO);
            }

            //display expecred data
            _testOutputHelper.WriteLine("Expected data");
            personForReturn_list_from_add.ForEach(p =>
            {
                _testOutputHelper.WriteLine(p.ToString());
            });
            //Act
            List<PersonForReturnDTO> persons_list_from_get =
                _personsService.GetAllPersons();

            //display acctual data
            _testOutputHelper.WriteLine(message: "Acctualdata");
            persons_list_from_get.ForEach(p =>
            {
                _testOutputHelper.WriteLine(p.ToString());
            });


            //Assert
            foreach (PersonForReturnDTO personForReturnDTO_from_add in personForReturn_list_from_add)
            {
                Assert.Contains(personForReturnDTO_from_add, persons_list_from_get);
            }
        }
        #endregion

        #region GetPersonById

        //If we supply null as Person Id, it should return null as PersonForReturnDTO
        [Fact]
        public void GetPersonById_NullPersonId()
        {
            //Arrange
            Guid? personId = null;

            //Act
            PersonForReturnDTO? personForReturnDTO_from_get = _personsService.GetPersonById(personId);

            //Assert
            Assert.Null(personForReturnDTO_from_get);
        }


        //If we supply a valid person id, it should return the valid person details as PersonForReturnDTO object
        [Fact]
        public void GetPersonById_WithPersonId()
        {
            //Arange
            CountryForCreateDto countryForCreateDTO = new() { Name = "Egypt" };
            CountryForReturnDto countryForReturnDTO = _countriesService.AddCountry(countryForCreateDTO);

            PersonForCreateDTO personForCreateDTO = new() { Name = "Muhammad", Email = "mo@email.com", CountryId = countryForReturnDTO.Id, DateOfBirth = DateTime.Parse("1981-01-01"), Gender = GenderOptions.Male, ReceiveEmails = false };

            PersonForReturnDTO personForReturnDTO_from_add = _personsService.AddPerson(personForCreateDTO);

            PersonForReturnDTO? personForReturnDTO_from_get = _personsService.GetPersonById(personForReturnDTO_from_add.Id);

            //Assert
            Assert.Equal(personForReturnDTO_from_add, personForReturnDTO_from_get);
        }

        #endregion

    }
}
