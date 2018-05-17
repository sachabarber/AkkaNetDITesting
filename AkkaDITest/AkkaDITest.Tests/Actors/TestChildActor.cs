using System;
using Akka.Actor;
using AkkaDITest.Messages;
using AkkaDITest.Services;

namespace AkkaDITest.Tests.Actors
{
    public  class TestChildActor : ReceiveActor
    {
        private IFooService _fooService;


        public TestChildActor(IFooService fooService)
        {
            _fooService = fooService;

            Receive<BeginChildMessage>(message =>
            {
                var x = _fooService.ReturnValue(12);
                Console.WriteLine($"IFooService.ReturnValue(12) gave result {x}");

                Sender.Tell(new ChildSucceededMessage("TestChildActor"));
            });
        }
    }
}
