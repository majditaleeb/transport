using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Transport.Models;

namespace Transport.App_Start
{
    public class AutoMapperConfig:Profile
    {
        //Mapper.CreateMap<Directorate, DirectorateDTO>().Re4verseMap();
        public override string ProfileName => "TestProfile";
        public static void ConfigureMappings()
        {
#pragma warning disable CS0618 // Type or member is obsolete
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<Informaion,HomeIndexMV>().ReverseMap();
                //cfg.CreateMap<Department, DepartmentDTO>().ReverseMap();
                //cfg.CreateMap<Division, DivisionDTO>().ReverseMap();
                //cfg.CreateMap<Employee, EmployeeDTO>().ReverseMap();
                //cfg.CreateMap<LoginInfo, LoginInfoDTO>().ReverseMap();
                //cfg.CreateMap<Rank, RankDTO>().ReverseMap();
                //cfg.CreateMap<Role, RoleDTO>().ReverseMap();
                //cfg.CreateMap<Vacation, VacationDTO>().ReverseMap();
                //cfg.CreateMap<TypeBranch, TypeBranchDTO>().ReverseMap();
                //cfg.CreateMap<VacationType, VacationTypeDTO>().ReverseMap();
                //cfg.CreateMap<Status, StatusDTO>().ReverseMap();
                //cfg.CreateMap<Attachment, AttachmentDTO>().ReverseMap();
            });
#pragma warning restore CS0618 // Type or member is obsolete
        }
    }
}