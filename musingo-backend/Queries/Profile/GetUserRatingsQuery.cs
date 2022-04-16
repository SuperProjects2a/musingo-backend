﻿using MediatR;
using musingo_backend.Models;
using musingo_backend.Repositories;

namespace musingo_backend.Queries;

public class GetUserRatingsQuery: IRequest<HandlerResultCollection<UserComment>>
{
    public int UserId { get; set; }
}