using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;
using System.Reflection;
using GameServer.Servers;

namespace GameServer.Controller
{
    class ControllerManager
    {
        private Dictionary<RequestCode, BaseController> controllerDic = new Dictionary<RequestCode, BaseController>();
        private Server server;

        public ControllerManager(Server server)
        {
            this.server = server;
            InitController();
        }

        void InitController()
        {
            DefaultController defaultController = new DefaultController();
            controllerDic.Add(defaultController.RequestCode, defaultController);
            controllerDic.Add(RequestCode.User, new UserController());
        }

        public void HandleRequest(RequestCode requestCode, ActionCode actionCode, string data, Client client)
        {
            BaseController controller;
            bool isGet = controllerDic.TryGetValue(requestCode, out controller);
            if(isGet == false)
            {
                Console.WriteLine("无法获得controller，无法请求");
                return;
            }

            string methodName = Enum.GetName(typeof(ActionCode), actionCode);
            MethodInfo mi= controller.GetType().GetMethod(methodName);
            if (mi == null)
            {
                Console.WriteLine("controller中没有对应的处理方法");
                return;
            }
            object[] parameters = new object[] { data, client, server };
            object o = mi.Invoke(controller, parameters);

            if (o == null || string.IsNullOrEmpty(o as string))
            {
                Console.WriteLine("无法返回！");
                return;
            }
            server.SendResponse(client, actionCode, o as string);
            Console.WriteLine("SendResponse！");
        }
    }
}
