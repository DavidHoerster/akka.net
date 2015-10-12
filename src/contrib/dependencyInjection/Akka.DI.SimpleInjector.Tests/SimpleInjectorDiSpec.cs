using System;
using System.Diagnostics;
using Akka.Actor;
using Akka.DI.Core;
using Akka.DI.TestKit;
using SimpleInjector;

namespace Akka.DI.SimpleInjector.Tests
{
    public class SimpleInjectorDiSpec : DiResolverSpec
    {
        protected override object NewDiContainer()
        {
            return new Container();
        }

        protected override IDependencyResolver NewDependencyResolver(object diContainer, ActorSystem system)
        {
            var container = ToContainer(diContainer);
            return new SimpleInjectorDependencyResolver(container, system);
        }

        protected override void Bind<T>(object diContainer, Func<T> generator)
        {
            var container = ToContainer(diContainer);
            container.Register(typeof(T), () => generator());
        }

        protected override void Bind<T>(object diContainer)
        {
            var container = ToContainer(diContainer);
            container.Register(typeof(T));
        }

        private static Container ToContainer(object diContainer)
        {
            var container = diContainer as Container;
            Debug.Assert(container != null, "container != null");
            return container;
        }
    }
}
