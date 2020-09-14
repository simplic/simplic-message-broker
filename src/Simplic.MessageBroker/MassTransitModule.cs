//using Simplic.Configuration;
//using Simplic.Framework.Base.Data;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading;
//using System.Threading.Tasks;
//using Unity;

//namespace Simplic.MessageBroker
//{
//    [ModuleDesc("Message Consumer", "65b57a52-a9af-45cf-8949-6aa4ffe9b6d3")]
//    public class MassTransitModule : ModuleBase
//    {
//        public MassTransitModule(Guid moduleGuid)
//            : base(moduleGuid)
//        {

//        }

//        public override bool LoadModule(string serviceName, string maschineName) => true;
//        public override bool Start()
//        {
//            var container = CommonServiceLocator.ServiceLocator.Current.GetInstance<IUnityContainer>();
//            container.InitializeMassTransitForServer(container.Resolve<IConfigurationService>());


//            return true;
//        }
//    }
//}
