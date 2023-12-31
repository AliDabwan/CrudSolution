﻿using Entities;
using Microsoft.EntityFrameworkCore;
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
            _countriesService = new CountriesService(new CRUDDbContext(
                new DbContextOptionsBuilder<CRUDDbContext>().Options ));
        }
        #region AddCountry
        //When CountryForCreateDTO is null, it should throw ArgumentNullException
        [Fact]
        public async Task AddCountry_NullCountry()
        {
            //Arrange
            CountryForCreateDto? countryForCreateDTO = null;

            //Assert
          await  Assert.ThrowsAsync<ArgumentNullException>(async() =>
            {
                //Act
              await  _countriesService.AddCountry(countryForCreateDTO);
            });
        }

        //When the Name is null, it should throw ArgumentException
        [Fact]
        public async Task AddCountry_NameIsNull()
        {
            //Arrange
            CountryForCreateDto? countryForCreateDTO = new CountryForCreateDto() { Name = null };

            //Assert
           await Assert.ThrowsAsync<ArgumentException>(async () =>
            {
                //Act
              await  _countriesService.AddCountry(countryForCreateDTO);
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
        public async Task AddCountry_ProperCountryDetails()
        {
            //Arrange
            CountryForCreateDto? countryForCreateDTO = new () { Name = "Palestine" };

            //Act
            CountryForReturnDto countryForReturnDTO =await _countriesService.AddCountry(countryForCreateDTO);

            List<CountryForReturnDto> actual_CountryForReturnDTO_List =
             await _countriesService.GetAllCountries();
            //Assert
            Assert.True(countryForReturnDTO.Id != Guid.Empty);

            Assert.Contains(countryForReturnDTO, actual_CountryForReturnDTO_List);
            //actual_CountryForReturnDTO_List
            //    .ForEach(ac => Assert.Equivalent(ac, countryForReturnDTO));
        }

        #endregion

        #region GetAllCountries

        [Fact]
        //The list of countries should be empty by default (before adding any countries)
        public async Task GetAllCountries_EmptyList()
        {
            //Act
            List<CountryForReturnDto> actualCountryForReturnList =await _countriesService.GetAllCountries();

            //Assert
            Assert.Empty(actualCountryForReturnList);
        }

        [Fact]
        public async void GetAllCountries_AddFewCountries()
        {
            //Arrange
            List<CountryForCreateDto> countryForCreateList = new() {
                    new(){ Name = "Iraq" },
                    new(){ Name = "Syria" }};
            //Act  expected CountryForReturnDTO List
            List<CountryForReturnDto> expected_countryForReturnDTO_List = new();

            countryForCreateList.ForEach(async cf => expected_countryForReturnDTO_List.Add(await _countriesService.AddCountry(cf)));

            List<CountryForReturnDto> actual_CountryForReturnDTO_List = 
               await _countriesService.GetAllCountries();

            //Check that actual_CountryForReturnDTO_List contains All expected_countryForReturn_List items 
            expected_countryForReturnDTO_List.ForEach(ec => Assert.Contains(ec, actual_CountryForReturnDTO_List));
        }
        #endregion


        #region GetCountryById
        [Fact]
        //If we supply null as Country Id, it should return null as CountryForReturnDTO
        public async Task GetCountryByID_NullId()
        {
            //Arrange
            Guid? Id = null;

            //Act
            CountryForReturnDto? countryForReturnDTO =await _countriesService.GetCountryById(Id);

            //Assert
            Assert.Null(countryForReturnDTO);
        }


        [Fact]
        //If we supply a valid country id, it should return the matching country details as CountryForReturnDTO object
        public async Task GetCountryByID_ValidId()
        {
            //Arrange
            CountryForCreateDto? countryForCreateDTO = new() { Name = "Libya" };
            CountryForReturnDto countryForReturnDTO_From_Add =await _countriesService.AddCountry(countryForCreateDTO);

            //Act
            CountryForReturnDto? countryForReturnDTO_From_Get =await _countriesService.GetCountryById(countryForReturnDTO_From_Add.Id);

            //Assert
            Assert.Equal(countryForReturnDTO_From_Add, countryForReturnDTO_From_Get);
        }
        #endregion
    }
}
