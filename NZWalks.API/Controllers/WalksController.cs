using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTO;
using NZWalks.API.Repositories;

namespace NZWalks.API.Controllers
{
    //api/walks
    [Route("api/[controller]")]
    [ApiController]
    public class WalksController : ControllerBase
    {
        private readonly IMapper mapper;
        private IWalkRepository walkRepository;

        public WalksController(IMapper mapper, IWalkRepository walkRepository)
        {
            this.mapper = mapper;
            this.walkRepository = walkRepository;

        }
        //Create Walk
        //POST
        [HttpPost]
        [ValidateModel]
        public async Task<IActionResult> Create([FromBody] AddWalksRequest addWalksRequest)
        {
            
         
                //map Dto to Domain Model
                var walksDomainModel = mapper.Map<Walk>(addWalksRequest);

                await walkRepository.CreateAsync(walksDomainModel);

                //Map Domain model to Dto
                return Ok(mapper.Map<WalkDto>(walksDomainModel));

        

        }
        //api/walks?filterOn=name?filterquery=Track

        [HttpGet]
        public async Task<IActionResult> GetAll()
                  //  public async Task<IActionResult> GetAll([FromQuery] string? filterOn, [FromQuery] string filterQuery)
        {
            var walksDomain = await walkRepository.GetAllWalkAsync();
            var regionsDto = mapper.Map<List<WalkDto>>(walksDomain);
            return Ok(regionsDto);
        }

        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetWalkById([FromRoute] Guid id)
        {
            var WalksDomin = await walkRepository.GetWalkIdAsync(id);

            if (WalksDomin == null)
            {
                return NotFound();
            }

            return Ok(mapper.Map<WalkDto>(WalksDomin));

        }

        [HttpPut]
        [Route("{id:Guid}")]
        [ValidateModel]
        public async Task<IActionResult> Update([FromRoute] Guid id, UpdateWalksRequestDto updateWalksRequestDto)
        {

            //map Dto to Domain Model

            var walkDomailModel = mapper.Map<Walk>(updateWalksRequestDto);

            walkDomailModel = await walkRepository.UpdateAsync(id, walkDomailModel);

            if (walkDomailModel == null)
            {
                return NotFound();

            }

            return Ok(mapper.Map<WalkDto>(walkDomailModel));

        }


        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            var regionWalkModel = await walkRepository.DeleteWalkById(id);

            if (regionWalkModel == null)
            {
                return NotFound();

            }
            return Ok(mapper.Map<WalkDto>(regionWalkModel));

        }
    }
}
