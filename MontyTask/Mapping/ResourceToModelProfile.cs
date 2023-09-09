using AutoMapper;
using MontyTask.Data.Models;
using MontyTask.Data.Resources;

namespace MontyTask.Mapping;
public class ResourceToModelProfile : Profile
{
    public ResourceToModelProfile()
    {
        CreateMap<UserCredentialsResource, User>();
    }
}