using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using consoleRemHall.SemanticObjects;
using wasmSmokeMan.Shared.RemoveHall;


namespace consoleRemHall
{
    static class Program
    {
        static void Main(string[] args)
        {
            Climate climate = new Climate(26, 26, 2);
            List<Opening> openings = new List<Opening>() { new Opening(1,2.1),new Opening(1.8,1) };
            Room room = new Room(25, 2.8, openings, 14, 200, climate);
            DoorHall doorHall = new DoorHall(1.1, 2.1,DoorHall.Type.SmokeResistant,climate);
            Hall hall = new Hall(30, 15, 2.8, doorHall, room, climate,BuildingType.Residential);
            
            
            Network network=new Network(1,5,hall,climate);
            network.AddSingle(1,4,(600,400),10,4);
            network.AddRange((2,4),3.2,(600,400),0,0.5,(800,600));
            network.AddSingle(5,4,(800,600),20,4);
            foreach (KeyValuePair<int,SysPart> part in network.System)
            {
                Console.WriteLine($"{part.Key,-5}{"LEVEL",-10}|{part.Value.Floor.Level.RoundTo2()}");
                Console.WriteLine($"{part.Key,-5}{"Gdpa",-10}|{part.Value.NetPart.Gdpa.RoundTo4()}");
            }

            Console.WriteLine($"-----");
            Console.WriteLine($"{"Lv Fan",-10}|{network.System.Last().Value.NetPart.FlowEnd.ToCubicMetersPerHour(network.System.Last().Value.NetPart.Fluid.Density).RoundTo0()}");
            Console.WriteLine($"{"Pv Fan",-10}|{network.System.Last().Value.NetPart.PressureEnd.RoundTo1()}");

        }
    }
}
