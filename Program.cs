using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using wasmSmokeMan.Shared.RemoveHall;


namespace consoleRemHall
{
    static class Program
    {
        static void Main(string[] args)
        {
            Floors floors=new Floors(-3,4,0);
            
            foreach (var item in floors.Levels)
            {
                Console.WriteLine($"{item.Key} : {item.Value.Height}");
            }
            Console.WriteLine("-----");
            Console.WriteLine(floors.Levels.Count);
            
        }
    }
}
