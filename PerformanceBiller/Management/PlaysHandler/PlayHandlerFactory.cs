using System;

namespace PerformanceBiller.Management.PlaysHandler
{
    public class PlayHandlerFactory
    {
        public PlayHandler Create(Play play)
        {
            switch (play.Type)
            {
                case "tragedy":
                    return new TragedyHandler();
                case "comedy":
                    return new ComedyHandler();
                default:
                    throw new NotImplementedException();
            }
        }
    }
}
