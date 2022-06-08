using AutoMapper;
using PruebaBackAPI.Dtos;
using PruebaBackAPI.Models;

namespace PruebaBackAPI.Profiles;

public class UsersProfile : Profile
{
    public UsersProfile()
    {
        CreateMap<User, UserDto>();
        CreateMap<PaginationResult<User>, PaginationDto<UserDto>>();
        CreateMap<UserCreateDto, User>();
        CreateMap<UserUpdateDto, User>();
    }
}