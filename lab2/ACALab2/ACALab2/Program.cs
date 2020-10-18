using System;
using System.IO;

namespace ACALab2
{
    internal static class Program
    {
        
        private static void Main()
        {
            FunctionalCheck.CheckInfixConvert();
            Console.ReadLine();
            ThirdPart.DoWork(10, false);
            Console.ReadLine();
            FunctionalCheck.CheckBasicFunctional();
            FunctionalCheck.CheckInputQueries();
            Console.ReadLine();
        }
    }
}