﻿namespace PropertyManagementService.Common
{
    using AutoMapper;

    public interface IHaveCustomMapping
    {
        void Configure(Profile profile);
    }
}
