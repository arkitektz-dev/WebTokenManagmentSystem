using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebTokenManagmentSystem.Authentication.enums;
using WebTokenManagmentSystem.Authentication.Params;
using WebTokenManagmentSystem.Dtos.Token;
using WebTokenManagmentSystem.Helper;
using WebTokenManagmentSystem.Models;
using WebTokenManagmentSystem.LINQExtension;

namespace WebTokenManagmentSystem.BLL
{
    public class TokenBLL : ITokenBLL
    {
        private readonly WebTokenManagmentSystemDBContext context;
        private IConfiguration config;
        private readonly ITokenHelper tokenHelper;

        public TokenBLL(WebTokenManagmentSystemDBContext _context, IConfiguration _config, ITokenHelper _tokenHelper)
        {
            config = _config;
            context = _context;
            tokenHelper = _tokenHelper;
        }

        public NewTokendto AddNewToken(TokenModel model)
        {


            int? param_is_customer = Convert.ToInt32(model.CustomerType);
            string CustomerType = "";

            switch (param_is_customer)
            {
                case (1):
                    CustomerType = "C";
                    break;

                case (2):
                    CustomerType = "N";
                    break;

                default:
                    //Invalid Customer Type
                    return null;
            }


            int recordCount = context.
                 Tokens
                 .Where(x => x.CreatedDate.Value.Date == DateTime.Now.Date).ToList().Count();

            //If table is empty
            if (recordCount <= 0)
            {

                Token objRow = new Token()
                {
                    TokenNumber = Convert.ToInt32(tokenHelper.GetStartCounter().SettingValue),
                    IsCustomer = param_is_customer == 1 ? true : false,
                    Status = (byte?)GlobalEnums.Status.Pending
                };

                objRow.CustomTokenNumber = CustomerType + objRow.TokenNumber.Value.ToString("D5");

                context.Tokens.Add(objRow);
                context.SaveChanges();

                tokenHelper.InsertTokenStatus(objRow.CustomTokenNumber, GlobalEnums.Status.Pending);


                var return_message = new NewTokendto()
                {
                    Token = CustomerType + objRow.TokenNumber.Value.ToString("D5"),
                    Date = objRow.CreatedDate.Value.ToString(@"dd/MM/yyyy"),
                    Time = objRow.CreatedDate.Value.ToString(@"hh:m tt")

                };

                return return_message;

            }
            else
            {
                var row = tokenHelper.GenerateNewToken(param_is_customer);

                tokenHelper.InsertTokenStatus(row.CustomTokenNumber, GlobalEnums.Status.Pending);


                var return_message = new NewTokendto()
                {
                    Token = CustomerType + row.TokenNumber.Value.ToString("D5"),
                    Date = row.CreatedDate.Value.ToString(@"dd/MM/yyyy"),
                    Time = row.CreatedDate.Value.ToString(@"hh:m tt"),

                };

                return return_message;


            }





        }

        public List<ListTokenDto> ListToken(int? token_status, int? customer_Type)
        {
            int? token_status_code = 0;
            int? param_token_status = Convert.ToInt32(token_status);
            int? param_customer_type = Convert.ToInt32(customer_Type);
            int? customer_type_id = 0;
            string customer_prefix = "";

            switch (param_token_status)
            {
                case 1:
                    token_status_code = (int?)GlobalEnums.Status.Pending;
                    break;

                case 2:
                    token_status_code = (int?)GlobalEnums.Status.Complete;
                    break;

                case 3:
                    token_status_code = (int?)GlobalEnums.Status.All;
                    break;

                default:
                    token_status_code = (int?)GlobalEnums.Status.Invalid_Status;
                    break;

            }

            switch (param_customer_type)
            {
                case (1):
                    customer_type_id = (int?)GlobalEnums.CustomerType.Customer;
                    customer_prefix = "C";
                    break;

                case (2):
                    customer_type_id = (int?)GlobalEnums.CustomerType.Non_Customer;
                    customer_prefix = "N";
                    break;

                default:
                    //Error : Invalid Customer Type
                    return null;

            }

            if (token_status_code == (int?)GlobalEnums.Status.Invalid_Status)
                //Error : Invalid token status
                return null;

            //var Token_list = context.Tokens
            //     .Where(x => x.Status == token_status_code
            //     && x.CreatedDate.Value.Date == DateTime.Now.Date
            //     && x.IsCustomer == (customer_type_id == (int?)GlobalEnums.CustomerType.Customer ? true : false))
            //     .Select(row => new
            //     {
            //         token = customer_prefix + row.TokenNumber.Value.ToString("D5"),
            //         date = row.CreatedDate.Value.ToString(@"dd/MM/yyyy"),
            //         time = row.CreatedDate.Value.ToString(@"hh:m tt")
            //     });

            var Token_list = context.Tokens
                 .WhereIf(token_status_code != (int?)GlobalEnums.Status.All, x => x.Status == token_status_code
                 && x.CreatedDate.Value.Date == DateTime.Now.Date
                 && x.IsCustomer == (customer_type_id == (int?)GlobalEnums.CustomerType.Customer ? true : false))

                 .WhereIf(token_status_code == (int?)GlobalEnums.Status.All, 
                 x => x.CreatedDate.Value.Date == DateTime.Now.Date
                 && x.IsCustomer == (customer_type_id == (int?)GlobalEnums.CustomerType.Customer ? true : false))
                 .Select(row => new
                 {
                     token = customer_prefix + row.TokenNumber.Value.ToString("D5"),
                     date = row.CreatedDate.Value.ToString(@"dd/MM/yyyy"),
                     time = row.CreatedDate.Value.ToString(@"hh:m tt")
                 });



            List<ListTokenDto> master = new List<ListTokenDto>();

            foreach (var item in Token_list)
            {
                ListTokenDto row = new ListTokenDto();

                row.Token = item.token;
                row.Date = item.date;
                row.Time = item.time;

                master.Add(row);
            }

            var return_message = master;

  
            return return_message;

        }

        public TokenDto AssignToken(UserTokenModel model)
        {
            if (!tokenHelper.VerifyUserById(model.UserId))
                //Error : user id not found
                return null;

            if (!tokenHelper.VerifyTokenById(model.TokenNumber))
                //Error : token number not found
                return null;


            if (tokenHelper.VerifyIfTokenAlreadyAssigned(model.TokenNumber))
                return null;
            

            var token_recored = tokenHelper.FindTokenByTokenNumber(model.TokenNumber);

            UserToken row = new UserToken()
            {
                TokenNumber = token_recored.TokenNumber,
                TokenId = token_recored.Id,
                UserId = model.UserId
            };

            context.Add(row);
            context.SaveChanges();

            var return_message = new TokenDto()
            {
                Id = row.Id,
                TokenNumber = model.TokenNumber,
                UserId = row.UserId

            };

            return return_message;


        }

        public StatusChangeDto ChangeTokenStatus(StatusChangeModel model)
        {
            if (!tokenHelper.VerifyTokenExsists(model.TokenNumber))
                //Error : Token not found
                return null;

            if (!tokenHelper.VerifyTokenStatus(model.Status))
                //Error : Invalid status
                return null;

            if (tokenHelper.VerifyTokenExsistsWithSameStatus(model.TokenNumber, model.Status))
                //Error : Same status for this token already exsists"
                return null;



            var update_token = context.Tokens
                .Where(x => x.Id == tokenHelper.FindTokenByTokenNumber(model.TokenNumber).Id)
                .FirstOrDefault();

            update_token.Status = model.Status;
            context.SaveChanges();

                        

            TokenStatusHistory inserted_Row = new TokenStatusHistory()
            {
                Status = model.Status,
                TokenId = tokenHelper.FindTokenByTokenNumber(model.TokenNumber).Id

            };

            context.TokenStatusHistories.Add(inserted_Row);
            context.SaveChanges();

            var return_message = new StatusChangeDto()
            {
                TokenNumber = inserted_Row.Token.CustomTokenNumber,
                Status = (byte)model.Status
            };

            return return_message;

        }

        public AddService AddService(ServiceBody model)
        {
            ServiceMaster master_row = new ServiceMaster();
            master_row.ServiceName = model.ServiceName;
            context.ServiceMasters.Add(master_row);
            context.SaveChanges();

            foreach (var item in model.ListOption)
            {

                //This will stop duplicate recored
                var count = context.ServiceOptions.Where(x => x.Name == item).Count() > 0;

                if (count)
                    continue;

                ServiceOption option = new ServiceOption();
                option.Name = item;
                option.ServiceMasterId = master_row.Id;
                context.ServiceOptions.Add(option);
                context.SaveChanges();
            }

            var return_message = new AddService()
            {
                Id = master_row.Id,
                ServiceName = master_row.ServiceName
            };


            return return_message;
        }

        public CounterTypeBodyDto AddCounterType(CounterTypeBody model)
        {
            foreach (var item in model.Types)
            {

                var count = context.CounterTypes.Where(x => x.CounterName == item).Count() > 0;
                if (count)
                    continue;

                CounterType row = new CounterType();
                row.CounterName = item;
                context.CounterTypes.Add(row);
                context.SaveChanges();

            }

            var message = new CounterTypeBodyDto()
            {
                Types = model.Types
            };

            return message;
            
        }

        public CounterServiceRelationDto AssignCounterServiceRelation(CounterRelationBody model)
        {
            var count_type = context.CounterTypes.Where(x => x.Id == model.CounterTypeID).Count() > 0;
            if (!count_type)
                //Error : counter type id not found
                return null;

            var count_service_master = context.ServiceMasters.Where(x => x.Id == model.ServiceMasterID).Count() > 0;
            if (!count_service_master)
                //Error : service master id not found
                return null;


            CounterServiceRelation row = new CounterServiceRelation();
            row.CounterTypeId = model.CounterTypeID;
            row.ServiceMasterId = model.ServiceMasterID;
            context.CounterServiceRelations.Add(row);
            context.SaveChanges();

            var return_message = new CounterServiceRelationDto()
            {
                Id = row.Id,
                CounterTypeId = row.CounterTypeId,
                ServiceMasterId = row.ServiceMasterId
            };

            return return_message;
        }

        public CounterDto AddCounter(CounterBody model)
        {
            var count_user = context.AspNetUsers.Where(x => x.Id == model.UserID).Count() > 0;
            if (!count_user)
                //Error : user not found
                return null;

            var count_counter_relation = context.CounterServiceRelations.Where(x => x.Id == model.CounterServiceRelationID).Count() > 0;
            if(!count_counter_relation)
                //Error : Wrong Relation ID
                return null;


            Counter row = new Counter()
            {
                
                CounterUserId = model.UserID,
                Csrid = model.CounterServiceRelationID,
                Number = tokenHelper.GenerateCounterNumber(),
                Description = model.Description
            };

            context.Counters.Add(row);
            context.SaveChanges();


            var return_message = new CounterDto()
            {
                CounterUserId = row.CounterUserId,
                Csrid = row.Csrid,
                Number = row.Number,
                Description = row.Description
         
            };



            return return_message;




        }

        public CounterTokenDto AssignTicketToCounter(CounterTokenBody model)
        {
            var count_counter = context.Counters.Where(x => x.Id == model.CounterId).Count() > 0;
            if (!count_counter)
                //Error : Counter not found
                return null;

            var count_token = context.Tokens.Where(x => x.Id == model.TokenId).Count() > 0;
            if (!count_token)
                //Error : Counter token not found 
                return null;


            var count_status = context.Statuses.Where(x => x.Id == model.StatusId).Count() > 0;
            if (!count_status)
                //Error : Status not found
                return null;


            CounterTokenRelation row = new CounterTokenRelation()
            {
                CounterId = model.CounterId,
                TokenId = model.TokenId,
                StatusId = model.StatusId
            };

            var return_message = new CounterTokenDto()
            {
                CounterId = model.CounterId,
                StatusId = model.StatusId,
                TokenId = model.TokenId
            };

            return return_message;


        }

       


    }
}
