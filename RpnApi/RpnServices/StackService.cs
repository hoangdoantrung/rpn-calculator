using RpnInfrastructures.Logging;
using RpnModels;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace RpnServices
{
    public class StackService : IStackService
    {
        private readonly IApiLogger _apiLogger;
        private Lazy<ConcurrentDictionary<int, ConcurrentStack<decimal>>> _stacks;
        private int _newId;
        public StackService(IApiLogger apiLogger)
        {
            _stacks = new Lazy<ConcurrentDictionary<int, ConcurrentStack<decimal>>>(() => new ConcurrentDictionary<int, ConcurrentStack<decimal>>());
            _apiLogger = apiLogger;
        }

        public Task<bool> Clear(int id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> CreateOperator(int id, Operator op)
        {
            throw new NotImplementedException();
        }

        public int CreateStack()
        {
            var newId = Interlocked.Increment(ref _newId);
            if (!_stacks.Value.TryAdd(newId, new ConcurrentStack<decimal>()))
            {
                _apiLogger.Error("Could not create a new stack", null, new Dictionary<string, object>() 
                {
                    {nameof(newId), newId}
                });
                return -1;
            }
            
            _apiLogger.Fuctional("CreateStack");
            return newId;
        }

        public Task<StackModel> GetById(int id)
        {
            throw new NotImplementedException();
        }

        public Task<StackModel> Push(int id, decimal item)
        {
            throw new NotImplementedException();
        }

    }
}
