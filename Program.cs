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
            Console.WriteLine("Hello!");
            Climate climate = new Climate(26, 20, 2);
            Opening opening1=new Opening(1,1);
            Opening opening2=new Opening(1,2);
            List<Opening> openings=new List<Opening>();
            openings.Add(opening1);
            openings.Add(opening2);
            Room room=new Room(25,3,openings,13.8,500,climate);
            DoorRoom doorRoom=new DoorRoom(1.1,2.1);
            DoorHall doorHall=new DoorHall(1.1,2.1,DoorHall.Type.SmokeResistant,climate);
            Hall hall=new Hall(40,15,4.6,doorHall,room,climate,BuildingType.Residential);
            Network network=new Network(-1,10,-4.5,climate,hall);
            network.AddSingle(-1, 4.5, (800, 600), 4, 5);
            network.AddRange((1,10), 3, (800, 600), 1, 1, (800, 600));
            network.AddSingle(10, 3, (800, 600), 1, 1, (800, 600));
            network.CompLevels();
            network.CompSystem();
            foreach (var item in network.System)
            {
                Console.WriteLine($"level : {item.Value.Floor.Level}");
            }
            
        }
    }
}
