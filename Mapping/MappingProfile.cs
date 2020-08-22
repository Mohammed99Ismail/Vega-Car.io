using System.Linq;
using AutoMapper;
using vega.Models;
using vega.Models.Resources;

namespace vega.Mapping
{
    public class MappingProfile:Profile
    {
        public MappingProfile()
        {
        CreateMap<Photo,PhotoResource>();
        CreateMap<VehicleQueryResource,VehicleQuery>();
        CreateMap<Make,MakeResource>();
        CreateMap<Model,ModelResource>();
        CreateMap<Vehicle,SaveVehicleResource>()
        .ForMember(vr=>vr.Contact,opt=>opt
        .MapFrom(v=>new ContactResources{ Name=v.ContactName,Phone=v.ContactPhone,Email=v.ContactEmail }))
        .ForMember(vr=>vr.Features,opt=>opt.MapFrom(v=>v.Features.Select(vf=>vf.FeatureId)));

        CreateMap<Vehicle,VehicleResource>()
        .ForMember(vr=>vr.Make,opt=>opt.MapFrom(v=>v.Model.Make))
        .ForMember(vr=>vr.Contact,opt=>opt
        .MapFrom(v=>new ContactResources{ Name=v.ContactName,Phone=v.ContactPhone,Email=v.ContactEmail }))
        .ForMember(vr=>vr.Features,opt=>opt.MapFrom(v=>v.Features.Select(vf=>new FeatureResource{Id=vf.Feature.Id,Name=vf.Feature.Name})));



         CreateMap<SaveVehicleResource,Vehicle>()
        .ForMember(v=>v.Id,opt=>opt.Ignore())
        .ForMember(v=>v.ContactName,opt=>opt.MapFrom(vr=>vr.Contact.Name))
        .ForMember(v=>v.ContactPhone,opt=>opt.MapFrom(vr=>vr.Contact.Phone))
        .ForMember(v=>v.ContactEmail,opt=>opt.MapFrom(vr=>vr.Contact.Email))
        .ForMember(v=>v.Features,opt=>opt.Ignore()).AfterMap((vr,v)=>{

        //remove unselected feature
        var removedFeature=v.Features.Where(f=>!vr.Features.Contains(f.FeatureId)).ToList();
        foreach(var r in removedFeature)
        v.Features.Remove(r);

        //add new feature

        var addedFeatur=vr.Features.Where(id=>!v.Features.Any(v=>v.FeatureId==id));
        foreach(var id in addedFeatur)
        v.Features.Add(new VehicleFeature{FeatureId=id});
        
        }); 
        
    }
    }
}