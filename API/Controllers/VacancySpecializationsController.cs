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
    [Route("api/vacancyspecializations")]
    [ApiController]
    public class VacancySpecializationsController : ControllerBase
    {
        private readonly IVacancySpecializationerRepo _repo;
        private readonly IMapper _mapper;

        public VacancySpecializationsController(IVacancySpecializationerRepo repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }


        /// <summary>
        /// GET api/vacancyspecializations
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [HttpGet("sort")]
        [HttpGet("sort/{type}")]
        public ActionResult<IEnumerable<VacancySpecializationReadDto>> GetAllVacancySpecializations([FromQuery] string fields = "", string type = "")
        {
            var vacancyspecializationItems = _repo.GetAllVacancySpecializations();
            if (fields != "") vacancyspecializationItems = Sort(vacancyspecializationItems, fields, type);
            return Ok(_mapper.Map<IEnumerable<VacancySpecializationReadDto>>(vacancyspecializationItems));
        }

        /// <summary>
        /// GET ../sort
        /// </summary>
        /// <returns></returns>
        private IEnumerable<VacancySpecialization> Sort(IEnumerable<VacancySpecialization> vacancyspecializations,string fields, string type = "")
        {
            List<VacancySpecialization> vacancyspecializationItems = (List<VacancySpecialization>)vacancyspecializations;
            if (fields != "")
            {
                var f = fields.Split(',');
                f = f.Where(x => x != "" && x != " ").ToArray();

                for (int i = 0; i < f.Length; i++)
                {
                    if (f[i].ToLower() != "name" || f[i].ToLower() != "id") continue;
                    f[i] = f[i].Insert(0, Char.ToUpper(f[i][0]).ToString());
                    f[i] = f[i].Remove(1, 1);
                    if (type == "desc")
                        vacancyspecializationItems = vacancyspecializationItems.OrderByDescending(x => x.GetType().GetProperty(f[i]).GetValue(x)).ToList();
                    if (type == "")
                        vacancyspecializationItems = vacancyspecializationItems.OrderBy(x => x.GetType().GetProperty(f[i]).GetValue(x)).ToList();
                }

            }
            return vacancyspecializationItems;
        }
        /// <summary>
        /// GET api/vacancyspecializations
        /// </summary>
        /// <returns></returns>
        [HttpGet("{parametr}/{value}")]
        [HttpGet("{parametr}/{value}/sort/{type}")]
        [HttpGet("{parametr}/{value}/sort")]
        public ActionResult<IEnumerable<VacancySpecializationReadDto>> GetSelectedVacancySpecializations(string parametr, string value="", [FromQuery]string fields="", string type="")
        {
            var vacancyspecializationItems = _repo.GetAllVacancySpecializations();
            if (value != "")
            {
                parametr = parametr.Insert(0, Char.ToUpper(parametr[0]).ToString());
                parametr = parametr.Remove(1, 1);
                vacancyspecializationItems = vacancyspecializationItems.Where(x => x.GetType().GetProperty(parametr).GetValue(x).ToString() == value).ToList();
            }
            if (vacancyspecializationItems != null)
            {
                if (fields == "") fields = "id";
                vacancyspecializationItems = Sort(vacancyspecializationItems, fields, type);
            }
            else return NotFound();
            return Ok(_mapper.Map<IEnumerable<VacancySpecializationReadDto>>(vacancyspecializationItems));
        }
        /// <summary>
        /// GET api/vacancyspecializations/vacancy/{id}
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("vacancy/{id}")]
        public ActionResult<VacancySpecialization> GetVacancySpecializationByVacancyId(int id)
        {
            var vacancyspecializationItem = _repo.GetVacancySpecializationByVacancyId(id);
            if (vacancyspecializationItem != null)
            {
                return Ok(_mapper.Map<VacancySpecializationReadDto>(vacancyspecializationItem));
            }
            return NotFound();
        }
        [HttpGet("specialization/{id}")]
        public ActionResult<VacancySpecialization> GetVacancySpecializationBySpecializationId(int id)
        {
            var vacancyspecializationItem = _repo.GetVacancySpecializationByVacancyId(id);
            if (vacancyspecializationItem != null)
            {
                return Ok(_mapper.Map<VacancySpecializationReadDto>(vacancyspecializationItem));
            }
            return NotFound();
        }

        [HttpPost]
        public ActionResult<VacancySpecializationCreateDto> CreateVacancySpecialization(VacancySpecializationCreateDto vacancyspecializationCreateDto)
        {
            var vacancyspecializationModel = _mapper.Map<VacancySpecialization>(vacancyspecializationCreateDto);
            _repo.CreateVacancySpecialization(vacancyspecializationModel);
            _repo.SaveChanges();

            var vacancyspecializationReadDto = _mapper.Map<VacancySpecializationReadDto>(vacancyspecializationModel);

            return CreatedAtRoute(new { Id = vacancyspecializationReadDto.Id }, vacancyspecializationReadDto);
            //return Ok(vacancyspecializationReadDto);
        }

        /// <summary>
        /// POST api/vacancyspecializations/{id}
        /// </summary>
        /// <param name="id"></param>
        /// <param name="vacancyspecializationUpdateDto"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public ActionResult UpdateVacancySpecialization(int id, VacancySpecializationUpdateDto vacancyspecializationUpdateDto)
        {
            var vacancyspecializationModelFromRepo = _repo.GetVacancySpecializationById(id);
            if (vacancyspecializationModelFromRepo == null)
                return NotFound();
            _mapper.Map(vacancyspecializationUpdateDto, vacancyspecializationModelFromRepo);

            _repo.UpdateVacancySpecialization(vacancyspecializationModelFromRepo);
            _repo.SaveChanges();
            return NoContent();
        }

        [HttpPatch("{id}")]
        public  ActionResult PartialVacancySpecializationUpdate(int id, JsonPatchDocument<VacancySpecializationUpdateDto> patchDocument)
        {
            var vacancyspecializationModelFromRepo = _repo.GetVacancySpecializationById(id);
            if (vacancyspecializationModelFromRepo == null)
                return NotFound();
            var vacancyspecializationToPatch = _mapper.Map<VacancySpecializationUpdateDto>(vacancyspecializationModelFromRepo);
            patchDocument.ApplyTo(vacancyspecializationToPatch, ModelState);
            if (!TryValidateModel(vacancyspecializationToPatch))
            {
                return ValidationProblem(ModelState);
            }
            _mapper.Map(vacancyspecializationToPatch, vacancyspecializationModelFromRepo);
            _repo.UpdateVacancySpecialization(vacancyspecializationModelFromRepo);
            _repo.SaveChanges();
            return NoContent();
        }

        /// <summary>
        /// DELETE api/vacancyspecializations/{id}
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public ActionResult DeleteVacancySpecialization(int id)
        {
            var vacancyspecializationModelFromRepo = _repo.GetVacancySpecializationById(id);
            if (vacancyspecializationModelFromRepo == null)
                return NotFound();
            _repo.DeleteVacancySpecialization(vacancyspecializationModelFromRepo);
            _repo.SaveChanges();
            return NoContent();
        }
    }
}
