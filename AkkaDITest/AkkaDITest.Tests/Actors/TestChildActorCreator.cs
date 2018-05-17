using System;
using System.Collections.Generic;
using Akka.Actor;
using Akka.DI.Core;
using AkkaDITest.Actors;
using AkkaDITest.Messages;

namespace AkkaDITest.Tests.Actors
{
    public class TestChildActorCreator : IChildActorCreator
    {
        private Dictionary<Type, Func<IUntypedActorContext, IActorRef>> _propLookup =
            new Dictionary<Type, Func<IUntypedActorContext, IActorRef>>();

        public TestChildActorCreator()
        {
            _propLookup.Add(typeof(BeginChildMessage), (context) => context.ActorOf(context.DI().Props<TestChildActor>(), ActorNames.MyChildActorName));
        }

        public IActorRef GetChild<T>(IUntypedActorContext context)
        {
            return _propLookup[typeof(T)](context);
        }


        public string Name => "TestChildActorCreator";
    }
}
