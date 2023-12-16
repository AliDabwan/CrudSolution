using Entities;
using ServiceContracts.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ServiceContracts.DTOS
{
    public class CountryForReturnDto
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }

        public override bool Equals(object? obj)
        {
            if (obj == null)
            {
                return false;
            }

            if (obj.GetType() != typeof(CountryForReturnDto))
            {
                return false;
            }

            CountryForReturnDto country_to_compare = (CountryForReturnDto)obj;

            return Id == country_to_compare.Id && Name == country_to_compare.Name;
        }

        public override int GetHashCode()
        {
           return Id.GetHashCode();
        }
       

    }

    public static class CountryExtensions
    {
        public static CountryForReturnDto ToCountryForReturn(
            this Country country) => new() { Id = country.Id, Name = country.Name };
    }
}
