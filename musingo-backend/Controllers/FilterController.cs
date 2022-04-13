using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using musingo_backend.Authentication;
using musingo_backend.Dtos;
using musingo_backend.Models;
using musingo_backend.Repositories;

namespace musingo_backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FilterController : ControllerBase
    {
        private IMapper _mapper;
        private IOfferRepository _offerRepository;
        private IUserRepository _userRepository;

        public FilterController(IMapper mapper, IOfferRepository offerRepository, IUserRepository userRepository)
        {
            _mapper = mapper;
            _offerRepository = offerRepository;
            _userRepository = userRepository;
        }

        [HttpGet]//string? search,string? category,double? priceFrom,double? priceTo,string? sorting
        public async Task<ActionResult<ICollection<OfferDetailsDto>>> GetOfferByFilter([FromQuery]FilterOfferDto filterDto)
        {
            var offers = await _offerRepository.GetOfferByFilter(filterDto);
            return Ok(_mapper.Map<ICollection<OfferDto>>(offers));
        }
    }
}
