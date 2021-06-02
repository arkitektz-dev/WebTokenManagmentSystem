using WebTokenManagmentSystem.Authentication.Params;
using WebTokenManagmentSystem.Dtos.Counter;

namespace WebTokenManagmentSystem.BLL
{
    public interface ICounterBLL
    {
        CounterDetailDto GetCounterDetailByUserId(CounterDetailBody model);
    }
}