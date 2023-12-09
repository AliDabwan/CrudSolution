using Microsoft.AspNetCore.Mvc;
using ServiceContracts.DTOS;
using ServiceContracts.Interfaces;

namespace CRUDUI.Controllers
{
    public class PersonsController : Controller
    {
        private readonly ICountriesService _countriesService;
        private readonly IPersonService _personService;

        public PersonsController(ICountriesService countriesService,IPersonService personService)
        {
            _countriesService = countriesService;
            _personService = personService;
        }

        [Route("/")]
        [Route("persons")]
        [Route("persons/index")]

        public IActionResult Index(string searchBy,string? searchString)
        {
            ViewBag.SearchFields = new Dictionary<string, string>()
            {
                {nameof(PersonForReturnDTO.Name),"Person Name" },
                {nameof(PersonForReturnDTO.Email),"Person Name" },
                {nameof(PersonForReturnDTO.DateOfBirth),"Date of Birth" },
                {nameof(PersonForReturnDTO.Gender),"Gender" },

            };
           List<PersonForReturnDTO> persons=_personService.GetAllPersons();

            ViewBag.CurrentSearchBy = searchBy;
            ViewBag.CurrentSearchString = searchString;
            return View(persons);
        }
    }
}
