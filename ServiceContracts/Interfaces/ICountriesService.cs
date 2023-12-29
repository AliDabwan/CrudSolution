using ServiceContracts.DTOS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceContracts.Interfaces
{
    public interface ICountriesService
    {
       Task<CountryForReturnDto> AddCountry(CountryForCreateDto? country);

        Task<List<CountryForReturnDto>> GetAllCountries();
        Task<CountryForReturnDto?> GetCountryById(Guid? id);
    }
}
