using System;
using FidelizeDotz.Services.Api.CrossCutting.Bases;
using FidelizeDotz.Services.Api.Domain.Application.Dtos;
using FidelizeDotz.Services.Api.Domain.Infra.Interfaces;
using Microsoft.Extensions.Logging;

namespace FidelizeDotz.Services.Api.Domain.Application.Services
{
    public abstract class ServiceBase : IDisposable
    {
        public ServiceBase(
            IUnitOfWork unitOfWork = null,
            IAdapter adapter = null,
            ILogger logger = null,
            UserLogged userLogged = null)
        {
            UnitOfWork = unitOfWork;
            Adapter = adapter;
            Logger = logger;
            UserLogged = userLogged;
        }

        protected IUnitOfWork UnitOfWork { get; }

        protected IAdapter Adapter { get; }

        protected ILogger Logger { get; }

        protected UserLogged UserLogged { get; }

        public async void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public virtual void Dispose(bool a = false)
        {
        }

        ~ServiceBase()
        {
            Dispose(false);
        }
    }
}