using AutoMapper;
using Microsoft.OpenApi.Extensions;
using MontyTask.Data.DTOs;
using MontyTask.Data.Models;
using MontyTask.Data.Resources;
using MontyTask.Helpers;

namespace MontyTask.Mapping;
public class ModelToResourceProfile : Profile
{
    public ModelToResourceProfile()
    {
        CreateMap<User, UserResource>();

        CreateMap<AccessToken, AccessTokenResource>()
            .ForMember(a => a.AccessToken, opt => opt.MapFrom(a => a.Token))
            .ForMember(a => a.Expiration, opt => opt.MapFrom(a => a.Expiration));

        CreateMap<Subscription, SubscriptionResource>()
            .ForMember(a => a.SubscriptionType, opt => opt.MapFrom(x => Enum.GetName(x.SubscriptionType)));
    }
}