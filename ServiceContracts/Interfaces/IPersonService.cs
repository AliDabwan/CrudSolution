using ServiceContracts.DTOS;
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
        PersonForReturnDTO AddPerson(PersonForCreateDTO personForCreateDTO);

        List<PersonForReturnDTO> GetAllPersons();
        PersonForReturnDTO? GetPersonById(Guid? id);

        List<PersonForReturnDTO> GetFilteredPersons(string searchBy, string? searchString);

        List<PersonForReturnDTO> GetSortedPersons(List<PersonForReturnDTO> allPersons, string sortBy, SortOrderOptions sortOrder);

        PersonForReturnDTO UpdatePerson(PersonForUpdateDto personForUpdateDto);
        bool DeletePerson(Guid? id);
    }
}
