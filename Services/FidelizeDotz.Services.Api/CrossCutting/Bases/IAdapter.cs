using System;

namespace FidelizeDotz.Services.Api.CrossCutting.Bases
{
    public interface IAdapter : IDisposable
    {
        U ConvertTo<T, U>(T convertable)
            where T : class
            where U : class;

        U ConvertTo<T, U>(T fromObject, U toObject)
            where T : class
            where U : class;
    }
}