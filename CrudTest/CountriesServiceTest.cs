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

        #region GetAllCountries

        [Fact]
        //The list of countries should be empty by default (before adding any countries)
        public void GetAllCountries_EmptyList()
        {
            //Act
            List<CountryForReturnDto> actualCountryForReturnList = _countriesService.GetAllCountries();

            //Assert
            Assert.Empty(actualCountryForReturnList);
        }

        [Fact]
        public void GetAllCountries_AddFewCountries()
        {
            //Arrange
            List<CountryForCreateDto> countryForCreateList = new() {
                    new(){ Name = "Iraq" },
                    new(){ Name = "Syria" }};
            //Act  expected CountryForReturnDTO List
            List<CountryForReturnDto> expected_countryForReturnDTO_List = new();

            countryForCreateList.ForEach(cf => expected_countryForReturnDTO_List.Add(_countriesService.AddCountry(cf)));

            List<CountryForReturnDto> actual_CountryForReturnDTO_List = 
                _countriesService.GetAllCountries();

            //Check that actual_CountryForReturnDTO_List contains All expected_countryForReturn_List items 
            expected_countryForReturnDTO_List.ForEach(ec => Assert.Contains(ec, actual_CountryForReturnDTO_List));
        }
        #endregion


    }
}
