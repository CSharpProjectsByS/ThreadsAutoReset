using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThreadAutoReset
{
    class Program
    {
        static void Main(string[] args)
        {
            Simulation sim = new Simulation();
            sim.Start();
            //Console.ReadKey();
        }
    }
}
