﻿using ServiceContracts.DTOS;
using ServiceContracts.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceContracts.Interfaces
{
    public interface IPersonService
    {
        Task<PersonForReturnDTO> AddPerson(PersonForCreateDTO personForCreateDTO);

         Task<List<PersonForReturnDTO>> GetAllPersons();
         Task<PersonForReturnDTO?> GetPersonById(Guid? id);

         Task<List<PersonForReturnDTO>> GetFilteredPersons(string searchBy, string? searchString);

         List<PersonForReturnDTO> GetSortedPersons(List<PersonForReturnDTO> allPersons, string sortBy, SortOrderOptions sortOrder);

        Task<PersonForReturnDTO> UpdatePerson(PersonForUpdateDto personForUpdateDto);
        Task<bool> DeletePerson(Guid? id);
    }
}
