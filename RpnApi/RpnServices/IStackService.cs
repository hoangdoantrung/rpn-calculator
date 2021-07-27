using RpnModels;

namespace RpnServices
{
    public interface IStackService
    {
        int CreateStack();
        StackModel GetById(int id);
        StackModel Push(int id, decimal item);
        bool Clear(int id);
        StackModel CreateOperator(int id, Operator op);
    }
}
