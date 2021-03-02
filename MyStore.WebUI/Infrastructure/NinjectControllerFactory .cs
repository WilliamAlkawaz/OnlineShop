using System; 
using System.Web.Routing;
using System.Web.Mvc;
using Ninject;
using MyStore.Domain.Entities;
using MyStore.Domain.Abstract;
using System.Collections.Generic;
using System.Linq;
using Moq;
using MyStore.Domain.Concrete;
using MyStore.WebUI.Infrastructure.Abstract;
using MyStore.WebUI.Infrastructure.Concrete;

namespace MyStore.WebUI.Infrastructure
{
    public class NinjectControllerFactory : DefaultControllerFactory {
        private IKernel ninjectKernel;
        public NinjectControllerFactory() { ninjectKernel = new StandardKernel(); AddBindings(); }
        protected override IController GetControllerInstance(RequestContext requestContext, Type controllerType)
        {
            return controllerType == null ? null : (IController)ninjectKernel.Get(controllerType);
        }
        private void AddBindings() {
            ninjectKernel.Bind<IProductRepository>().To<EFProductRepository>();
            ninjectKernel.Bind<ICategoryRepository>().To<EFCategoryRepository>();
            ninjectKernel.Bind<ICategory_ProductRepository>().To<EFCategory_ProductRepository>();

            ninjectKernel.Bind<IAuthProvider>().To<FormsAuthProvider>();
        }
    }
}