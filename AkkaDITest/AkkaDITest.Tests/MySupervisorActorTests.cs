using System;
using Akka.Actor;
using Akka.DI.AutoFac;
using Akka.DI.Core;
using Akka.TestKit.NUnit3;
using AkkaDITest.Actors;
using AkkaDITest.IOC;
using AkkaDITest.Messages;
using AkkaDITest.Services;
using AkkaDITest.Tests.Actors;
using Autofac;
using Moq;
using NUnit.Framework;
using NUnit.Framework.Internal;

namespace AkkaDITest.Tests
{
    [TestFixture]
    public class MySupervisorActorTests : TestKit
    {
        [SetUp]
        public void SetUp()
        {
            ContainerOperations.Instance.ReInitialise();
        }

        [Test]
        public void Correct_Message_Received_When_Using_TestChildActor_Test()
        {
            //Setup stuff for this testcase
            Mock<ISomeService> mockSomeService = new Mock<ISomeService>();
            mockSomeService.Setup(x => x.ReturnValue(It.IsAny<string>())).Returns("In a test mock");
            ContainerOperations.Instance.AddExtraModulesCallBack = builder =>
            {

                builder.Register(x=> mockSomeService.Object)
                    .As<ISomeService>()
                    .SingleInstance();


                builder.RegisterType<TestChildActorCreator>()
                    .As<IChildActorCreator>()
                    .SingleInstance();

                builder.RegisterType<TestChildActor>();

            };

            var system = ContainerOperations.Instance.Container.Resolve<ActorSystem>();
            IDependencyResolver resolver = new AutoFacDependencyResolver(ContainerOperations.Instance.Container, system);
            var mySupervisorActor = system.ActorOf(system.DI().Props<MySupervisorActor>(), "MySupervisorActor");
            mySupervisorActor.Tell(new StartMessage(), TestActor);


            // Assert
            AwaitCondition(() => HasMessages, TimeSpan.FromSeconds(10));
            var message = ExpectMsg<ChildSucceededMessage>();
            Assert.AreEqual("TestChildActor", message.FromWho);
        }
    }
}
