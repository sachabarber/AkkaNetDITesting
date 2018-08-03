using System;
using Akka.Actor;
using AkkaDITest.Messages;
using AkkaDITest.Services;

namespace AkkaDITest.Actors
{
    public  class MyChildActor : ReceiveActor
    {
        private IFooService _fooService;

        public MyChildActor(IFooService fooService)
        {
            _fooService = fooService;

            Receive<BeginChildMessage>(message =>
            {
                var x = _fooService.ReturnValue(12);
                Console.WriteLine($"IFooService.ReturnValue(12) gave result {x}");

                Sender.Tell(new ChildSucceededMessage("MyChildActor"));
            });
        }
    }
}
