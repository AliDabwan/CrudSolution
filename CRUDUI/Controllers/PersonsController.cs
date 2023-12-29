using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Rotativa.AspNetCore;
using ServiceContracts.DTOS;
using ServiceContracts.Enums;
using ServiceContracts.Interfaces;
using Services;

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

        public async Task<IActionResult> Index(string searchBy, string? searchString, string sortBy = nameof(PersonForReturnDTO.Name), SortOrderOptions sortOrder = SortOrderOptions.ASC)
        {
            ViewBag.SearchFields = new Dictionary<string, string>()
            {
                {nameof(PersonForReturnDTO.Name),"Person Name" },
                {nameof(PersonForReturnDTO.Email),"Email" },
                {nameof(PersonForReturnDTO.DateOfBirth),"Date of Birth" },
                {nameof(PersonForReturnDTO.Gender),"Gender" },
                {nameof(PersonForReturnDTO.Country),"Country" },

            };
            //search
            List<PersonForReturnDTO> persons =await _personService.GetFilteredPersons(searchBy, searchString);

            ViewBag.CurrentSearchBy = searchBy;
            ViewBag.CurrentSearchString = searchString;


            //sort

            List<PersonForReturnDTO> sortedPersons = _personService.GetSortedPersons(persons, sortBy, sortOrder);

            ViewBag.CurrentSortBy = sortBy;
            ViewBag.CurrentSortOrder = sortOrder.ToString();

            return View(sortedPersons);
        }
        [Route("/persons/create")]
        [HttpGet]
        public async Task<IActionResult> Create() {
            List<CountryForReturnDto>countries =await _countriesService.GetAllCountries();
            //ViewBag.Countries = countries;
            ViewBag.Countries = countries.Select(c=>new SelectListItem
            {
                Text= c.Name,
                Value=c.Id.ToString()

            });


            return View(); }

        [Route("/persons/create")]
        [HttpPost]
        public async Task<IActionResult> Create(PersonForCreateDTO person)
        {
            if(!ModelState.IsValid)
            {
                List<CountryForReturnDto> countries =await _countriesService.GetAllCountries();
                ViewBag.Countries = countries.Select(c => new SelectListItem
                {
                    Text = c.Name,
                    Value = c.Id.ToString()

                });
                ViewBag.Errors = ModelState.Values.SelectMany(v => v.Errors)
                    .Select(v => v.ErrorMessage).ToList() ;
                return View();

            }
          await  _personService.AddPerson(person);
            return RedirectToAction("Index","Persons");
        }

        [Route("/persons/update/{id}")]
        [HttpGet]
        public async Task<IActionResult> Update(Guid id)
        {
            PersonForReturnDTO? personForReturnDTO =await _personService.GetPersonById(id);

            if(personForReturnDTO == null)
            {
                return RedirectToAction("index");
            }

            PersonForUpdateDto personForUpdateDto=personForReturnDTO.ToPersonForUpdateDTO();
            List<CountryForReturnDto> countries =await _countriesService.GetAllCountries();
            //ViewBag.Countries = countries;
            ViewBag.Countries = countries.Select(c => new SelectListItem
            {
                Text = c.Name,
                Value = c.Id.ToString()

            });


            return View(personForUpdateDto);
        }

        [Route("/persons/update/{id}")]
        [HttpPost]
        public async Task<IActionResult> Update(PersonForUpdateDto personForUpdateDto)
        {
            PersonForReturnDTO? personForReturnDTO =await _personService.GetPersonById(personForUpdateDto.Id);

            if (personForReturnDTO == null)
            {
                return RedirectToAction("index");
            }
            if (!ModelState.IsValid)
            {
                List<CountryForReturnDto> countries =await _countriesService.GetAllCountries();
                ViewBag.Countries = countries.Select(c => new SelectListItem
                {
                    Text = c.Name,
                    Value = c.Id.ToString()

                });
                ViewBag.Errors = ModelState.Values.SelectMany(v => v.Errors)
                    .Select(v => v.ErrorMessage).ToList();
                return View();

            }
           await _personService.UpdatePerson(personForUpdateDto);
            return RedirectToAction("Index", "Persons");
        }



        [Route("/persons/delete/{id}")]
        [HttpGet]
        public async Task<IActionResult> Delete(Guid id)
        {
            PersonForReturnDTO? personForReturnDTO =await _personService.GetPersonById(id);

            if (personForReturnDTO == null)
            {
                return RedirectToAction("index");
            }

           

            return View(personForReturnDTO);
        }

        [Route("/persons/delete/{id}")]
        [HttpPost]
        public async Task<IActionResult> Delete(PersonForReturnDTO personForReturnDTO)
        {
            PersonForReturnDTO? personFordelete =await _personService.GetPersonById(personForReturnDTO.Id);

            if (personFordelete == null)
            {
                return RedirectToAction("index");
            }
           
          await  _personService.DeletePerson(personFordelete.Id);
            return RedirectToAction("Index", "Persons");
        }

        [Route("PersonsPdf")]
        public async Task<IActionResult> PersonsPdf()
        {
            //Get list of persons
            List<PersonForReturnDTO> persons =
                await _personService.GetAllPersons();

            //Return view as pdf
            return new ViewAsPdf("PersonsPdf", persons, ViewData)
            {
                PageMargins = new Rotativa.AspNetCore.Options.Margins() { Top = 20, Right = 20, Bottom = 20, Left = 20 },
                PageOrientation = Rotativa.AspNetCore.Options.Orientation.Landscape,
                FileName = "PersonsPdf_" + DateTime.Now.ToString("mm-dd HH:MM:ss") + ".pdf"



            };
        }




    }


}
