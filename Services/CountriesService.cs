﻿using Entities;
using Microsoft.EntityFrameworkCore;
using ServiceContracts.DTOS;
using ServiceContracts.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class CountriesService : ICountriesService
    {
        //private readonly List<Country> _countries;
        private readonly CRUDDbContext _dbContext;

        public CountriesService(CRUDDbContext dbContext ) {
            _dbContext = dbContext;

            //_countries =new List<Country>();

            //if (initialize)
            //{
            //    _countries.AddRange(new List<Country>() {
            //    new() {  Id = Guid.Parse("000C76EB-62E9-4465-96D1-2C41FDB64C3B"), Name = "Yemen" },

            //    new() { Id = Guid.Parse("32DA506B-3EBA-48A4-BD86-5F93A2E19E3F"), Name = "Palestine" },

            //    new() { Id = Guid.Parse("DF7C89CE-3341-4246-84AE-E01AB7BA476E"), Name = "Iraq" },

            //    new() { Id = Guid.Parse("15889048-AF93-412C-B8F3-22103E943A6D"), Name = "Syria" },

            //    new() { Id = Guid.Parse("80DF255C-EFE7-49E5-A7F9-C35D7C701CAB"), Name = "Libya" }
            //    });
            //}


        }
        public async Task<CountryForReturnDto> AddCountry(CountryForCreateDto? countryForCreateDTO)
        {
            //Validation: countryForCreateDTO parameter can't be null
            if (countryForCreateDTO == null)
            {
                throw new ArgumentNullException(nameof(CountryForCreateDto));
            }

            //Validation: Name can't be null
            if (countryForCreateDTO.Name == null)
            {
                throw new ArgumentException(nameof(CountryForCreateDto.Name));
            }

            //Validation: Name can't be duplicate
            if (_dbContext.Countries.Where(c => c.Name == countryForCreateDTO.Name).Any())
            {
                throw new ArgumentException("Given country name already exists");
            }

            //Convert object from CountryForCreateDTO to Country type
            Country country = countryForCreateDTO.ToCountry();

            //generate Id
            country.Id = Guid.NewGuid();

            //Validation: Every thing is ok , Add country object into _countries
            _dbContext.Countries.Add(country);
           await _dbContext.SaveChangesAsync();
            return country.ToCountryForReturn();
        }

        public async Task<List<CountryForReturnDto>> GetAllCountries()
        {
            return await _dbContext.Countries.Select(c=>c.ToCountryForReturn()).ToListAsync();
        }

        public async Task<CountryForReturnDto?> GetCountryById(Guid? id)
        {
            if (id == null)
            {
                return null;
            }
            Country? country=await _dbContext.Countries.FirstOrDefaultAsync(c => c.Id==id);
            if (country == null) { return null; }
            return country.ToCountryForReturn();
        }
    }
}
