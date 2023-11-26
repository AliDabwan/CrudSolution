using ServiceContracts.DTOS;
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
    }
}
