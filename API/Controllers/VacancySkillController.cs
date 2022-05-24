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
    [Route("api/vskills")]
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
        /// GET api/vskills
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [HttpGet("sort")]
        [HttpGet("sort/{type}")]
        public ActionResult<IEnumerable<VacancySkillReadDto>> GetAllVacancySkills([FromQuery] string fields = "", string type = "")
        {
            var vskillItems = _repo.GetAllVacancySkills();
            if (fields != "") vskillItems = Sort(vskillItems, fields, type);
            return Ok(_mapper.Map<IEnumerable<VacancySkillReadDto>>(vskillItems));
        }

        /// <summary>
        /// GET ../sort
        /// </summary>
        /// <returns></returns>
        private IEnumerable<VacancySkill> Sort(IEnumerable<VacancySkill> vskills,string fields, string type = "")
        {
            List<VacancySkill> vskillItems = (List<VacancySkill>)vskills;
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
                        vskillItems = vskillItems.OrderByDescending(x => x.GetType().GetProperty(f[i]).GetValue(x)).ToList();
                    if (type == "")
                        vskillItems = vskillItems.OrderBy(x => x.GetType().GetProperty(f[i]).GetValue(x)).ToList();
                }

            }
            return vskillItems;
        }
        /// <summary>
        /// GET api/vskills
        /// </summary>
        /// <returns></returns>
        [HttpGet("{parametr}/{value}")]
        [HttpGet("{parametr}/{value}/sort/{type}")]
        [HttpGet("{parametr}/{value}/sort")]
        public ActionResult<IEnumerable<VacancySkillReadDto>> GetSelectedVacancySkills(string parametr, string value="", [FromQuery]string fields="", string type="")
        {
            IEnumerable<VacancySkill> vskillItems = null;
            if (value != "")
            {
                parametr = parametr.Insert(0, Char.ToUpper(parametr[0]).ToString());
                parametr = parametr.Remove(1, 1);
                vskillItems = (IEnumerable<VacancySkill>)_repo.GetType().GetMethod("GetVacancySkillsById" + parametr).Invoke(_repo, new object[] { value });
            }
            if (vskillItems != null && vskillItems.Count() != 0)
            {
                if (fields == "") fields = "id";
                vskillItems = Sort(vskillItems, fields, type);
            }
            else return NotFound();
            return Ok(_mapper.Map<IEnumerable<VacancySkillReadDto>>(vskillItems));
        }
        

        [HttpPost]
        public ActionResult<VacancySkillCreateDto> CreateVacancySkill(VacancySkillCreateDto vskillCreateDto)
        {
            var vskillModel = _mapper.Map<VacancySkill>(vskillCreateDto);
            _repo.CreateVacancySkill(vskillModel);
            _repo.SaveChanges();

            var vskillReadDto = _mapper.Map<VacancySkillReadDto>(vskillModel);

            return CreatedAtRoute(new { VId = vskillReadDto.IdVacancy, SId=vskillCreateDto.IdSkill }, vskillReadDto);
            //return Ok(vskillReadDto);
        }

        /// <summary>
        /// POST api/vskills/{id}
        /// </summary>
        /// <param name="id"></param>
        /// <param name="vskillUpdateDto"></param>
        /// <returns></returns>
        [HttpPut("{vid}/{sid}")]
        public ActionResult UpdateVacancySkill(string vid, string sid, VacancySkillUpdateDto vskillUpdateDto)
        {
            var vskillModelFromRepo = _repo.GetVacancySkillsByIds(vid,sid);
            if (vskillModelFromRepo?.First() == null)
                return NotFound();
            _mapper.Map(vskillUpdateDto, vskillModelFromRepo);

            _repo.UpdateVacancySkill(vskillModelFromRepo.First());
            _repo.SaveChanges();
            return NoContent();
        }

        [HttpPatch("{vid}/{sid}")]
        public  ActionResult PartialVacancySkillUpdate(string vid,string sid, JsonPatchDocument<VacancySkillUpdateDto> patchDocument)
        {
            var vskillModelFromRepo = _repo.GetVacancySkillsByIds(vid,sid);
            if (vskillModelFromRepo?.First() == null)
                return NotFound();
            var vskillToPatch = _mapper.Map<VacancySkillUpdateDto>(vskillModelFromRepo);
            patchDocument.ApplyTo(vskillToPatch, ModelState);
            if (!TryValidateModel(vskillToPatch))
            {
                return ValidationProblem(ModelState);
            }
            _mapper.Map(vskillToPatch, vskillModelFromRepo);
            _repo.UpdateVacancySkill(vskillModelFromRepo.First());
            _repo.SaveChanges();
            return NoContent();
        }

        /// <summary>
        /// DELETE api/vskills/{id}
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{vid}/{sid}")]
        public ActionResult DeleteVacancySkill(string vid, string sid)
        {
            var vskillModelFromRepo = _repo.GetVacancySkillsByIds(vid,sid);
            if (vskillModelFromRepo?.First() == null)
                return NotFound();
            _repo.DeleteVacancySkill(vskillModelFromRepo.First());
            _repo.SaveChanges();
            return NoContent();
        }
    }
}
