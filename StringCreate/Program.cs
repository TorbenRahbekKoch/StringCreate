using System;
using BenchmarkDotNet.Running;

namespace StringCreate
{
    class Program
    {
        static void Main(string[] args)
        {
            //Console.WriteLine(new Replace().StringCreateForEach());
            BenchmarkRunner.Run(typeof(Program).Assembly);
        }
    }
}
