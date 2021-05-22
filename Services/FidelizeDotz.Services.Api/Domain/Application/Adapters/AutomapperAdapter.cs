using AutoMapper;
using FidelizeDotz.Services.Api.CrossCutting.Bases;

namespace FidelizeDotz.Services.Api.Domain.Application.Adapters
{
    public class AutomapperAdapter : BaseAdapter
    {
        public AutomapperAdapter(IMapper mapper)
            : base(mapper)
        {
        }
    }
}