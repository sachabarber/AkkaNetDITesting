using System;
using Akka.Actor;
using AkkaDITest.Messages;
using AkkaDITest.Services;

namespace AkkaDITest.Actors
{
    public  class MySupervisorActor : ReceiveActor
    {
        private readonly ISomeService _someService;
        private readonly IChildActorCreator _childActorCreator;
        private IActorRef originalSender;

        public MySupervisorActor(ISomeService someService, IChildActorCreator childActorCreator)
        {
            _someService = someService;
            _childActorCreator = childActorCreator;
            Receive<StartMessage>(message =>
            {
                originalSender = Sender;
                var x = _someService.ReturnValue("war is a big business");
                Console.WriteLine($"ISomeService.ReturnValue(\"war is a big business\") gave result {x}");

                var childActor = _childActorCreator.GetChild<BeginChildMessage>(Context);
                childActor.Tell(new BeginChildMessage());

            });
            Receive<ChildSucceededMessage>(message =>
            {

                Console.WriteLine($"{message.FromWho}_ChildSucceededMessage");
                originalSender.Tell(message);
            });
            Receive<ChildFailedMessage>(message =>
            {
                Console.WriteLine($"{message.FromWho}_ChildFailedMessage");
                originalSender.Tell(message);
            });
        }
    }
}
