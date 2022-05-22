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
    [Route("api/areas")]
    [ApiController]
    public class AreasController : ControllerBase
    {
        private readonly IAreaerRepo _repo;
        private readonly IMapper _mapper;

        public AreasController(IAreaerRepo repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }


        /// <summary>
        /// GET api/areas
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [HttpGet("sort")]
        [HttpGet("sort/{type}")]
        public ActionResult<IEnumerable<AreaReadDto>> GetAllAreas([FromQuery] string fields = "", string type = "")
        {
            var areaItems = _repo.GetAllAreas();
            if (fields != "") areaItems = Sort(areaItems, fields, type);
            return Ok(_mapper.Map<IEnumerable<AreaReadDto>>(areaItems));
        }

        /// <summary>
        /// GET ../sort
        /// </summary>
        /// <returns></returns>
        private IEnumerable<Area> Sort(IEnumerable<Area> areas,string fields, string type = "")
        {
            List<Area> areaItems = (List<Area>)areas;
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
                        areaItems = areaItems.OrderByDescending(x => x.GetType().GetProperty(f[i]).GetValue(x)).ToList();
                    if (type == "")
                        areaItems = areaItems.OrderBy(x => x.GetType().GetProperty(f[i]).GetValue(x)).ToList();
                }

            }
            return areaItems;
        }
        /// <summary>
        /// GET api/areas
        /// </summary>
        /// <returns></returns>
        [HttpGet("{parametr}/{value}")]
        [HttpGet("{parametr}/{value}/sort/{type}")]
        [HttpGet("{parametr}/{value}/sort")]
        public ActionResult<IEnumerable<AreaReadDto>> GetSelectedAreas(string parametr, string value="", [FromQuery]string fields="", string type="")
        {
            var areaItems = _repo.GetAllAreas();
            if (value != "")
            {
                parametr = parametr.Insert(0, Char.ToUpper(parametr[0]).ToString());
                parametr = parametr.Remove(1, 1);
                areaItems = areaItems.Where(x => x.GetType().GetProperty(parametr).GetValue(x).ToString() == value).ToList();
            }
            if (areaItems != null)
            {
                if (fields == "") fields = "id";
                areaItems = Sort(areaItems, fields, type);
            }
            else return NotFound();
            return Ok(_mapper.Map<IEnumerable<AreaReadDto>>(areaItems));
        }
        /// <summary>
        /// GET api/areas/{id}
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public ActionResult<Area> GetAreaById(int id)
        {
            var areaItem = _repo.GetAreaById(id);
            if (areaItem != null)
            {
                return Ok(_mapper.Map<AreaReadDto>(areaItem));
            }
            return NotFound();
        }

        [HttpPost]
        public ActionResult<AreaCreateDto> CreateArea(AreaCreateDto areaCreateDto)
        {
            var areaModel = _mapper.Map<Area>(areaCreateDto);
            _repo.CreateArea(areaModel);
            _repo.SaveChanges();

            var areaReadDto = _mapper.Map<AreaReadDto>(areaModel);

            return CreatedAtRoute(new { Id = areaReadDto.Id }, areaReadDto);
            //return Ok(areaReadDto);
        }

        /// <summary>
        /// POST api/areas/{id}
        /// </summary>
        /// <param name="id"></param>
        /// <param name="areaUpdateDto"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public ActionResult UpdateArea(int id, AreaUpdateDto areaUpdateDto)
        {
            var areaModelFromRepo = _repo.GetAreaById(id);
            if (areaModelFromRepo == null)
                return NotFound();
            _mapper.Map(areaUpdateDto, areaModelFromRepo);

            _repo.UpdateArea(areaModelFromRepo);
            _repo.SaveChanges();
            return NoContent();
        }

        [HttpPatch("{id}")]
        public  ActionResult PartialAreaUpdate(int id, JsonPatchDocument<AreaUpdateDto> patchDocument)
        {
            var areaModelFromRepo = _repo.GetAreaById(id);
            if (areaModelFromRepo == null)
                return NotFound();
            var areaToPatch = _mapper.Map<AreaUpdateDto>(areaModelFromRepo);
            patchDocument.ApplyTo(areaToPatch, ModelState);
            if (!TryValidateModel(areaToPatch))
            {
                return ValidationProblem(ModelState);
            }
            _mapper.Map(areaToPatch, areaModelFromRepo);
            _repo.UpdateArea(areaModelFromRepo);
            _repo.SaveChanges();
            return NoContent();
        }

        /// <summary>
        /// DELETE api/areas/{id}
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public ActionResult DeleteArea(int id)
        {
            var areaModelFromRepo = _repo.GetAreaById(id);
            if (areaModelFromRepo == null)
                return NotFound();
            _repo.DeleteArea(areaModelFromRepo);
            _repo.SaveChanges();
            return NoContent();
        }
    }
}
