using System;
using AutoMapper;

namespace FidelizeDotz.Services.Api.CrossCutting.Bases
{
    public abstract class BaseAdapter : IAdapter, IDisposable
    {
        private readonly IMapper _mapper;

        protected BaseAdapter(IMapper mapper)
        {
            _mapper = mapper;
        }

        public virtual TDestination ConvertTo<TSource, TDestination>(TSource source)
            where TSource : class
            where TDestination : class
        {
            var destination = default(TDestination);
            if (source != null)
                destination = _mapper.Map<TSource, TDestination>(source);
            return destination;
        }

        public virtual TDestination ConvertTo<TSource, TDestination>(
            TSource source,
            TDestination destination)
            where TSource : class
            where TDestination : class
        {
            if (source != null)
                _mapper.Map(source, destination);
            return destination;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
        }

        ~BaseAdapter()
        {
            Dispose(false);
        }
    }
}