using SharedKernel.Domain.UniqueKey;

namespace SharedKernel.Application.Exceptions
{
    public class ConcurrencyException : Exception
    {
        public ConcurrencyException(AggregateKey key): 
        base($"A diffrent version than excepted was found in aggregate {key}")
        {
            
        }
    }
}