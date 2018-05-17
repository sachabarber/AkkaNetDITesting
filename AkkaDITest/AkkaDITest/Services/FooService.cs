using System;

namespace AkkaDITest.Services
{


    public interface IFooService
    {
        int ReturnValue(int input);
    }

    public class FooService : IFooService
    {
        Random _rand = new Random();

        public int ReturnValue(int input)
        {
            return input * _rand.Next(0, 500);
        }
    }

}
