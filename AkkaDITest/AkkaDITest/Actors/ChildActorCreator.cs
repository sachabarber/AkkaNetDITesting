using System;
using System.Collections.Generic;
using System.Text;
using Akka.Actor;
using Akka.DI.Core;
using AkkaDITest.Messages;

namespace AkkaDITest.Actors
{

    public interface IChildActorCreator
    {
        Props GetChild(string actorNameKey, IUntypedActorContext context);
    }


    public class ChildActorCreator : IChildActorCreator
    {
        private Dictionary<string, Func<IUntypedActorContext, Props>> _propLookup =
            new Dictionary<string, Func<IUntypedActorContext, Props>>();

        public ChildActorCreator()
        {
            _propLookup.Add(ActorNames.MyChildActorName, (context) => context.DI().Props<MyChildActor>());
        }

        public Props GetChild(string actorNameKey, IUntypedActorContext context)
        {
            return _propLookup[actorNameKey](context);
        }

        public string Name => "ChildActorCreator";

    }
}
