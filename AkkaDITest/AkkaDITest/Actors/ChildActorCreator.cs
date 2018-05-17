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
        IActorRef GetChild<T>(IUntypedActorContext context);
    }


    public class ChildActorCreator : IChildActorCreator
    {
        private Dictionary<Type, Func<IUntypedActorContext, IActorRef>> _propLookup =
            new Dictionary<Type, Func<IUntypedActorContext, IActorRef>>();

        public ChildActorCreator()
        {
            _propLookup.Add(typeof(BeginChildMessage), (context) => context.ActorOf(context.DI().Props<MyChildActor>(), ActorNames.MyChildActorName));
        }

        public IActorRef GetChild<T>(IUntypedActorContext context)
        {
            return _propLookup[typeof(T)](context);
        }

        public string Name => "ChildActorCreator";

    }
}
