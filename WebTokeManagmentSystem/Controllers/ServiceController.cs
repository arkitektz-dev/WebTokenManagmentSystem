using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using WebTokenManagmentSystem.Models;
using WebTokenManagmentSystem.Authentication.enums;
using WebTokenManagmentSystem.Authentication.Params;
using WebTokenManagmentSystem.Dtos.Token;
using System.Text.RegularExpressions;
using WebTokenManagmentSystem.Helper;
using WebTokenManagmentSystem.BLL;
using WebTokenManagmentSystem.Params;
using WebTokenManagmentSystem.LINQExtension;
using WebTokenManagmentSystem.Service;
using Microsoft.Extensions.Hosting;

namespace WebTokenManagmentSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServiceController : ControllerBase
    {
        private readonly WebTokenManagmentSystemDBContext context;
        private IConfiguration config;
        private readonly ITokenHelper tokenHelper;
        private readonly ITokenBLL tokenBLL;

        public ServiceController(WebTokenManagmentSystemDBContext _context, 
            IConfiguration _config,
            ITokenHelper _tokenHelper, 
            ITokenBLL _tokenBLL)
        {
            config = _config;
            context = _context;
            tokenHelper = _tokenHelper;
            tokenBLL = _tokenBLL;
        }

        /// <summary>
        /// list counter service
        /// </summary>
        /// <returns></returns>
        /// 
        [HttpPost]
        [Route("GetListCounter")]
        public IActionResult Generate_Customer_Token([FromBody] TokenModel model)
        {
            var message = tokenBLL.AddNewToken(model);

            if (message != null)
            {
                return Ok(message);
            }
            else
            {
                return NotFound();
            }

        }

    }
}
