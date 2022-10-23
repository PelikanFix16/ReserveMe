using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentResults;
using SharedKernel.Domain.Aggregate;
using SharedKernel.Domain.UniqueKey;

namespace SharedKernel.Application.Interfaces.Repositories
{
    public interface IAggregateRepository
    {
        Result Save(AggregateRoot aggregate, AggregateKey key);
        Task<Result<T>> GetAsync<T>(AggregateKey key) where T : AggregateRoot, new();
        Task<Result> CommitAsync();
    }
}
