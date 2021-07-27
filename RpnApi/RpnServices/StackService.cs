using RpnInfrastructures.Exceptions;
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
            if (!_stacks.Value.TryGetValue(id, out var stack))
            {
                throw new NotFoundException($"Could not find stack with id:{id}");
            }
            _stacks.Value.AddOrUpdate(id, new ConcurrentStack<decimal>(), (x,y) => new ConcurrentStack<decimal>());

            _apiLogger.Fuctional("Clear");
            return true;
        }

        public StackModel CreateOperator(int id, Operator op)
        {
            if (!_stacks.Value.TryGetValue(id, out var stack))
            {
                throw new NotFoundException($"Could not find stack with id:{id}");
            }
            if (!stack.TryPop(out var lastOperand))
            {
                throw new InvalidRequestException($"Could not apply operator {op} because lack of operand.");
            }
            if (!stack.TryPop(out var firstOperand))
            {
                throw new InvalidRequestException($"Could not apply operator {op} because lack of operand.");
            }
            switch (op)
            {
                case Operator.Add:
                    stack.Push(firstOperand + lastOperand);
                    break;
                case Operator.Substract:
                    stack.Push(firstOperand - lastOperand);
                    break;
                case Operator.Multiply:
                    stack.Push(firstOperand * lastOperand);
                    break;
                case Operator.Divide:
                    stack.Push(firstOperand / lastOperand);
                    break;
            }

            _apiLogger.Fuctional("CreateOperator");
            return new StackModel()
            {
                Id = id,
                Operands = stack.ToArray()
            };
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

        public bool Delete(int id)
        {
            if (!_stacks.Value.TryGetValue(id, out var _))
            {
                return true;
            }
            return _stacks.Value.TryRemove(id, out var _);
        }

        public IList<StackModel> GetAll()
        {
            // TODO: add pagination
            return _stacks.Value.Select(s => new StackModel() 
            {
                Id = s.Key,
                Operands = s.Value.ToArray()
            }).ToList();
        }

        public StackModel GetById(int id)
        {
            if (!_stacks.Value.TryGetValue(id, out var stack))
            {
                throw new NotFoundException($"Could not find stack with id:{id}");
            }

            _apiLogger.Fuctional("GetById");

            return new StackModel() 
            { 
                Id = id,
                Operands = stack.ToArray()
            };
        }

        public StackModel Push(int id, decimal item)
        {
            if (!_stacks.Value.TryGetValue(id, out var stack))
            {
                throw new NotFoundException($"Could not find stack with id:{id}");
            }
            stack.Push(item);

            _apiLogger.Fuctional("Push");

            return new StackModel()
            {
                Id = id,
                Operands = stack.ToArray()
            };
        }
    }
}