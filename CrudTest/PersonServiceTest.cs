using Entities;
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

        #region GetFilteredPersons
        //If the search text is empty and search by is "Name", it should return all persons
        [Fact]
        public void GetFilteredPersons_EmptySearchText()
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

            //Display Expected Data
            _testOutputHelper.WriteLine("Expected:");
            personForReturn_list_from_add.ForEach(p =>
            {
                _testOutputHelper.WriteLine(p.ToString());
            });

            //Act
            List<PersonForReturnDTO> persons_list_from_search = _personsService.GetFilteredPersons(nameof(Person.Name), "");

            //Display Actual Data
            _testOutputHelper.WriteLine("Actual:");
            persons_list_from_search.ForEach(p =>
            {
                _testOutputHelper.WriteLine(p.ToString());
            });

            //Assert
            foreach (PersonForReturnDTO personForReturnDTO_from_add in personForReturn_list_from_add)
            {
                Assert.Contains(personForReturnDTO_from_add, persons_list_from_search);
            }
        }

        //First we will add few persons; and then we will search based on person name with some search string. It should return the matching persons
        [Fact]
        public void GetFilteredPersons_SearchByPersonName()
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

            //Display Expected Data
            _testOutputHelper.WriteLine("Expected:");
            personForReturn_list_from_add.ForEach(p =>
            {
                _testOutputHelper.WriteLine(p.ToString());
            });

            //Act
            List<PersonForReturnDTO> persons_list_from_search = _personsService.GetFilteredPersons(nameof(Person.Name), "am");

            //Display Actual Data
            _testOutputHelper.WriteLine("Actual:");
            persons_list_from_search.ForEach(p =>
            {
                _testOutputHelper.WriteLine(p.ToString());
            });

            //Assert
            foreach (PersonForReturnDTO personForReturnDTO_from_add in personForReturn_list_from_add)
            {
                if (personForReturnDTO_from_add.Name != null)
                {
                    if (personForReturnDTO_from_add.Name.Contains("am", StringComparison.OrdinalIgnoreCase))
                    {
                        Assert.Contains(personForReturnDTO_from_add, persons_list_from_search);
                    }
                }
            }
        }
        #endregion

        #region GetSortedPersons

        //When we sort based on Person Name in ASC, it should return persons list in ascending on Person Name
        [Fact]
        public void GetSortedPersons()
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

            //Display Expected Data
            _testOutputHelper.WriteLine("Expected:");
            personForReturn_list_from_add.ForEach(p =>
            {
                _testOutputHelper.WriteLine(p.ToString());
            });

            List<PersonForReturnDTO> allPersons = _personsService.GetAllPersons();
            //Act
            List<PersonForReturnDTO> persons_list_from_sort = _personsService.GetSortedPersons(allPersons, nameof(Person.Name), SortOrderOptions.ASC);

            //Display Actual Data
            _testOutputHelper.WriteLine("Actual:");
            persons_list_from_sort.ForEach(p =>
            {
                _testOutputHelper.WriteLine(p.ToString());
            });
            personForReturn_list_from_add = personForReturn_list_from_add.OrderBy(p => p.Name).ToList();
            //Assert
            for (int i = 0; i < personForReturn_list_from_add.Count; i++)
            {
                Assert.Equal(personForReturn_list_from_add[i], persons_list_from_sort[i]);
            }
        }
        #endregion


        #region UpdatePerson

        //When we supply null as PersonForUpdateDTO, it should throw ArgumentNullException
        [Fact]
        public void UpdatePerson_NullPerson()
        {
            //Arrange
            PersonForUpdateDto? personForUpdateDTO = null;

            //Assert
            Assert.Throws<ArgumentNullException>(() =>
            {
                //Act
                _personsService.UpdatePerson(personForUpdateDTO);
            });
        }


        //When we supply invalid person id, it should throw ArgumentException
        [Fact]
        public void UpdatePerson_InvalidPersonID()
        {
            //Arrange
            PersonForUpdateDto? personForUpdateDTO = new() { Id = Guid.NewGuid() };

            //Assert
            Assert.Throws<ArgumentException>(() =>
            {
                //Act
                _personsService.UpdatePerson(personForUpdateDTO);
            });
        }

        //Validations
        //When Person Name is null, it should throw ArgumentException
        [Fact]
        public void UpdatePerson_PersonNameIsNull()
        {
            //Arrange
            CountryForCreateDto countryForCreateDTO = new() { Name = "Egypt" };
            CountryForReturnDto countryForReturnDTO = _countriesService.AddCountry(countryForCreateDTO);

            PersonForCreateDTO personForCreateDTO = new() { Name = "Amir", CountryId = countryForReturnDTO.Id, Email = "amir@email.com", Gender = GenderOptions.Male };

            PersonForReturnDTO personForReturnDTO_from_add = _personsService.AddPerson(personForCreateDTO);

            PersonForUpdateDto personForUpdateDTO = personForReturnDTO_from_add.ToPersonForUpdateDTO();
            personForUpdateDTO.Name = null;

            //Assert
            Assert.Throws<ArgumentException>(() =>
            {
                //Act
                _personsService.UpdatePerson(personForUpdateDTO);
            });

        }


        //First, add a new person and try to update the person name,email,gender
        [Fact]
        public void UpdatePerson_PersonFullDetailsUpdate()
        {
            //Arrange
            CountryForCreateDto countryForCreateDTO = new() { Name = "Egypt" };
            CountryForReturnDto countryForReturnDTO = _countriesService.AddCountry(countryForCreateDTO);

            PersonForCreateDTO personForCreateDTO = new() { Name = "Amir", CountryId = countryForReturnDTO.Id, Email = "amir@email.com", Gender = GenderOptions.Male };

            PersonForReturnDTO personForReturnDTO_from_add = _personsService.AddPerson(personForCreateDTO);

            //Display Person before update
            _testOutputHelper.WriteLine("Person before update:");
            _testOutputHelper.WriteLine(personForReturnDTO_from_add.ToString());

            PersonForUpdateDto PersonForUpdateDTO = personForReturnDTO_from_add.ToPersonForUpdateDTO();
            PersonForUpdateDTO.Name = "Amany";
            PersonForUpdateDTO.Email = "amany@email.com";
            PersonForUpdateDTO.Gender = GenderOptions.Female;

            //Act
            PersonForReturnDTO personForReturnDTO_from_update = _personsService.UpdatePerson(PersonForUpdateDTO);

            PersonForReturnDTO? personForReturnDTO_from_get = _personsService.GetPersonById(personForReturnDTO_from_update.Id);

            //Display Person after update
            _testOutputHelper.WriteLine("Person after update:");
            _testOutputHelper.WriteLine(personForReturnDTO_from_get!.ToString());
            //Assert
            Assert.Equal(personForReturnDTO_from_get, personForReturnDTO_from_update);

        }

        #endregion
        #region DeletePerson

        //If you supply an invalid Person Id, it should return false
        [Fact]
        public void DeletePerson_InvalidPersonID()
        {
            //Act
            bool isDeleted = _personsService.DeletePerson(Guid.NewGuid());

            //Assert
            Assert.False(isDeleted);
        }

        //If you supply an valid Person ID, it should return true
        [Fact]
        public void DeletePerson_ValidPersonID()
        {
            //Arrange
            CountryForCreateDto countryForCreateDTO = new() { Name = "Egypt" };
            CountryForReturnDto CountryForReturnDTO_from_add = _countriesService.AddCountry(countryForCreateDTO);

            PersonForCreateDTO personForCreateDTO = new() { Name = "Ahmed", CountryId = CountryForReturnDTO_from_add.Id, DateOfBirth = Convert.ToDateTime("2010-01-01"), Email = "ahmed@email.com", Gender = GenderOptions.Male, ReceiveEmails = true };

            PersonForReturnDTO PersonForReturnDTO_from_add = _personsService.AddPerson(personForCreateDTO);

            //Act
            bool isDeleted = _personsService.DeletePerson(PersonForReturnDTO_from_add.Id);

            //Assert
            Assert.True(isDeleted);
        }

        #endregion
    }
}
