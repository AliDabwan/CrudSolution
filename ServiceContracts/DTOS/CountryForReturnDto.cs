using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceContracts.DTOS
{
    public class CountryForReturnDto
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }


    }

    public static class CountryExtensions
    {
        public static CountryForReturnDto ToCountryForReturn(
            this Country country) => new() { Id = country.Id, Name = country.Name };
    }
}
