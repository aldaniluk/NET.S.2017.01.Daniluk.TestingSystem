using Domain.Abstract;
using Domain.Consrete;
using Ninject;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApplication.Infrastructure
{
    public class NinjectDependencyResolver : IDependencyResolver
    {
        private IKernel kernel;

        public NinjectDependencyResolver(IKernel kernel)
        {
            this.kernel = kernel;
            Configure();
        }

        public object GetService(Type serviceType)
        {
            return kernel.TryGet(serviceType);
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return kernel.GetAll(serviceType);
        }

        public void Configure()
        {
            kernel.Bind<DbContext>().To<Context>().InSingletonScope();
            kernel.Bind<ITestRepository>().To<TestRepository>();
            kernel.Bind<IQuestionRepository>().To<QuestionRepository>();
            kernel.Bind<IAnswerRepository>().To<AnswerRepository>();
            kernel.Bind<IUserRepository>().To<UserRepository>();
            kernel.Bind<IRoleRepository>().To<RoleRepository>();
            kernel.Bind<IStatisticRepository>().To<StatisticRepository>();
        }
    }
}