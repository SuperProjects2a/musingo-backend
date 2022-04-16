using MediatR;
using Microsoft.AspNetCore.Mvc;
using musingo_backend.Commands;
using musingo_backend.Models;
using musingo_backend.Repositories;

namespace musingo_backend.Handlers;

public class UpdateUserHandler : IRequestHandler<UpdateUserCommand, HandlerResult<User>>
{
    private readonly IUserRepository _userRepository;

    public UpdateUserHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<HandlerResult<User>> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetUserById(request.UserId);
        var result = new HandlerResult<User>();

        if (user is null)
        {
            result.Status = 404;
            return result;
        }

        if (!String.IsNullOrEmpty(request.Email))
        {
            user.Email = request.Email;
        }

        if (!(String.IsNullOrEmpty(request.OldPassword) && String.IsNullOrEmpty(request.NewPassword)))
        {
            if (!BCrypt.Net.BCrypt.Verify(request.OldPassword, user.Password))
                return null;


            if (BCrypt.Net.BCrypt.Verify(request.NewPassword, user.Password))
                return null;

            user.Password = BCrypt.Net.BCrypt.HashPassword(request.NewPassword);
        }
        if (!String.IsNullOrEmpty(request.Name))
        {
            user.Name = request.Name;
        }
        if (!String.IsNullOrEmpty(request.Surname))
        {
            user.Surname = request.Surname;
        }
        if (!String.IsNullOrEmpty(request.ImageUrl))
        {
            user.ImageUrl = request.ImageUrl;
        }
        if (!String.IsNullOrEmpty(request.PhoneNumber))
        {
            user.PhoneNumber = request.PhoneNumber;
        }
        if (!String.IsNullOrEmpty(request.City))
        {
            user.City = request.City;
        }
        if (!String.IsNullOrEmpty(request.Street))
        {
            user.Street = request.Street;
        }
        if (!String.IsNullOrEmpty(request.HouseNumber))
        {
            user.HouseNumber = request.HouseNumber;
        }
        if (!String.IsNullOrEmpty(request.PostCode))
        {
            user.PostCode = request.PostCode;
        }
        if (!String.IsNullOrEmpty(request.Gender))
        {
            user.Gender = Enum.Parse<Gender>(request.Gender);
        }
        if (request.Birth is not null)
        {
            user.Birth = request.Birth;
        }

        await _userRepository.UpdateUser(user);
        result.Body = user;
        return  result;
    }
}