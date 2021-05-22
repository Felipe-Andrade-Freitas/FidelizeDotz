using System;
using AutoMapper;
using FakeItEasy;
using FidelizeDotz.Services.Api.CrossCutting.Bases;
using FidelizeDotz.Services.Api.Domain.Application.Adapters;
using FidelizeDotz.Services.Api.Domain.Application.Dtos;
using FidelizeDotz.Services.Api.Domain.Infra.Interfaces;

namespace Tests.Application.Services
{
    public class ServiceBaseTest
    {
        protected readonly UserLogged _userLogged;
        protected readonly IUnitOfWork _unitOfWork;
        protected readonly IAdapter _adapter;

        public ServiceBaseTest()
        {
            _unitOfWork = A.Fake<IUnitOfWork>();

            var mapper = new MapperConfiguration(cfg => { cfg.ConfigureAutomapper(); }).CreateMapper();
            _adapter = new AutomapperAdapter(mapper);

            _userLogged = A.Fake<UserLogged>(_ => _.WithArgumentsForConstructor
            (
                new object[]
                {
                    Guid.NewGuid(),
                    "User",
                    "user@email",
                    true,
                    "127.0.0.1",
                    "Username"
                }
            ));
        }
    }
}