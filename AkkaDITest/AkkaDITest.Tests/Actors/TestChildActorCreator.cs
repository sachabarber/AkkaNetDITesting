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
        private Dictionary<string, Func<IUntypedActorContext, Props>> _propLookup =
            new Dictionary<string, Func<IUntypedActorContext, Props>>();

        public TestChildActorCreator()
        {
            _propLookup.Add(ActorNames.MyChildActorName, (context) => context.DI().Props<TestChildActor>());
        }

        public Props GetChild(string actorNameKey, IUntypedActorContext context)
        {
            return _propLookup[actorNameKey](context);
        }


        public string Name => "TestChildActorCreator";
    }
}
