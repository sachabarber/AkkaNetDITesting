using System;
using Akka.Actor;
using Akka.TestKit.NUnit3;
using AkkaDITest.Actors;
using AkkaDITest.IOC;
using AkkaDITest.Messages;
using AkkaDITest.Services;
using AkkaDITest.Tests.Actors;
using Moq;
using NUnit.Framework;

namespace AkkaDITest.Tests
{
    [TestFixture]
    public class MySupervisorActorTests : TestKit
    {
        [Test]
        public void Correct_Message_Received_When_Using_TestChildActor_Test()
        {
            // Arrange

            Mock<IChildActorCreator> mockChildActorCreator = new Mock<IChildActorCreator>();

            mockChildActorCreator
                .Setup(x => x.Create<MyChildActor>(It.IsAny<IActorContext>()))
                .Returns(() => ActorOf(Props.Create(() => new TestChildActor(new FooService()))));

            Mock<ISomeService> mockSomeService = new Mock<ISomeService>();
            mockSomeService.Setup(x => x.ReturnValue(It.IsAny<string>())).Returns("In a test mock");

            var mySupervisorActor = Sys.ActorOf(Props.Create(() => new MySupervisorActor(mockSomeService.Object, mockChildActorCreator.Object)));

            // Act
            mySupervisorActor.Tell(new StartMessage(), TestActor);

            // Assert
            AwaitCondition(() => HasMessages, TimeSpan.FromSeconds(10));
            var message = ExpectMsg<ChildSucceededMessage>();
            Assert.AreEqual("TestChildActor", message.FromWho);
        }
    }
}
