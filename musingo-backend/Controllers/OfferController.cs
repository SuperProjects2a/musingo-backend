using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using musingo_backend.Authentication;
using musingo_backend.Commands;
using musingo_backend.Dtos;
using musingo_backend.Models;
using musingo_backend.Queries;
using musingo_backend.Repositories;

namespace musingo_backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OfferController : ControllerBase
    {
        private IMapper _mapper;
        private IMediator _mediator;

        public OfferController(IMapper mapper, IMediator mediator)
        {
            _mapper = mapper;
            _mediator = mediator;
        }
        [HttpGet]
        public async Task<ActionResult<ICollection<OfferDetailsDto>>> GetAll([FromQuery] OfferFilterDto filterDto)
        {
            var userIdString = User.Claims.FirstOrDefault(x => x.Type == "id");
            var userId = userIdString is null ? -1 : int.Parse(userIdString.Value);
            var request = _mapper.Map<GetOffersByFilterQuery>(filterDto);
            var watchedRequest = new GetOffersWatchedByUserQuery()
            {
                UserId = userId
            };
            var result = await _mediator.Send(request);
            
            var watchedOffers = await _mediator.Send(watchedRequest);
            if (watchedOffers is not null)
            {
                var arr = result.Body.ToArray();
                for (int i = 0; i < result.Body.Count; i++)
                {
                    arr[i].isWatched = watchedOffers.Body.Any(x => x.Id == arr[i].Id);
                }

                result.Body = arr;
            }
            
            return Ok(result.Body);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<OfferDetailsDto>> GetById(int id)
        {
            var userIdString = User.Claims.FirstOrDefault(x => x.Type == "id");
            var userId = userIdString is null ? -1 : int.Parse(userIdString.Value);
            var request = new GetOfferByIdQuery()
            {
                Id = id
            };
            var watchedRequest = new GetOffersWatchedByUserQuery()
            {
                UserId = userId
            };

            var result = await _mediator.Send(request);
            var watchedOffers = await _mediator.Send(watchedRequest);
            if (watchedOffers is not null && result.Body is not null)
            {
                if (watchedOffers.Body.Any(x => x.Id == result.Body.Id))
                    result.Body.isWatched = true;
            }

            return result.Status switch
            {
                1 => Problem("This offer is banned"),
                200 => Ok(result.Body),
                404 => NotFound(),
                _ => Forbid()
            };
        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResult<OfferDetailsDto>> Add([FromBody] OfferCreateDto offerCreateDto)
        {
            var userId = int.Parse(User.Claims.First(x => x.Type == "id").Value);

            var request = _mapper.Map<AddOfferCommand>(offerCreateDto);
            request.UserId = userId;
            request.ImageUrls = offerCreateDto.ImageUrls;

            var result = await _mediator.Send(request);

            switch (result.Status)
            {
                case 404:
                    return NotFound();
            }

            return Ok(result.Body);
        }

        [Authorize]
        [HttpPut]
        public async Task<ActionResult<OfferDetailsDto>> Update([FromBody] OfferUpdateDto offerUpdateDto)
        {
            var userId = int.Parse(User.Claims.First(x => x.Type == "id").Value);

            var request = _mapper.Map<UpdateOfferCommand>(offerUpdateDto);
            request.UserId = userId;

            var result = await _mediator.Send(request);

            return result.Status switch
            {
                1 => Problem("This offer is banned"),
                200 => Ok(_mapper.Map<OfferDetailsDto>(result.Body)),
                403 => Forbid(),
                404 => NotFound(),
                _ => Forbid()
            };
        }
        [Authorize]
        [HttpPost("Report")]
        public async Task<ActionResult<ReportDto>> ReportOffer(ReportCreateDto report)
        {
            var userId = int.Parse(User.Claims.First(x => x.Type == "id").Value);

            var request = new ReportOfferCommand()
            {
                OfferId = report.OfferId,
                UserId = userId,
                Reason = report.Reason,
                Text = report.Text
            };

            var result = await _mediator.Send(request);

            return result.Status switch
            {
                1 => Problem("You reported this offer"),
                200 => Ok(_mapper.Map<ReportDto>(result.Body)),
                404 => NotFound(),
                _ => Forbid()
            };
        }

        [Authorize(Roles = "Admin,Moderator")]
        [HttpGet("ReportedOffers")]
        public async Task<ActionResult<ICollection<ReportedOffersDto>>> GetReportedOffers()
        {
            var request = new GetReportedOffersQuery();

            var result = await _mediator.Send(request);

            return result.Status switch
            {
                200 => Ok(result.Body),
                _ => Forbid()
            };
        }
        [Authorize]
        [HttpPut("Promote/{offerId}")]
        public async Task<ActionResult<OfferDto>> PromoteOffer(int offerId)
        {
            var request = new PromoteOfferCommand()
            {
                UserId = int.Parse(User.Claims.First(x => x.Type == "id").Value),
                OfferId = offerId
            };

            var result = await _mediator.Send(request);

            return result.Status switch
            {
                1 => Problem("Offer is already promoted"),
                2 => Problem("not enough money to promote offer"),
                3 => Problem("You can only promote active offer"),
                200 => Ok(_mapper.Map<OfferDto>(result.Body)),
                404 => NotFound(),
                _ => Forbid()
            };
        }
        [HttpGet("Promote")]
        public async Task<ActionResult<ICollection<OfferDetailsDto>>> GetPromoted()
        {
            var userIdString = User.Claims.FirstOrDefault(x => x.Type == "id");
            var userId = userIdString is null ? -1 : int.Parse(userIdString.Value);
            
            var request = new GetPromotedOffersQuery();
            var watchedRequest = new GetOffersWatchedByUserQuery()
            {
                UserId = userId
            };

            var result = await _mediator.Send(request);
            
            var watchedOffers = await _mediator.Send(watchedRequest);
            if (watchedOffers is not null)
            {
                var arr = result.Body.ToArray();
                for (int i = 0; i < result.Body.Count; i++)
                {
                    arr[i].isWatched = watchedOffers.Body.Any(x => x.Id == arr[i].Id);
                }

                result.Body = arr;
            }

            return result.Status switch
            {
                200 => Ok(result.Body),
                _ => Forbid()
            };
        }

        [HttpGet("User")]
        public async Task<ActionResult<ICollection<OfferDetailsDto>>> GetOfferByUser([FromQuery] UserOffersDto userOfferDto)
        {
            var userIdString = User.Claims.FirstOrDefault(x => x.Type == "id");
            var userId = userIdString is null ? -1 : int.Parse(userIdString.Value);
            
            var watchedRequest = new GetOffersWatchedByUserQuery()
            {
                UserId = userId
            };
            
            var request = new GetUserOtherOffersQuery()
            {
                Email = userOfferDto.Email,
                OfferId = userOfferDto.OfferId
            };

            var result = await _mediator.Send(request);
            
            var watchedOffers = await _mediator.Send(watchedRequest);
            if (watchedOffers is not null)
            {
                var arr = result.Body.ToArray();
                for (int i = 0; i < result.Body.Count; i++)
                {
                    arr[i].isWatched = watchedOffers.Body.Any(x => x.Id == arr[i].Id);
                }

                result.Body = arr;
            }

            return result.Status switch
            {
                200 => Ok(result.Body),
                404 => NotFound(),
                _ => Forbid()
            };
        }



    }
}
