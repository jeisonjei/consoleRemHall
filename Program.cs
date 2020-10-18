using consoleRemHall.CompoundObjects;
using consoleRemHall.NaturalPhenomenaDependent;
using consoleRemHall.SimpleObjects;
using consoleRemHall.Support;
using Main.NaturalPhenomenaIndependent;
using Main.SimpleObjects;
using System;
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
            
            
            Fluid fluid=new Fluid(20);
            Floor floor=new Floor(1,4.5,0);
            Duct duct=new Duct(550,550,4.5,fluid);
            NetPart netPart=new NetPart(floor,duct,10,2);
            Console.WriteLine($"{"DP",-25}|{netPart.DP}");
        }
    }
}
