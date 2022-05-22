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
    [Route("api/specializations")]
    [ApiController]
    public class SpecializationsController : ControllerBase
    {
        private readonly ISpecializationerRepo _repo;
        private readonly IMapper _mapper;

        public SpecializationsController(ISpecializationerRepo repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }


        /// <summary>
        /// GET api/specializations
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [HttpGet("sort")]
        [HttpGet("sort/{type}")]
        public ActionResult<IEnumerable<SpecializationReadDto>> GetAllSpecializations([FromQuery] string fields = "", string type = "")
        {
            var specializationItems = _repo.GetAllSpecializations();
            if (fields != "") specializationItems = Sort(specializationItems, fields, type);
            return Ok(_mapper.Map<IEnumerable<SpecializationReadDto>>(specializationItems));
        }

        /// <summary>
        /// GET ../sort
        /// </summary>
        /// <returns></returns>
        private IEnumerable<Specialization> Sort(IEnumerable<Specialization> specializations,string fields, string type = "")
        {
            List<Specialization> specializationItems = (List<Specialization>)specializations;
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
                        specializationItems = specializationItems.OrderByDescending(x => x.GetType().GetProperty(f[i]).GetValue(x)).ToList();
                    if (type == "")
                        specializationItems = specializationItems.OrderBy(x => x.GetType().GetProperty(f[i]).GetValue(x)).ToList();
                }

            }
            return specializationItems;
        }
        /// <summary>
        /// GET api/specializations
        /// </summary>
        /// <returns></returns>
        [HttpGet("{parametr}/{value}")]
        [HttpGet("{parametr}/{value}/sort/{type}")]
        [HttpGet("{parametr}/{value}/sort")]
        public ActionResult<IEnumerable<SpecializationReadDto>> GetSelectedSpecializations(string parametr, string value="", [FromQuery]string fields="", string type="")
        {
            var specializationItems = _repo.GetAllSpecializations();
            if (value != "")
            {
                parametr = parametr.Insert(0, Char.ToUpper(parametr[0]).ToString());
                parametr = parametr.Remove(1, 1);
                specializationItems = specializationItems.Where(x => x.GetType().GetProperty(parametr).GetValue(x).ToString() == value).ToList();
            }
            if (specializationItems != null)
            {
                if (fields == "") fields = "id";
                specializationItems = Sort(specializationItems, fields, type);
            }
            else return NotFound();
            return Ok(_mapper.Map<IEnumerable<SpecializationReadDto>>(specializationItems));
        }
        /// <summary>
        /// GET api/specializations/{id}
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public ActionResult<Specialization> GetSpecializationById(string id)
        {
            var specializationItem = _repo.GetSpecializationById(id);
            if (specializationItem != null)
            {
                return Ok(_mapper.Map<SpecializationReadDto>(specializationItem));
            }
            return NotFound();
        }

        [HttpPost]
        public ActionResult<SpecializationCreateDto> CreateSpecialization(SpecializationCreateDto specializationCreateDto)
        {
            var specializationModel = _mapper.Map<Specialization>(specializationCreateDto);
            _repo.CreateSpecialization(specializationModel);
            _repo.SaveChanges();

            var specializationReadDto = _mapper.Map<SpecializationReadDto>(specializationModel);

            return CreatedAtRoute(new { Id = specializationReadDto.Id }, specializationReadDto);
            //return Ok(specializationReadDto);
        }

        /// <summary>
        /// POST api/specializations/{id}
        /// </summary>
        /// <param name="id"></param>
        /// <param name="specializationUpdateDto"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public ActionResult UpdateSpecialization(string id, SpecializationUpdateDto specializationUpdateDto)
        {
            var specializationModelFromRepo = _repo.GetSpecializationById(id);
            if (specializationModelFromRepo == null)
                return NotFound();
            _mapper.Map(specializationUpdateDto, specializationModelFromRepo);

            _repo.UpdateSpecialization(specializationModelFromRepo);
            _repo.SaveChanges();
            return NoContent();
        }

        [HttpPatch("{id}")]
        public  ActionResult PartialSpecializationUpdate(string id, JsonPatchDocument<SpecializationUpdateDto> patchDocument)
        {
            var specializationModelFromRepo = _repo.GetSpecializationById(id);
            if (specializationModelFromRepo == null)
                return NotFound();
            var specializationToPatch = _mapper.Map<SpecializationUpdateDto>(specializationModelFromRepo);
            patchDocument.ApplyTo(specializationToPatch, ModelState);
            if (!TryValidateModel(specializationToPatch))
            {
                return ValidationProblem(ModelState);
            }
            _mapper.Map(specializationToPatch, specializationModelFromRepo);
            _repo.UpdateSpecialization(specializationModelFromRepo);
            _repo.SaveChanges();
            return NoContent();
        }

        /// <summary>
        /// DELETE api/specializations/{id}
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public ActionResult DeleteSpecialization(string id)
        {
            var specializationModelFromRepo = _repo.GetSpecializationById(id);
            if (specializationModelFromRepo == null)
                return NotFound();
            _repo.DeleteSpecialization(specializationModelFromRepo);
            _repo.SaveChanges();
            return NoContent();
        }
    }
}
