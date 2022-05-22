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
    [Route("api/vacancyskills")]
    [ApiController]
    public class VacancySkillsController : ControllerBase
    {
        private readonly IVacancySkillerRepo _repo;
        private readonly IMapper _mapper;

        public VacancySkillsController(IVacancySkillerRepo repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }


        /// <summary>
        /// GET api/vacancyskills
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [HttpGet("sort")]
        [HttpGet("sort/{type}")]
        public ActionResult<IEnumerable<VacancySkillReadDto>> GetAllVacancySkills([FromQuery] string fields = "", string type = "")
        {
            var vacancyskillItems = _repo.GetAllVacancySkills();
            if (fields != "") vacancyskillItems = Sort(vacancyskillItems, fields, type);
            return Ok(_mapper.Map<IEnumerable<VacancySkillReadDto>>(vacancyskillItems));
        }

        /// <summary>
        /// GET ../sort
        /// </summary>
        /// <returns></returns>
        private IEnumerable<VacancySkill> Sort(IEnumerable<VacancySkill> vacancyskills,string fields, string type = "")
        {
            List<VacancySkill> vacancyskillItems = (List<VacancySkill>)vacancyskills;
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
                        vacancyskillItems = vacancyskillItems.OrderByDescending(x => x.GetType().GetProperty(f[i]).GetValue(x)).ToList();
                    if (type == "")
                        vacancyskillItems = vacancyskillItems.OrderBy(x => x.GetType().GetProperty(f[i]).GetValue(x)).ToList();
                }

            }
            return vacancyskillItems;
        }
        /// <summary>
        /// GET api/vacancyskills
        /// </summary>
        /// <returns></returns>
        [HttpGet("{parametr}/{value}")]
        [HttpGet("{parametr}/{value}/sort/{type}")]
        [HttpGet("{parametr}/{value}/sort")]
        public ActionResult<IEnumerable<VacancySkillReadDto>> GetSelectedVacancySkills(string parametr, string value="", [FromQuery]string fields="", string type="")
        {
            var vacancyskillItems = _repo.GetAllVacancySkills();
            if (value != "")
            {
                parametr = parametr.Insert(0, Char.ToUpper(parametr[0]).ToString());
                parametr = parametr.Remove(1, 1);
                vacancyskillItems = vacancyskillItems.Where(x => x.GetType().GetProperty(parametr).GetValue(x).ToString() == value).ToList();
            }
            if (vacancyskillItems != null)
            {
                if (fields == "") fields = "id";
                vacancyskillItems = Sort(vacancyskillItems, fields, type);
            }
            else return NotFound();
            return Ok(_mapper.Map<IEnumerable<VacancySkillReadDto>>(vacancyskillItems));
        }
        /// <summary>
        /// GET api/vacancyskills/{id}
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public ActionResult<VacancySkill> GetVacancySkillById(int id)
        {
            var vacancyskillItem = _repo.GetVacancySkillById(id);
            if (vacancyskillItem != null)
            {
                return Ok(_mapper.Map<VacancySkillReadDto>(vacancyskillItem));
            }
            return NotFound();
        }

        [HttpPost]
        public ActionResult<VacancySkillCreateDto> CreateVacancySkill(VacancySkillCreateDto vacancyskillCreateDto)
        {
            var vacancyskillModel = _mapper.Map<VacancySkill>(vacancyskillCreateDto);
            _repo.CreateVacancySkill(vacancyskillModel);
            _repo.SaveChanges();

            var vacancyskillReadDto = _mapper.Map<VacancySkillReadDto>(vacancyskillModel);

            return CreatedAtRoute(new { Id = vacancyskillReadDto.Id }, vacancyskillReadDto);
            //return Ok(vacancyskillReadDto);
        }

        /// <summary>
        /// POST api/vacancyskills/{id}
        /// </summary>
        /// <param name="id"></param>
        /// <param name="vacancyskillUpdateDto"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public ActionResult UpdateVacancySkill(int id, VacancySkillUpdateDto vacancyskillUpdateDto)
        {
            var vacancyskillModelFromRepo = _repo.GetVacancySkillById(id);
            if (vacancyskillModelFromRepo == null)
                return NotFound();
            _mapper.Map(vacancyskillUpdateDto, vacancyskillModelFromRepo);

            _repo.UpdateVacancySkill(vacancyskillModelFromRepo);
            _repo.SaveChanges();
            return NoContent();
        }

        [HttpPatch("{id}")]
        public  ActionResult PartialVacancySkillUpdate(int id, JsonPatchDocument<VacancySkillUpdateDto> patchDocument)
        {
            var vacancyskillModelFromRepo = _repo.GetVacancySkillById(id);
            if (vacancyskillModelFromRepo == null)
                return NotFound();
            var vacancyskillToPatch = _mapper.Map<VacancySkillUpdateDto>(vacancyskillModelFromRepo);
            patchDocument.ApplyTo(vacancyskillToPatch, ModelState);
            if (!TryValidateModel(vacancyskillToPatch))
            {
                return ValidationProblem(ModelState);
            }
            _mapper.Map(vacancyskillToPatch, vacancyskillModelFromRepo);
            _repo.UpdateVacancySkill(vacancyskillModelFromRepo);
            _repo.SaveChanges();
            return NoContent();
        }

        /// <summary>
        /// DELETE api/vacancyskills/{id}
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public ActionResult DeleteVacancySkill(int id)
        {
            var vacancyskillModelFromRepo = _repo.GetVacancySkillById(id);
            if (vacancyskillModelFromRepo == null)
                return NotFound();
            _repo.DeleteVacancySkill(vacancyskillModelFromRepo);
            _repo.SaveChanges();
            return NoContent();
        }
    }
}
