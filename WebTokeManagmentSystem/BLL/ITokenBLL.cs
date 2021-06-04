using System.Collections.Generic;
using WebTokenManagmentSystem.Authentication.Params;
using WebTokenManagmentSystem.Dtos.Token;

namespace WebTokenManagmentSystem.BLL
{
    public interface ITokenBLL
    {
        NewTokendto AddNewToken(TokenModel model);
        List<ListTokenDto> ListToken(int? token_status, int? customer_Type);
        TokenDto AssignToken(UserTokenModel model);
        StatusChangeDto ChangeTokenStatus(StatusChangeModel model);
        AddService AddService(ServiceBody model);
        CounterTypeBodyDto AddCounterType(CounterTypeBody model);
        CounterServiceRelationDto AssignCounterServiceRelation(CounterRelationBody model);
        CounterDto AddCounter(CounterBody model);
        CounterTokenDto AssignTicketToCounter(CounterTokenBody model); 
        CompleteTicketDto SubmittedTicket(CompleteTicketBody model);
        List<TokenCounterDto> ListCounterToken();
    }
}