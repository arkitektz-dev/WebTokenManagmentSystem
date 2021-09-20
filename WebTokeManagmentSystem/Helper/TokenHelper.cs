using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using WebTokenManagmentSystem.Authentication.enums;
using WebTokenManagmentSystem.Models;
using System.Speech;
using System.Speech.Synthesis;


namespace WebTokenManagmentSystem.Helper
{
    public class TokenHelper : ITokenHelper
    {
        private readonly WebTokenManagmentSystemDBContext _context;
        private IConfiguration _config;

        public TokenHelper(WebTokenManagmentSystemDBContext context, IConfiguration config)
        {
            _config = config;
            _context = context;
        }

        public bool VerifyIfTokenAlreadyAssigned(string TokeNumber)
        {
            var count_row = _context
                .UserTokens
                .Where(x => x.TokenId == FindTokenByTokenNumber(TokeNumber).Id
                 && x.CreatedDate.Value.Date == DateTime.Now.Date)
                .FirstOrDefault();

            if (count_row != null)
                return true;
            else
                return false;


        }

        public void InsertTokenStatus(string TokenNumber, GlobalEnums.Status status_number)
        {
            TokenStatusHistory insert_row = new TokenStatusHistory()
            {
                Status = (byte?)status_number,
                TokenId = FindTokenByTokenNumber(TokenNumber).Id
            };

            _context.TokenStatusHistories.Add(insert_row);
            _context.SaveChanges();

        }

        public bool VerifyTokenStatus(byte status)
        {
            var checkStatus = _context.Statuses.Where(x => x.Id == status).Count() > 0;
            if (checkStatus)
                return true;
            else
                return false;
            



        }

        public bool VerifyTokenExsistsWithSameStatus(string Token_Number, byte status)
        {


            var token_record_count = _context.TokenStatusHistories.Where(
                x => x.TokenId == FindTokenByTokenNumber(Token_Number).Id
                && x.Status == status
                && x.CreatedDate.Value.Date == DateTime.Now.Date).Count();

            if (token_record_count > 0)
                return true;
            else
                return false;


        }

        public bool VerifyTokenExsists(string Token_Number)
        {


            var token_record_count = _context.Tokens.Where(x => x.CustomTokenNumber == Token_Number
            && x.CreatedDate.Value.Date == DateTime.Now.Date).Count();

            if (token_record_count > 0)
                return true;
            else
                return false;


        }

        public Token FindTokenByTokenNumber(string Token_Number)
        {

            //int value_token_number = Convert.ToInt32(Regex.Replace(Token_Number, "[A-Za-z]", ""));

            Token token_record = _context.Tokens.Where(x => x.CustomTokenNumber == Token_Number && x.CreatedDate.Value.Day == DateTime.Now.Day).FirstOrDefault();

            return token_record;

        }

        public bool VerifyUserById(string user_id)
        {

            var user_count = _context.AspNetUsers.Where(x => x.Id == user_id).Count();

            if (user_count > 0)
                return true;
            else
                return false;

        }

        public bool VerifyTokenById(string Token_Number)
        {

            int value_token_number = Convert.ToInt32(Regex.Replace(Token_Number, "[A-Za-z]", ""));

            var token_count = _context.Tokens.Where(x => x.TokenNumber == value_token_number
            && x.CreatedDate.Value.Date == DateTime.Now.Date).Count();

            if (token_count > 0)
                return true;
            else
                return false;
        }


        public Token GenerateNewToken(int? isCustomer)
        {

            //Create a new Token
            Token objRow = new Token()
            {
                TokenNumber = GetTokenNumber(isCustomer),

                CustomTokenNumber = isCustomer == Convert.ToInt32(GlobalEnums.CustomerType.Customer) ? "C" + GetTokenNumber(isCustomer).Value.ToString("D5") : "N" + GetTokenNumber(isCustomer).Value.ToString("D5"),
                IsCustomer = isCustomer == 1 ? true : false,
                Status = (byte?)GlobalEnums.Status.Pending

            };

            if (objRow.TokenNumber == null)
                objRow.TokenNumber = Convert.ToInt32(GetStartCounter().SettingValue);

            _context.Tokens.Add(objRow);
            _context.SaveChanges();


         
           
        
            return objRow;


        }

         

        public int? GetTokenNumber(int? isCustomer)
        {
            int? number = _context.Tokens
                     .Where(x => x.CreatedDate.Value.Date == DateTime.Now.Date)
                     .Where(x => x.IsCustomer == (isCustomer == Convert.ToInt32(GlobalEnums.CustomerType.Customer) ? true : false))
                     .Select(x => x.TokenNumber)
                     .Max() + 1;

            if (number == null)
            {
                number = Convert.ToInt32(GetStartCounter().SettingValue);
            }


            return number;

        }

        public AppSetting GetStartCounter()
        {
            return _context.AppSettings.Where(x => x.SettingKey == "Counter_Start").FirstOrDefault();

        }

        public int? GenerateCounterNumber()
        {
            int? get_latest_count = _context.Counters.Select(x => x.Number).FirstOrDefault();

            if (get_latest_count != null)
                return get_latest_count + 1;
            else
                return 1;
        }

        public CounterTokenRelation GetCounterDetailById(int TokenID)
        {
            return _context.CounterTokenRelations.Where(x => x.TokenId == TokenID).FirstOrDefault();
        }
      



    }
}
