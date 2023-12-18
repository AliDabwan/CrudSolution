using Microsoft.AspNetCore.Mvc;
using ServiceContracts.DTOS;
using ServiceContracts.Enums;
using ServiceContracts.Interfaces;

namespace CRUDUI.Controllers
{
    public class PersonsController : Controller
    {
        private readonly ICountriesService _countriesService;
        private readonly IPersonService _personService;

        public PersonsController(ICountriesService countriesService, IPersonService personService)
        {
            _countriesService = countriesService;
            _personService = personService;
        }

        [Route("/")]
        [Route("persons")]
        [Route("persons/index")]

        public IActionResult Index(string searchBy, string? searchString, string sortBy = nameof(PersonForReturnDTO.Name), SortOrderOptions sortOrder = SortOrderOptions.ASC)
        {
            ViewBag.SearchFields = new Dictionary<string, string>()
            {
                {nameof(PersonForReturnDTO.Name),"Person Name" },
                {nameof(PersonForReturnDTO.Email),"Person Name" },
                {nameof(PersonForReturnDTO.DateOfBirth),"Date of Birth" },
                {nameof(PersonForReturnDTO.Gender),"Gender" },

            };
            //search
            List<PersonForReturnDTO> persons = _personService.GetFilteredPersons(searchBy, searchString);

            ViewBag.CurrentSearchBy = searchBy;
            ViewBag.CurrentSearchString = searchString;


            //sort

            List<PersonForReturnDTO> sortedPersons = _personService.GetSortedPersons(persons, sortBy, sortOrder);

            ViewBag.CurrentSortBy = sortBy;
            ViewBag.CurrentSortOrder = sortOrder.ToString();

            return View(sortedPersons);
        }
    }
}
