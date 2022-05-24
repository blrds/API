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
    [Route("api/vspecs")]
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
        /// GET api/vspecs
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [HttpGet("sort")]
        [HttpGet("sort/{type}")]
        public ActionResult<IEnumerable<VacancySpecializationReadDto>> GetAllVacancySpecializations([FromQuery] string fields = "", string type = "")
        {
            var vspecItems = _repo.GetAllVacancySpecializations();
            if (fields != "") vspecItems = Sort(vspecItems, fields, type);
            return Ok(_mapper.Map<IEnumerable<VacancySpecializationReadDto>>(vspecItems));
        }

        /// <summary>
        /// GET ../sort
        /// </summary>
        /// <returns></returns>
        private IEnumerable<VacancySpecialization> Sort(IEnumerable<VacancySpecialization> vspecs,string fields, string type = "")
        {
            List<VacancySpecialization> vspecItems = (List<VacancySpecialization>)vspecs;
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
                        vspecItems = vspecItems.OrderByDescending(x => x.GetType().GetProperty(f[i]).GetValue(x)).ToList();
                    if (type == "")
                        vspecItems = vspecItems.OrderBy(x => x.GetType().GetProperty(f[i]).GetValue(x)).ToList();
                }

            }
            return vspecItems;
        }
        /// <summary>
        /// GET api/vspecs
        /// </summary>
        /// <returns></returns>
        [HttpGet("{parametr}/{value}")]
        [HttpGet("{parametr}/{value}/sort/{type}")]
        [HttpGet("{parametr}/{value}/sort")]
        public ActionResult<IEnumerable<VacancySpecializationReadDto>> GetSelectedVacancySpecializations(string parametr, string value="", [FromQuery]string fields="", string type="")
        {
            IEnumerable<VacancySpecialization> vspecItems = null;
            if (value != "")
            {
                parametr = parametr.Insert(0, Char.ToUpper(parametr[0]).ToString());
                parametr = parametr.Remove(1, 1);
                vspecItems = (IEnumerable<VacancySpecialization>)_repo.GetType().GetMethod("GetVacancySpecializationsById" + parametr).Invoke(_repo, new object[] { value });
            }
            if (vspecItems != null && vspecItems.Count() != 0)
            {
                if (fields == "") fields = "id";
                vspecItems = Sort(vspecItems, fields, type);
            }
            else return NotFound();
            return Ok(_mapper.Map<IEnumerable<VacancySpecializationReadDto>>(vspecItems));
        }
        

        [HttpPost]
        public ActionResult<VacancySpecializationCreateDto> CreateVacancySpecialization(VacancySpecializationCreateDto vspecCreateDto)
        {
            var vspecModel = _mapper.Map<VacancySpecialization>(vspecCreateDto);
            _repo.CreateVacancySpecialization(vspecModel);
            _repo.SaveChanges();

            var vspecReadDto = _mapper.Map<VacancySpecializationReadDto>(vspecModel);

            return CreatedAtRoute(new { VId = vspecReadDto.IdVacancy, SId=vspecCreateDto.IdSpecialization }, vspecReadDto);
            //return Ok(vspecReadDto);
        }

        /// <summary>
        /// POST api/vspecs/{id}
        /// </summary>
        /// <param name="id"></param>
        /// <param name="vspecUpdateDto"></param>
        /// <returns></returns>
        [HttpPut("{vid}/{sid}")]
        public ActionResult UpdateVacancySpecialization(string vid, string sid, VacancySpecializationUpdateDto vspecUpdateDto)
        {
            var vspecModelFromRepo = _repo.GetVacancySpecializationsByIds(vid,sid);
            if (vspecModelFromRepo?.First() == null)
                return NotFound();
            _mapper.Map(vspecUpdateDto, vspecModelFromRepo);

            _repo.UpdateVacancySpecialization(vspecModelFromRepo.First());
            _repo.SaveChanges();
            return NoContent();
        }

        [HttpPatch("{vid}/{sid}")]
        public  ActionResult PartialVacancySpecializationUpdate(string vid,string sid, JsonPatchDocument<VacancySpecializationUpdateDto> patchDocument)
        {
            var vspecModelFromRepo = _repo.GetVacancySpecializationsByIds(vid,sid);
            if (vspecModelFromRepo?.First() == null)
                return NotFound();
            var vspecToPatch = _mapper.Map<VacancySpecializationUpdateDto>(vspecModelFromRepo);
            patchDocument.ApplyTo(vspecToPatch, ModelState);
            if (!TryValidateModel(vspecToPatch))
            {
                return ValidationProblem(ModelState);
            }
            _mapper.Map(vspecToPatch, vspecModelFromRepo);
            _repo.UpdateVacancySpecialization(vspecModelFromRepo.First());
            _repo.SaveChanges();
            return NoContent();
        }

        /// <summary>
        /// DELETE api/vspecs/{id}
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{vid}/{sid}")]
        public ActionResult DeleteVacancySpecialization(string vid, string sid)
        {
            var vspecModelFromRepo = _repo.GetVacancySpecializationsByIds(vid,sid);
            if (vspecModelFromRepo?.First() == null)
                return NotFound();
            _repo.DeleteVacancySpecialization(vspecModelFromRepo.First());
            _repo.SaveChanges();
            return NoContent();
        }
    }
}
