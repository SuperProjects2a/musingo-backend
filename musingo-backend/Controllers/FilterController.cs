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
        private ICommentRepository _commentRepository;
        private IJwtAuth _jwtAuth;

        public FilterController(IMapper mapper, IOfferRepository offerRepository, IUserRepository userRepository, ICommentRepository commentRepository, IJwtAuth jwtAuth)
        {
            _mapper = mapper;
            _offerRepository = offerRepository;
            _userRepository = userRepository;
            _commentRepository = commentRepository;
            _jwtAuth = jwtAuth;
        }

        [HttpGet]
        public async Task<ActionResult<ICollection<OfferDetailsDto>>> GetOfferByFilter(string? search,string? category,double? priceFrom,double? priceTo,string? sorting)
        {
            var offers = await _offerRepository.GetOfferByFilter(search,category,priceFrom,priceTo,sorting);
            return Ok(_mapper.Map<ICollection<OfferDto>>(offers));
        }
    }
}
