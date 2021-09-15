using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebTokenManagmentSystem.Authentication.enums;
using WebTokenManagmentSystem.Authentication.Params;
using WebTokenManagmentSystem.Dtos.Counter;
using WebTokenManagmentSystem.Models;

namespace WebTokenManagmentSystem.BLL
{
    public class CounterBLL : ICounterBLL
    {
        private readonly WebTokenManagmentSystemDBContext context;
        private IConfiguration config; 


        public CounterBLL(WebTokenManagmentSystemDBContext _context, IConfiguration _config)
        {
            config = _config;
            context = _context;
        }

        public CounterDetailDto GetCounterDetailByUserId(CounterDetailBody model)
        {
            var count_user = context.AspNetUsers.Where(x => x.Id == model.UserId).Count() > 0;
            if (!count_user)
                //Error : user Not found
                return null;

            var count_counter_user = context.Counters.Where(x => x.CounterUserId == model.UserId).Count() > 0;
            if (!count_counter_user)
                //Error : no user assigned to this counter 
                return null;

          
            //Find Counter Number
            var counter_number = context.Counters.Where(x => x.CounterUserId == model.UserId).FirstOrDefault();
         

            //Fill all status
            List<Status> list_status = context.Statuses.ToList();


            //Find Service Master Id
            var master_id = context.CounterServiceRelations
                .Where(x => x.Id == counter_number.Csrid)
                .Select(x => x.ServiceMasterId)
                .FirstOrDefault();

            //Fill all service
            List<ServiceOption> List_service =
                context.ServiceOptions
                .Where(x => x.ServiceMasterId == master_id)
                .ToList();


            var return_message = new CounterDetailDto()
            {
                CounterNumber = (int)counter_number.Number,
                CounterService = List_service,
                CounterStatus = context.Statuses.ToList(),
                CounterID = counter_number.Id
            };

            return return_message;
        }

        public List<Counter> ListCounter()
        {
            return context.Counters.ToList();
        }

        public List<CounterTokenBody> ViewCounterActivity(int? CounterId)
        {
            var listToken = context.CounterTokenRelations.Where(x => x.CounterId == CounterId).ToList();

            List<CounterTokenBody> List = new List<CounterTokenBody>();
            foreach (var item in listToken) {


                CounterTokenBody row = new CounterTokenBody();
                var rowItem = context.Tokens.Where(x => x.Id == item.TokenId).FirstOrDefault();
                row.TokenNumber = rowItem.CustomTokenNumber;
                row.StatusId = (int)item.StatusId;
                if (rowItem.CompleteDate != null) { 
                    row.CompletedDate = (DateTime)rowItem.CompleteDate;
                    row.ServingTime = (DateTime)context.TokenStatusHistories.Where(x => x.Status == (byte)GlobalEnums.Status.Serving
                    && x.TokenId == item.TokenId).Select(x => x.CreatedDate).FirstOrDefault();
                }
                row.CreatedDate = (DateTime)rowItem.CreatedDate;
                if (row.StatusId != (int)GlobalEnums.Status.Serving) { 
                    row.ServiceType = context.ServiceOptions.Where(x => x.Id == rowItem.ServiceOptionId).Select(x => x.Name).FirstOrDefault().ToString();
                }

                List.Add(row);
            }

            return List;
        }
    }
}
