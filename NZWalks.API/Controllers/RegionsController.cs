using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NZWalks.API.Data;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTO;
using NZWalks.API.Repositories;
using System.Collections.Generic;

namespace NZWalks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
   
    public class RegionsController : ControllerBase
    {
        private readonly NZWalksDbContext dbContext;
        private readonly IRegionRepository regionRepository;
        private readonly IMapper mapper;

        public RegionsController(NZWalksDbContext dbContext,IRegionRepository regionRepository, IMapper mapper)
        {
            this.dbContext = dbContext;
            this.regionRepository = regionRepository;
            this.mapper = mapper;
        }
        [HttpGet]
        [Authorize(Roles = "Reader,Writer")]
        public async Task<IActionResult> GetAll()
        {

            //Get data from database - domain models
            //var regionsDomain =  await dbContext.Regions.ToListAsync();

            var regionsDomain = await regionRepository.GetAllAsync();

            //Map Domain Models to DTOS

            var regionsDto =  mapper.Map<List<RegionDto>>(regionsDomain);


            //var regionsDto = new List<RegionDto>();
            //foreach (var region in regionsDomain) {
            //    regionsDto.Add(new RegionDto()
            //    {
            //        Id = region.Id,
            //        Code = region.Code,
            //        Name = region.Name,
            //        RegionImageUrl = region.RegionImageUrl,

            //    });
            //}



            //return DTOS

            return Ok(regionsDto);

        }

        [HttpGet]
        [Route("{id:Guid}")]
        [Authorize(Roles = "Reader,Writer")]
        public async Task<IActionResult> GetById([FromRoute] Guid id) {
            //var region = await dbContext.Regions.FirstOrDefaultAsync(x => x.Id == id);

            //var region = await regionRepository.GetByIdAsync(id);

            var region = await regionRepository.GetByIdAsync(id);
            if (region == null) {
                return NotFound();
            }


            //var regionsDto = new RegionDto
            //{
            //    Id = region.Id,
            //    Code = region.Code,
            //    Name = region.Name,
            //    RegionImageUrl = region.RegionImageUrl,

            //};

            //var regionsDto = mapper.Map<RegionDto> (region);

            //return Ok(regionsDto);

            return Ok(mapper.Map<RegionDto>(region));



        }


        [HttpPost]
        [ValidateModel]
        [Authorize(Roles = "Writer")]
        public async Task<IActionResult> Create([FromBody] AddRegionRequestDto addRegionRequestDto) {


         

                //map or conver Dto to Domain model
                //var regionDomainModel = new Region
                //{
                //    Code = addRegionRequestDto.Code,
                //    Name = addRegionRequestDto.Name,
                //    RegionImageUrl = addRegionRequestDto.RegionImageUrl,
                //};
                var regionDomainModel = mapper.Map<Region>(addRegionRequestDto);

                //use Domain Model to create Region

                //await dbContext.Regions.Add(regionDomainModel);
                //await dbContext.SaveChanges();

                regionDomainModel = await regionRepository.CreateAsync(regionDomainModel);




                //map domain model to Dto
                //var regionDto = new RegionDto
                //{
                //    Id = regionDomainModel.Id,
                //    Code = regionDomainModel.Code,
                //    Name = regionDomainModel.Name,
                //    RegionImageUrl = regionDomainModel.RegionImageUrl,
                //};

                var regionDto = mapper.Map<RegionDto>(regionDomainModel);

                return CreatedAtAction(nameof(GetById), new { id = regionDomainModel.Id }, regionDto);

            
         
 


        }

        //api/region/{id}
        [HttpPut]
        [Route("{id:Guid}")]
        [ValidateModel]
        [Authorize(Roles = "Writer")]
        public async Task <IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateRegionRequestDto updateRegionRequestDto)
        {



                //var regionDomainModel = new Region
                //{
                //    Code = updateRegionRequestDto.Code,
                //    Name = updateRegionRequestDto.Name,
                //    RegionImageUrl= updateRegionRequestDto.RegionImageUrl,
                //};

                var regionDomainModel = mapper.Map<Region>(updateRegionRequestDto);

                //regionDomainModel= await dbContext.Regions.FirstOrDefaultAsync(x => x.Id == id);

                regionDomainModel = await regionRepository.UpdateAsync(id, regionDomainModel);

                if (regionDomainModel == null)
                {
                    return NotFound();
                }

                // regionDomainModel.Code = updateRegionRequestDto.Code;
                // regionDomainModel.Name = updateRegionRequestDto.Name;
                // regionDomainModel.RegionImageUrl = updateRegionRequestDto.RegionImageUrl;

                //await dbContext.SaveChangesAsync();

                //var regionDto = new RegionDto
                //{
                //    Id = regionDomainModel.Id,
                //    Code = regionDomainModel.Code,
                //    Name = regionDomainModel.Name,
                //    RegionImageUrl = regionDomainModel.RegionImageUrl,
                //};

                //return Ok(regionDto);
                return Ok(mapper.Map<RegionDto>(regionDomainModel));
            
      
        }



        [HttpDelete]
        [Route("{id:Guid}")]
        [Authorize(Roles = "Writer")]

        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            //var regionDomainModel = await dbContext.Regions.FirstOrDefaultAsync(x => x.Id == id);
            var regionDomainModel = await regionRepository.DeleteAsync(id);

            if (regionDomainModel == null) {
                return NotFound();

            }

            // dbContext.Regions.Remove(regionDomainModel);
            //await dbContext.SaveChangesAsync();

            //var regionDto = new RegionDto
            //{
            //    Id = regionDomainModel.Id,
            //    Code = regionDomainModel.Code,
            //    Name = regionDomainModel.Name,
            //    RegionImageUrl = regionDomainModel.RegionImageUrl,
            //};

            //return Ok(regionDto);

            return Ok(mapper.Map<RegionDto>(regionDomainModel));
        }
    }
}
