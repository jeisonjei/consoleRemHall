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
            Network network=new Network(-2,25,-8,climate,hall);
            
        }
    }
}
