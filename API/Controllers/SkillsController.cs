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
    [Route("api/skills")]
    [ApiController]
    public class SkillsController : ControllerBase
    {
        private readonly ISkillerRepo _repo;
        private readonly IMapper _mapper;

        public SkillsController(ISkillerRepo repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }


        /// <summary>
        /// GET api/skills
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [HttpGet("sort")]
        [HttpGet("sort/{type}")]
        public ActionResult<IEnumerable<SkillReadDto>> GetAllSkills([FromQuery] string fields = "", string type = "")
        {
            var skillItems = _repo.GetAllSkills();
            if (fields != "") skillItems = Sort(skillItems, fields, type);
            return Ok(_mapper.Map<IEnumerable<SkillReadDto>>(skillItems));
        }

        /// <summary>
        /// GET ../sort
        /// </summary>
        /// <returns></returns>
        private IEnumerable<Skill> Sort(IEnumerable<Skill> skills,string fields, string type = "")
        {
            List<Skill> skillItems = (List<Skill>)skills;
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
                        skillItems = skillItems.OrderByDescending(x => x.GetType().GetProperty(f[i]).GetValue(x)).ToList();
                    if (type == "")
                        skillItems = skillItems.OrderBy(x => x.GetType().GetProperty(f[i]).GetValue(x)).ToList();
                }

            }
            return skillItems;
        }
        /// <summary>
        /// GET api/skills
        /// </summary>
        /// <returns></returns>
        [HttpGet("{parametr}/{value}")]
        [HttpGet("{parametr}/{value}/sort/{type}")]
        [HttpGet("{parametr}/{value}/sort")]
        public ActionResult<IEnumerable<SkillReadDto>> GetSelectedSkills(string parametr, string value="", [FromQuery]string fields="", string type="")
        {
            IEnumerable<Skill> skillItems = null;
            if (value != "")
            {
                parametr = parametr.Insert(0, Char.ToUpper(parametr[0]).ToString());
                parametr = parametr.Remove(1, 1);
                skillItems = (IEnumerable<Skill>)_repo.GetType().GetMethod("GetSkillsBy" + parametr).Invoke(_repo, new object[] { value });
            }
            if (skillItems != null)
            {
                if (fields == "") fields = "id";
                skillItems = Sort(skillItems, fields, type);
            }
            else return NotFound();
            return Ok(_mapper.Map<IEnumerable<SkillReadDto>>(skillItems));
        }
        /// <summary>
        /// GET api/skills/{id}
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public ActionResult<Skill> GetSkillById(int id)
        {
            var skillItem = _repo.GetSkillById(id);
            if (skillItem != null)
            {
                return Ok(_mapper.Map<SkillReadDto>(skillItem));
            }
            return NotFound();
        }

        [HttpPost]
        public ActionResult<SkillCreateDto> CreateSkill(SkillCreateDto skillCreateDto)
        {
            var skillModel = _mapper.Map<Skill>(skillCreateDto);
            _repo.CreateSkill(skillModel);
            _repo.SaveChanges();

            var skillReadDto = _mapper.Map<SkillReadDto>(skillModel);

            return CreatedAtRoute(new { Id = skillReadDto.Id }, skillReadDto);
            //return Ok(skillReadDto);
        }

        /// <summary>
        /// POST api/skills/{id}
        /// </summary>
        /// <param name="id"></param>
        /// <param name="skillUpdateDto"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public ActionResult UpdateSkill(int id, SkillUpdateDto skillUpdateDto)
        {
            var skillModelFromRepo = _repo.GetSkillById(id);
            if (skillModelFromRepo == null)
                return NotFound();
            _mapper.Map(skillUpdateDto, skillModelFromRepo);

            _repo.UpdateSkill(skillModelFromRepo);
            _repo.SaveChanges();
            return NoContent();
        }

        [HttpPatch("{id}")]
        public  ActionResult PartialSkillUpdate(int id, JsonPatchDocument<SkillUpdateDto> patchDocument)
        {
            var skillModelFromRepo = _repo.GetSkillById(id);
            if (skillModelFromRepo == null)
                return NotFound();
            var skillToPatch = _mapper.Map<SkillUpdateDto>(skillModelFromRepo);
            patchDocument.ApplyTo(skillToPatch, ModelState);
            if (!TryValidateModel(skillToPatch))
            {
                return ValidationProblem(ModelState);
            }
            _mapper.Map(skillToPatch, skillModelFromRepo);
            _repo.UpdateSkill(skillModelFromRepo);
            _repo.SaveChanges();
            return NoContent();
        }

        /// <summary>
        /// DELETE api/skills/{id}
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public ActionResult DeleteSkill(int id)
        {
            var skillModelFromRepo = _repo.GetSkillById(id);
            if (skillModelFromRepo == null)
                return NotFound();
            _repo.DeleteSkill(skillModelFromRepo);
            _repo.SaveChanges();
            return NoContent();
        }
    }
}
