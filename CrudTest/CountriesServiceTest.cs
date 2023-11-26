using ServiceContracts.DTOS;
using ServiceContracts.Interfaces;
using Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrudTest
{
    public class CountriesServiceTest
    {
        private readonly ICountriesService _countriesService;

        public CountriesServiceTest()
        {
            _countriesService = new CountriesService();
        }
        #region AddCountry
        //When CountryForCreateDTO is null, it should throw ArgumentNullException
        [Fact]
        public void AddCountry_NullCountry()
        {
            //Arrange
            CountryForCreateDto? countryForCreateDTO = null;

            //Assert
            Assert.Throws<ArgumentNullException>(() =>
            {
                //Act
                _countriesService.AddCountry(countryForCreateDTO);
            });
        }

        //When the Name is null, it should throw ArgumentException
        [Fact]
        public void AddCountry_NameIsNull()
        {
            //Arrange
            CountryForCreateDto? countryForCreateDTO = new CountryForCreateDto() { Name = null };

            //Assert
            Assert.Throws<ArgumentException>(() =>
            {
                //Act
                _countriesService.AddCountry(countryForCreateDTO);
            });
        }

        //When the Name is duplicate, it should throw ArgumentException
        [Fact]
        public void AddCountry_DuplicateName()
        {
            //Arrange
            CountryForCreateDto? countryForCreateDTO1 = new CountryForCreateDto() { Name = "Egypt" };
            CountryForCreateDto? countryForCreateDTO2 = new CountryForCreateDto() { Name = "Egypt" };

            //Assert
            Assert.Throws<ArgumentException>(() =>
            {
                //Act
                _countriesService.AddCountry(countryForCreateDTO1);
                _countriesService.AddCountry(countryForCreateDTO2);
            });
        }

        //When you supply proper country name, it should insert (add) the country to the existing list of countries
        [Fact]
        public void AddCountry_ProperCountryDetails()
        {
            //Arrange
            CountryForCreateDto? countryForCreateDTO = new () { Name = "Palestine" };

            //Act
            CountryForReturnDto countryForReturnDTO = _countriesService.AddCountry(countryForCreateDTO);

            //Assert
            Assert.True(countryForReturnDTO.Id != Guid.Empty);
        }

        #endregion
    }
}
