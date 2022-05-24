using API.Data.Base;
using API.Dtos;
using API.Models;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System;
using Microsoft.AspNetCore.JsonPatch;

namespace API.Controllers
{
    [Route("api/vacancies")]
    [ApiController]
    public class VacanciesController : ControllerBase
    {
        private readonly IVacancierRepo _repo;
        private readonly IMapper _mapper;

        public VacanciesController(IVacancierRepo repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }


        /// <summary>
        /// GET api/vacancies
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [HttpGet("sort")]
        [HttpGet("sort/{type}")]
        [HttpGet("period/{from}/{count}")]
        public ActionResult<IEnumerable<VacancyReadDto>> GetAllVacancies([FromQuery] string fields = "", string type = "", int from=0, int count=50)
        {
            var vacancyItems = _repo.GetAllVacancies();
            if (fields != "") vacancyItems = Sort(vacancyItems, fields, type);
            vacancyItems = vacancyItems.Skip(from).Take(count).ToList();
            return Ok(_mapper.Map<IEnumerable<VacancyReadDto>>(vacancyItems));
        }

        /// <summary>
        /// GET ../sort
        /// </summary>
        /// <returns></returns>
        private IEnumerable<Vacancy> Sort(IEnumerable<Vacancy> vacancies,string fields, string type = "")
        {
            List<Vacancy> vacancyItems = (List<Vacancy>)vacancies;
            if (fields != "")
            {
                var f = fields.Split(',');
                f = f.Where(x => x != "" && x != " ").ToArray();

                for (int i = 0; i < f.Length; i++)
                {
                    if (f[i].ToLower() != "name" || f[i].ToLower() != "id" || f[i].ToLower()!="idarea" 
                        || f[i].ToLower() != "salaryfrom" || f[i].ToLower() != "salaryto" || f[i].ToLower() != "salarycurrency"
                        || f[i].ToLower() != "publishat" || f[i].ToLower() != "snippetrequirement" || f[i].ToLower() != "snippetrequirement"
                        || f[i].ToLower() != "description" || f[i].ToLower() != "idexperience") continue;
                    f[i] = f[i].Insert(0, Char.ToUpper(f[i][0]).ToString());
                    f[i] = f[i].Remove(1, 1);
                    if (type == "desc")
                        vacancyItems = vacancyItems.OrderByDescending(x => x.GetType().GetProperty(f[i]).GetValue(x)).ToList();
                    if (type == "")
                        vacancyItems = vacancyItems.OrderBy(x => x.GetType().GetProperty(f[i]).GetValue(x)).ToList();
                }

            }
            return vacancyItems;
        }
        /// <summary>
        /// GET api/vacancies
        /// </summary>
        /// <returns></returns>
        [HttpGet("{parametr}/{value}")]
        [HttpGet("{parametr}/{value}/sort/{type}")]
        [HttpGet("{parametr}/{value}/sort")]
        public ActionResult<IEnumerable<VacancyReadDto>> GetSelectedVacancies(string parametr, string value="", [FromQuery]string fields="", string type="")
        {
            IEnumerable<Vacancy> vacancyItems = null;
            if (value != "")
            {
                parametr = parametr.Insert(0, Char.ToUpper(parametr[0]).ToString());
                parametr = parametr.Remove(1, 1);
                vacancyItems = (IEnumerable<Vacancy>)_repo.GetType().GetMethod("GetVacanciesBy" + parametr).Invoke(_repo, new object[] { value });
            }
            if (vacancyItems != null && vacancyItems.Count() != 0)
            {
                if (fields == "") fields = "id";
                vacancyItems = Sort(vacancyItems, fields, type);
            }
            else return NotFound();
            return Ok(_mapper.Map<IEnumerable<VacancyReadDto>>(vacancyItems));
        }
        /// <summary>
        /// GET api/vacancies/{id}
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public ActionResult<Vacancy> GetVacancyById(int id)
        {
            var vacancyItem = _repo.GetVacancyById(id);
            if (vacancyItem != null)
            {
                return Ok(_mapper.Map<VacancyReadDto>(vacancyItem));
            }
            return NotFound();
        }

        [HttpPost]
        public ActionResult<VacancyCreateDto> CreateVacancy(VacancyCreateDto vacancyCreateDto)
        {
            var vacancyModel = _mapper.Map<Vacancy>(vacancyCreateDto);
            _repo.CreateVacancy(vacancyModel);
            _repo.SaveChanges();

            var vacancyReadDto = _mapper.Map<VacancyReadDto>(vacancyModel);

            return CreatedAtRoute(new { Id = vacancyReadDto.Id }, vacancyReadDto);
            //return Ok(vacancyReadDto);
        }

        /// <summary>
        /// POST api/vacancies/{id}
        /// </summary>
        /// <param name="id"></param>
        /// <param name="vacancyUpdateDto"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public ActionResult UpdateVacancy(int id, VacancyUpdateDto vacancyUpdateDto)
        {
            var vacancyModelFromRepo = _repo.GetVacancyById(id);
            if (vacancyModelFromRepo == null)
                return NotFound();
            _mapper.Map(vacancyUpdateDto, vacancyModelFromRepo);

            _repo.UpdateVacancy(vacancyModelFromRepo);
            _repo.SaveChanges();
            return NoContent();
        }

        [HttpPatch("{id}")]
        public  ActionResult PartialVacancyUpdate(int id, JsonPatchDocument<VacancyUpdateDto> patchDocument)
        {
            var vacancyModelFromRepo = _repo.GetVacancyById(id);
            if (vacancyModelFromRepo == null)
                return NotFound();
            var vacancyToPatch = _mapper.Map<VacancyUpdateDto>(vacancyModelFromRepo);
            patchDocument.ApplyTo(vacancyToPatch, ModelState);
            if (!TryValidateModel(vacancyToPatch))
            {
                return ValidationProblem(ModelState);
            }
            _mapper.Map(vacancyToPatch, vacancyModelFromRepo);
            _repo.UpdateVacancy(vacancyModelFromRepo);
            _repo.SaveChanges();
            return NoContent();
        }

        /// <summary>
        /// DELETE api/vacancies/{id}
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public ActionResult DeleteVacancy(int id)
        {
            var vacancyModelFromRepo = _repo.GetVacancyById(id);
            if (vacancyModelFromRepo == null)
                return NotFound();
            _repo.DeleteVacancy(vacancyModelFromRepo);
            _repo.SaveChanges();
            return NoContent();
        }
    }
}
