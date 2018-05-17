using System;
using Akka.Actor;
using Akka.DI.AutoFac;
using Akka.DI.Core;
using AkkaDITest.Actors;
using AkkaDITest.IOC;
using AkkaDITest.Messages;
using Autofac;

namespace AkkaDITest
{
    class Program
    {
        static void Main(string[] args)
        {
            var system = ContainerOperations.Instance.Container.Resolve<ActorSystem>();
            IDependencyResolver resolver = new AutoFacDependencyResolver(ContainerOperations.Instance.Container, system);
            var mySupervisorActor = system.ActorOf(system.DI().Props<MySupervisorActor>(), "MySupervisorActor");
            mySupervisorActor.Tell(new StartMessage());

            Console.ReadLine();
        }
    }
}
