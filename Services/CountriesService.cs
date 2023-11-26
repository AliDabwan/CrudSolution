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
    public class CountriesService : ICountriesService
    {
        private readonly List<Country> _countries;

        public CountriesService() { 

            _countries =new List<Country>();

        }
        public CountryForReturnDto AddCountry(CountryForCreateDto? countryForCreateDTO)
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
            if (_countries.Where(c => c.Name == countryForCreateDTO.Name).Any())
            {
                throw new ArgumentException("Given country name already exists");
            }

            //Convert object from CountryForCreateDTO to Country type
            Country country = countryForCreateDTO.ToCountry();

            //generate Id
            country.Id = Guid.NewGuid();

            //Validation: Every thing is ok , Add country object into _countries
            _countries.Add(country);
            return country.ToCountryForReturn();
        }
    }
}
