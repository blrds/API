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
    [Route("api/experiences")]
    [ApiController]
    public class ExperiencesController : ControllerBase
    {
        private readonly IExperienceerRepo _repo;
        private readonly IMapper _mapper;

        public ExperiencesController(IExperienceerRepo repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }


        /// <summary>
        /// GET api/experiences
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [HttpGet("sort")]
        [HttpGet("sort/{type}")]
        public ActionResult<IEnumerable<ExperienceReadDto>> GetAllExperiences([FromQuery] string fields = "", string type = "")
        {
            var experienceItems = _repo.GetAllExperiences();
            if (fields != "") experienceItems = Sort(experienceItems, fields, type);
            return Ok(_mapper.Map<IEnumerable<ExperienceReadDto>>(experienceItems));
        }

        /// <summary>
        /// GET ../sort
        /// </summary>
        /// <returns></returns>
        private IEnumerable<Experience> Sort(IEnumerable<Experience> experiences,string fields, string type = "")
        {
            List<Experience> experienceItems = (List<Experience>)experiences;
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
                        experienceItems = experienceItems.OrderByDescending(x => x.GetType().GetProperty(f[i]).GetValue(x)).ToList();
                    if (type == "")
                        experienceItems = experienceItems.OrderBy(x => x.GetType().GetProperty(f[i]).GetValue(x)).ToList();
                }

            }
            return experienceItems;
        }
        /// <summary>
        /// GET api/experiences
        /// </summary>
        /// <returns></returns>
        [HttpGet("{parametr}/{value}")]
        [HttpGet("{parametr}/{value}/sort/{type}")]
        [HttpGet("{parametr}/{value}/sort")]
        public ActionResult<IEnumerable<ExperienceReadDto>> GetSelectedExperiences(string parametr, string value="", [FromQuery]string fields="", string type="")
        {
            IEnumerable<Experience> experienceItems = null;
            if (value != "")
            {
                parametr = parametr.Insert(0, Char.ToUpper(parametr[0]).ToString());
                parametr = parametr.Remove(1, 1);
                experienceItems = (IEnumerable<Experience>)_repo.GetType().GetMethod("GetExperiencesBy" + parametr).Invoke(_repo, new object[] { value });
            }
            if (experienceItems != null)
            {
                if (fields == "") fields = "id";
                experienceItems = Sort(experienceItems, fields, type);
            }
            else return NotFound();
            return Ok(_mapper.Map<IEnumerable<ExperienceReadDto>>(experienceItems));
        }
        /// <summary>
        /// GET api/experiences/{id}
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public ActionResult<Experience> GetExperienceById(string id)
        {
            var experienceItem = _repo.GetExperienceById(id);
            if (experienceItem != null)
            {
                return Ok(_mapper.Map<ExperienceReadDto>(experienceItem));
            }
            return NotFound();
        }

        [HttpPost]
        public ActionResult<ExperienceCreateDto> CreateExperience(ExperienceCreateDto experienceCreateDto)
        {
            var experienceModel = _mapper.Map<Experience>(experienceCreateDto);
            _repo.CreateExperience(experienceModel);
            _repo.SaveChanges();

            var experienceReadDto = _mapper.Map<ExperienceReadDto>(experienceModel);

            return CreatedAtRoute(new { Id = experienceReadDto.Id }, experienceReadDto);
            //return Ok(experienceReadDto);
        }

        /// <summary>
        /// POST api/experiences/{id}
        /// </summary>
        /// <param name="id"></param>
        /// <param name="experienceUpdateDto"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public ActionResult UpdateExperience(string id, ExperienceUpdateDto experienceUpdateDto)
        {
            var experienceModelFromRepo = _repo.GetExperienceById(id);
            if (experienceModelFromRepo == null)
                return NotFound();
            _mapper.Map(experienceUpdateDto, experienceModelFromRepo);

            _repo.UpdateExperience(experienceModelFromRepo);
            _repo.SaveChanges();
            return NoContent();
        }

        [HttpPatch("{id}")]
        public  ActionResult PartialExperienceUpdate(string id, JsonPatchDocument<ExperienceUpdateDto> patchDocument)
        {
            var experienceModelFromRepo = _repo.GetExperienceById(id);
            if (experienceModelFromRepo == null)
                return NotFound();
            var experienceToPatch = _mapper.Map<ExperienceUpdateDto>(experienceModelFromRepo);
            patchDocument.ApplyTo(experienceToPatch, ModelState);
            if (!TryValidateModel(experienceToPatch))
            {
                return ValidationProblem(ModelState);
            }
            _mapper.Map(experienceToPatch, experienceModelFromRepo);
            _repo.UpdateExperience(experienceModelFromRepo);
            _repo.SaveChanges();
            return NoContent();
        }

        /// <summary>
        /// DELETE api/experiences/{id}
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public ActionResult DeleteExperience(string id)
        {
            var experienceModelFromRepo = _repo.GetExperienceById(id);
            if (experienceModelFromRepo == null)
                return NotFound();
            _repo.DeleteExperience(experienceModelFromRepo);
            _repo.SaveChanges();
            return NoContent();
        }
    }
}
