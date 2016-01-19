﻿//-----------------------------------------------------------------------
// <copyright file="EchoActor.cs" company="Akka.NET Project">
//     Copyright (C) 2009-2015 Typesafe Inc. <http://www.typesafe.com>
//     Copyright (C) 2013-2015 Akka.NET project <https://github.com/akkadotnet/akka.net>
// </copyright>
//-----------------------------------------------------------------------

using Akka.Actor;

namespace Akka.TestKit.TestActors
{
    /// <summary>
    /// An <see cref="EchoActor"/> is an actor that echoes whatever is sent to it, to the
    /// TestKit's <see cref="TestKitBase.TestActor"/>.
    /// By default it also echoes back to the sender, unless the sender is the <see cref="TestKitBase.TestActor"/>
    /// (in this case the <see cref="TestKitBase.TestActor"/> will only receive one message).
    /// </summary>
    public class EchoActor : ReceiveActor
    {
        public EchoActor(TestKitBase testkit, bool echoBackToSenderAsWell=true)
        {
            ReceiveAny(msg =>
            {
                var sender = Sender;
                var testActor = testkit.TestActor;
                if(echoBackToSenderAsWell && testActor != sender)
                    sender.Forward(msg);
                testActor.Tell(msg,Sender);
            });
        }

        /// <summary>
        /// Returns a <see cref="Props"/> object that can be used to create an <see cref="EchoActor"/>.
        /// The  <see cref="EchoActor"/> echoes whatever is sent to it, to the
        /// TestKit's <see cref="TestKitBase.TestActor"/>.
        /// By default it also echoes back to the sender, unless the sender is the <see cref="TestKitBase.TestActor"/>
        /// (in this case the <see cref="TestKitBase.TestActor"/> will only receive one message) or unless 
        /// <paramref name="echoBackToSenderAsWell"/> has been set to <c>false</c>.
        /// </summary>
        public static Props Props(TestKitBase testkit, bool echoBackToSenderAsWell = true)
        {
            return Actor.Props.Create(()=>new EchoActor(testkit, echoBackToSenderAsWell));
        }
    }
}

