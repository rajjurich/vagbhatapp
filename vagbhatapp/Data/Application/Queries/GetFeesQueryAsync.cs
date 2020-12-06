using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using vagbhatapp.Data.Dtos;
using vagbhatapp.Data.Request;

namespace vagbhatapp.Data.Application.Queries
{
    public class GetFeesQueryAsync : IRequest<FeesDto>
    {
        public readonly FeesRequest feesRequest;
        public GetFeesQueryAsync(FeesRequest feesRequest)
        {
            this.feesRequest = feesRequest;
        }
    }
}
