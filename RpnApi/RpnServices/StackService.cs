using RpnInfrastructures.Logging;
using RpnModels;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

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

        public bool Clear(int id)
        {
            throw new NotImplementedException();
        }

        public StackModel CreateOperator(int id, Operator op)
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

        public StackModel GetById(int id)
        {
            if (!_stacks.Value.TryGetValue(id, out var stack))
            {
                // TODO: should throw NotFoundException then return 404 to client
                return null;
            }
            return new StackModel() 
            { 
                Id = id,
                Operands = stack.ToList()
            };
        }

        public StackModel Push(int id, decimal item)
        {
            throw new NotImplementedException();
        }
    }
}
