using RpnModels;
using System.Threading.Tasks;

namespace RpnServices
{
    public interface IStackService
    {
        int CreateStack();
        Task<StackModel> GetById(int id);
        Task<StackModel> Push(int id, decimal item);
        Task<bool> Clear(int id);
        Task<bool> CreateOperator(int id, Operator op);
    }
}
