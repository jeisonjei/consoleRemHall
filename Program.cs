using consoleRemHall.CompoundObjects;
using consoleRemHall.NaturalPhenomenaDependent;
using consoleRemHall.SimpleObjects;
using consoleRemHall.Support;
using Main.NaturalPhenomenaIndependent;
using Main.SimpleObjects;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using consoleRemHall.SemanticObjects;

namespace consoleRemHall
{
    static class Program
    {
        static void Main(string[] args)
        {
            //Climate climate = new Climate(26, 26, 2);
            //List<Opening> openings = new List<Opening>() { new Opening(1,2.1),new Opening(1.8,1) };
            //Room room = new Room(25, 2.8, openings, 14, 200, climate);
            //DoorHall doorHall = new DoorHall(1.1, 2.1,DoorHall.Type.SmokeResistant,climate);
            //Hall hall = new Hall(30, 15, 2.8, doorHall, room, climate,BuildingType.Residential);
            //Console.WriteLine($"{"ROOM VOLUME",-20} : {room.Volume.RoundTo1(),5}");
            //Console.WriteLine($"{"ROOM SURFACE",-20} : {room.Surface.RoundTo1(),5}");
            //Console.WriteLine($"{"OPENINGS AREA",-20} : {room.OpeningArea.RoundTo2(),5}");
            //Console.WriteLine($"{"g0",-20} : {room.g0.RoundTo3(),5}");
            //Console.WriteLine($"{"gk",-20} : {room.gk.RoundTo3(),5}");
            //Console.WriteLine($"{"gcrit",-20} : {room.gcrit.RoundTo3(),5}");
            //Console.WriteLine($"{"OPENING ROOM VALUE",-20} : {room.OpeningRoomVal.RoundTo3(),5}");
            //Console.WriteLine($"{"AIR FLOW COMBUSTION",-20} : {room.FlowAirCompleteCombustion.RoundTo3(),5}");
            //Console.WriteLine($"{"T0max",-20} : {(room.T0max.ToCelsius().RoundTo0()),5}°C");
            //Console.WriteLine($"{"T0",-20} : {(room.T0.ToCelsius().RoundTo0()),5}°C");
            //Console.WriteLine($"{"Ksm",-20} : {hall.Ksm}");
            //Console.WriteLine($"{"Tsm",-20} : {hall.Tsm.ToCelsius().RoundTo0()}°C");
            //Console.WriteLine($"{"Gsm",-20} : {hall.Gsm.RoundTo4()}");
            
            
            // Fluid fluid = new Fluid(20);
            // Duct duct = new Duct(550, 550, 4.5, fluid);
            // Console.WriteLine($"{"TYPE",-15}|{duct.Type}");
            // Console.WriteLine($"{"WIDTH",-15}|{duct.Width.RoundTo0()}");
            // Console.WriteLine($"{"HEIGHT",-15}|{duct.Height.RoundTo0()}");
            // Console.WriteLine($"{"DIAMETER",-15}|{duct.Diameter.RoundTo0()}");
            // Console.WriteLine($"{"AREA", -15}|{duct.Area.RoundTo3()}");
            // Console.WriteLine($"{"VELOCITY",-15}|{duct.Velocity.RoundTo2()}");
            // Console.WriteLine($"{"VISC",-15}|{duct.Fluid.Nu}");
            // Console.WriteLine($"{"RE",-15}|{duct.Re.RoundTo0()}");
            // Console.WriteLine($"{"HIDRAULIC D",-15}|{duct.HidraulicDiameter.RoundTo1()}");
            // Console.WriteLine($"{"LAMBDA",-15}|{duct.Lambda}");
            // Console.WriteLine($"{"DP",-15}|{duct.dP}");
            // duct.Diameter = 450;
            // Console.WriteLine("--------------------------------------------");
            // Console.WriteLine($"{"TYPE",-15}|{duct.Type}");
            // Console.WriteLine($"{"WIDTH",-15}|{duct.Width.RoundTo0()}");
            // Console.WriteLine($"{"HEIGHT",-15}|{duct.Height.RoundTo0()}");
            // Console.WriteLine($"{"DIAMETER",-15}|{duct.Diameter.RoundTo0()}");
            // Console.WriteLine($"{"AREA",-15}|{duct.Area.RoundTo3()}");
            // Console.WriteLine($"{"VELOCITY",-15}|{duct.Velocity.RoundTo2()}");
            // Console.WriteLine($"{"VISC",-15}|{duct.Fluid.Nu}");
            // Console.WriteLine($"{"RE",-15}|{duct.Re.RoundTo0()}");
            // Console.WriteLine($"{"HIDRAULIC D",-15}|{duct.HidraulicDiameter.RoundTo1()}");
            // Console.WriteLine($"{"LAMBDA",-15}|{duct.Lambda}");
            
            SortedList<int,SysPart> system=new SortedList<int, SysPart>();
            Fluid fluid1=new Fluid(100);
            Fluid fluid2=new Fluid(80);
            Fluid fluid3=new Fluid(60);
            Floor floor1=new Floor(1,4.5,0);
            Floor floor2=new Floor(2,3,4.5);
            Floor floor3 = new Floor(3, 3, 7.5);
            Duct duct1=new Duct(550,550,4.5,fluid1);
            Duct duct2=new Duct(400,550,4.5,fluid2);
            Duct duct3=new Duct(600,550,4.5,fluid3);
            NetPart netPart1=new NetPart(duct1,0,0.5);
            NetPart netPart2=new NetPart(duct2,5,1);
            NetPart netPart3=new NetPart(duct3,10,2);
            SysPart sysPart1=new SysPart(floor1,fluid1,duct1,netPart1);
            SysPart sysPart2=new SysPart(floor2,fluid2,duct2,netPart2);
            SysPart sysPart3=new SysPart(floor3,fluid3,duct3,netPart3);
            system.Add(1,sysPart1);
            system.Add(2,sysPart2);
            system.Add(3,sysPart3);
            var sum = system.Sum(x => x.Value.NetPart.GetDP(x.Value.Floor.Height));
            Console.WriteLine($"{"SUM DP",-25}|{sum}");
        }
    }
}
