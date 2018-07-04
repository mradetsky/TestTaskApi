using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using TestTaskApi.Dto;
using TestTaskApi.EF;
using TestTaskApi.EF.Entities;
using TestTaskApi.Filters;

namespace TestTaskApi.Controllers
{
    [Produces("application/json")]
    [Route("resources")]
    public class ResourcesController : Controller
    {
        private MapperConfiguration _mapperConfiguration;
        private IMapper _mapper;
        public ResourcesController(MapperConfiguration mapperConfiguration, IMapper mapper)
        {
            _mapperConfiguration = mapperConfiguration;
            _mapper = mapper;
        }
        
        [HttpGet]
        [ETag]
        public async Task<List<ResourceListDto>> Get()
        {
            using (var db = new TestDbContext())
            {

                return await db.Resources.AsQueryable().ProjectToListAsync<ResourceListDto>(_mapperConfiguration);
            }
        }

        [HttpGet("{id}")]
        [ETag]
        public async Task<ResourceDto> Get(int id)
        {
            using (var db = new TestDbContext())
            {
                return await db.Resources.AsQueryable().Where(x => x.Id == id).ProjectToSingleOrDefaultAsync<ResourceDto>(_mapperConfiguration);
            }
        }



        [HttpPost]
        public async Task Post(ResourceEditDto dto)
        {

            if (TryValidateModel(dto))
            {
                using (var db = new TestDbContext())
                {
                    var model = await db.Resources.FirstOrDefaultAsync(x => x.Id == dto.Id);
                    if (model != null)
                    {
                        _mapper.Map(dto, model);
                        model.UpdatedOnUtc = DateTime.Now;
                        db.Entry(model).State = EntityState.Modified;
                        await db.SaveChangesAsync();
                    }
                    else
                    {
                        model = _mapper.Map<Resource>(dto);
                        model.CreatedOnUtc = DateTime.Now;
                        model.UpdatedOnUtc = DateTime.Now;
                        db.Resources.Add(model);
                        await db.SaveChangesAsync();
                    }
                }
            }
            else
            {
                throw new Exception(String.Join(";", ModelState.Values.SelectMany(v => v.Errors).Select(x => x.ErrorMessage)));
            }
        }


        [HttpDelete("{id}")]
        public async Task Delete(int id)
        {
            using (var db = new TestDbContext())
            {
                var model = await db.Resources.FirstOrDefaultAsync(x => x.Id == id);
                if (model != null)
                {
                    db.Resources.Remove(model);
                }
                await db.SaveChangesAsync();

            }
        }


        [HttpPatch("{id}")]
        public async Task Patch(int id, [FromForm]JsonPatchDocument<ResourceEditDto> patch)
        {
            using (var db = new TestDbContext())
            {
                var resource = await db.Resources.FirstOrDefaultAsync(a => a.Id == id);
                var dto = _mapper.Map<ResourceEditDto>(resource);
                patch.ApplyTo(dto);
                if (TryValidateModel(dto))
                {
                    _mapper.Map(dto, resource);
                    resource.UpdatedOnUtc = DateTime.Now;
                    await db.SaveChangesAsync();
                }
                else
                {
                    throw new Exception(String.Join(";", ModelState.Values.SelectMany(v => v.Errors).Select(x => x.ErrorMessage)));
                }
            }
        }
    }
}
