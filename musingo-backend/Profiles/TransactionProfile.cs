using AutoMapper;
using musingo_backend.Dtos;
using musingo_backend.Models;

namespace musingo_backend.Profiles;

public class TransactionProfile : Profile
{
    public TransactionProfile()
    {
        CreateMap<Transaction, TransactionDetailsDto>();
        CreateMap<Transaction, TransactionDto>().ReverseMap();
    }
}