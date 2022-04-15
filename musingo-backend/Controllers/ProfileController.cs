using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using musingo_backend.Authentication;
using musingo_backend.Dtos;
using musingo_backend.Models;
using musingo_backend.Repositories;

namespace musingo_backend.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ProfileController : ControllerBase
    {
        private IMapper _mapper;
        private IOfferRepository _offerRepository;
        private IUserRepository _userRepository;
        private ICommentRepository _commentRepository;
        private IJwtAuth _jwtAuth;

        public ProfileController(IMapper mapper, IOfferRepository offerRepository, IJwtAuth jwt,
            IUserRepository userRepository,ICommentRepository commentRepository)
        {
            _mapper = mapper;
            _offerRepository = offerRepository;
            _jwtAuth = jwt;
            _userRepository = userRepository;
            _commentRepository = commentRepository;
        }

        [HttpGet("Offers")]
        public async Task<ActionResult<ICollection<OfferDto>>> GetUserOffers()
        {
            var userId = int.Parse(User.Claims.First(x => x.Type == "id").Value);
            var result = await _offerRepository.GetUserOffers(userId);
            if (result is not null)
            {
                return Ok(_mapper.Map<ICollection<OfferDto>>(result));
            }

            return NotFound();
        }
        [HttpGet("Comments")]
        public async Task<ActionResult<ICollection<UserCommentDto>>> GetUserComments()
        {
            var userId = int.Parse(User.Claims.First(x => x.Type == "id").Value);
            var result = await _commentRepository.GetUserComments(userId);
            if (result is not null)
            {
                return Ok(_mapper.Map<ICollection<UserCommentDto>>(result));
            }
            return NotFound();
        }
        [HttpGet("Ratings")]
        public async Task<ActionResult<ICollection<UserCommentDto>>> GetUserRatings()
        {
            var userId = int.Parse(User.Claims.First(x => x.Type == "id").Value);
            var result = await _commentRepository.GetUserRatings(userId);
            if (result is not null)
            {
                return Ok(_mapper.Map<ICollection<UserCommentDto>>(result));
            }
            return NotFound();
        }
        [HttpGet("Profile")]
        public async Task<ActionResult<UserDetailsDto>> GetUserInfo()
        {
            var userId = int.Parse(User.Claims.First(x => x.Type == "id").Value);
            var result = await _userRepository.GetUserById(userId);
            if (result is not null)
            {
                var user = _mapper.Map<UserDetailsDto>(result);
                user.AvgRating =await  _userRepository.GetAvg(userId);
                return Ok(user);
            }
            return NotFound();
        }
        [HttpPut]
        public async Task<ActionResult<UserDetailsDto>> UpdateUser(UserUpdateDto userUpdateDto)
        {
            var userId = int.Parse(User.Claims.First(x => x.Type == "id").Value);
            var result = await _userRepository.GetUserById(userId);
            if (result is null) { return NotFound(); }

            if (!String.IsNullOrEmpty(userUpdateDto.Email))
            {
                result.Email = userUpdateDto.Email;
            }

            if (!(String.IsNullOrEmpty(userUpdateDto.OldPassword)&& String.IsNullOrEmpty(userUpdateDto.NewPassword)))
            {
                if (!BCrypt.Net.BCrypt.Verify(userUpdateDto.OldPassword, result.Password))
                {
                    return Problem("Old password do not match");
                }

                if (BCrypt.Net.BCrypt.Verify(userUpdateDto.NewPassword, result.Password))
                {
                    return Problem("The new password cannot be the same as the old one");
                }
                result.Password = BCrypt.Net.BCrypt.HashPassword(userUpdateDto.NewPassword);
            }
            if (!String.IsNullOrEmpty(userUpdateDto.Name))
            {
                result.Name = userUpdateDto.Name;
            }
            if (!String.IsNullOrEmpty(userUpdateDto.Surname))
            {
                result.Surname = userUpdateDto.Surname;
            }
            if (!String.IsNullOrEmpty(userUpdateDto.ImageUrl))
            {
                result.ImageUrl = userUpdateDto.ImageUrl;
            }
            if (!String.IsNullOrEmpty(userUpdateDto.PhoneNumber))
            {
                result.PhoneNumber = userUpdateDto.PhoneNumber;
            }
            if (!String.IsNullOrEmpty(userUpdateDto.City))
            {
                result.City = userUpdateDto.City;
            }
            if (!String.IsNullOrEmpty(userUpdateDto.Street))
            {
                result.Street = userUpdateDto.Street;
            }
            if (!String.IsNullOrEmpty(userUpdateDto.HouseNumber))
            {
                result.HouseNumber = userUpdateDto.HouseNumber;
            }
            if (!String.IsNullOrEmpty(userUpdateDto.PostCode))
            {
                result.PostCode = userUpdateDto.PostCode;
            }
            if (!String.IsNullOrEmpty(userUpdateDto.Gender))
            {
                result.Gender = Enum.Parse<Gender>(userUpdateDto.Gender);
            }
            if (userUpdateDto.Birth is not null)
            {
                result.Birth = userUpdateDto.Birth;
            }

            var user = await _userRepository.UpdateUser(result);

            return _mapper.Map<UserDetailsDto>(user);

        }

    }
}
