using ServiceContracts.DTOS;
using ServiceContracts.Enums;
using ServiceContracts.Interfaces;
using Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrudTest
{
    public class PersonServiceTest
    {
        private readonly IPersonService _personsService;

        public PersonServiceTest()
        {
            _personsService=new PersonService();
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

    }
}
