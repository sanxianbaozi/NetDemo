using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;
using GameServer.Servers;

namespace GameServer.Controller
{
    class UserController : BaseController
    {
        public UserController()
        {
            requestCode = RequestCode.User;
        }

        public string Login(string data, Client client, Server server)
        {
            string[] strs = data.Split(',');
            Console.WriteLine("登陆-->用户名：" + strs[0] + " 密码：" + strs[1]);
            return ((int)ReturnCode.Success).ToString();
        }
    }
}
