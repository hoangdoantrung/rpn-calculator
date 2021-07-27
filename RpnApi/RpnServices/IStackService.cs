using RpnModels;
using System.Collections.Generic;

namespace RpnServices
{
    public interface IStackService
    {
        int CreateStack();
        StackModel GetById(int id);
        StackModel Push(int id, decimal item);
        bool Clear(int id);
        bool Delete(int id);
        StackModel CreateOperator(int id, Operator op);
        IList<StackModel> GetAll();
    }
}
