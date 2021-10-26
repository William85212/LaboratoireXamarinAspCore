using DataAccesLayer.Interfaces;
using DataAccesLayer.Models;
using DataAccesLayer.Tools;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccesLayer.Services
{
    public class ServicesNewLogin : ApiRequester, IServicesNewLogin
    {
        public ServicesNewLogin(IConfiguration config) : base(config.GetSection("BaseAddres").Value)
        {

        }

        public void NewPassword(NewLoginModel mdl)
        {
            Update<NewLoginModel>("User/setLogin", mdl);
        }
    }
}
