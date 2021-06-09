using System.Collections.Generic;
using WebTokenManagmentSystem.Authentication.Params;
using WebTokenManagmentSystem.Dtos.Counter;
using WebTokenManagmentSystem.Models;

namespace WebTokenManagmentSystem.BLL
{
    public interface ICounterBLL
    {
        CounterDetailDto GetCounterDetailByUserId(CounterDetailBody model);
        List<Counter> ListCounter();
        List<CounterTokenBody> ViewCounterActivity(int? CounterId);
    }
}