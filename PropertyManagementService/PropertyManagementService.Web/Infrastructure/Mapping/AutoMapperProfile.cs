﻿namespace PropertyManagementService.Web.Infrastructure.Mapping
{
    using AutoMapper;
    using PropertyManagementService.Common;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;

    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            IEnumerable<Assembly> assemblies = AppDomain.CurrentDomain
                .GetAssemblies()
                .Where(x => x.FullName.Contains(Constants.Constants.SolutionName));

            IEnumerable<Type> types = assemblies
                .SelectMany(a => a.GetTypes().Where(t => t.IsClass && !t.IsAbstract))
                .Where(t => t.GetInterfaces()
                .Any(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IMapFrom<>)));

            types
                .Select(t => new
                {
                    Destination = t,
                    Source = t.GetInterfaces()
                    .Where(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IMapFrom<>))
                    .FirstOrDefault()
                    .GetGenericArguments()
                    .FirstOrDefault()
                })
                .ToList()
                .ForEach(m => this.CreateMap(m.Source, m.Destination));

            IEnumerable<Type> configurationTypes = assemblies
                .SelectMany(a => a.GetTypes()
                .Where(t => typeof(IHaveCustomMapping)
                .IsAssignableFrom(t) && t.IsClass && !t.IsAbstract));

            configurationTypes
                .Select(t => Activator.CreateInstance(t))
                .Cast<IHaveCustomMapping>()
                .ToList()
                .ForEach(t => t.Configure(this));
        }
    }
}
