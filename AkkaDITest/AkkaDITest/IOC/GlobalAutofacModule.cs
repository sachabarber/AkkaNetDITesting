using Akka.Actor;
using AkkaDITest.Actors;
using AkkaDITest.Services;
using Autofac;
using System;

namespace AkkaDITest.IOC
{
    public class GlobalAutofacModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<SomeService>()
                .As<ISomeService>()
                .SingleInstance();

            builder.RegisterType<FooService>()
                .As<IFooService>()
                .SingleInstance();

            builder.RegisterType<ChildActorCreator>()
                .As<IChildActorCreator>()
                .SingleInstance();

            builder.RegisterType<MySupervisorActor>();
            builder.RegisterType<MyChildActor>();

            var _runModelActorSystem = new Lazy<ActorSystem>(() =>
            {
                return ActorSystem.Create("MySystem");
            });

            builder.Register<ActorSystem>(cont => _runModelActorSystem.Value);


        }
    }
}
