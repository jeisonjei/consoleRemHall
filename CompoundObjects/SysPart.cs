using consoleRemHall.SemanticObjects;

namespace wasmSmokeMan.Shared.RemoveHall
{
    public class SysPart
    {
        public SysPart()
        {
            
        }
        public SysPart(Floor floor, Duct duct, NetPart netPart)
        {
            Floor = floor;
            Duct = duct;
            NetPart = netPart;
        }

        public Floor Floor { get; set; }
        public Duct Duct { get; set; }
        public NetPart NetPart { get; set; }
    }
}