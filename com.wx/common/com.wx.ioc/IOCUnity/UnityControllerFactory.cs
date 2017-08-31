using System;
using System.Configuration;
using System.Web.Mvc;
using System.Web.Routing;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.Configuration;

namespace com.wx.ioc.IOCUnity
{
    public class UnityControllerFactory : DefaultControllerFactory
    {

        private IUnityContainer container;

        public UnityControllerFactory()
            : this(new UnityContainer())
        {
        }




        public UnityControllerFactory(IUnityContainer container)
        {
            if (ConfigurationManager.GetSection(UnityConfigurationSection.SectionName) != null)
            {
                try
                {
                    var configuration = (UnityConfigurationSection)ConfigurationManager.GetSection(UnityConfigurationSection.SectionName);
                    //var configuration = (UnityConfigurationSection)ConfigurationManager.GetSection("unity");
                    configuration.Configure(container, "defaultContainer");
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    throw;
                }
                this.container = container;
            }
        }


        protected override IController GetControllerInstance(RequestContext requestContext, Type controllerType)
        {
            return null == controllerType ? null : (IController)this.container.Resolve(controllerType);
            //return base.GetControllerInstance(requestContext, controllerType);
        }
    }
}
