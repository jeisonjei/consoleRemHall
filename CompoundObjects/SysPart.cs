using consoleRemHall.NaturalPhenomenaDependent;
using consoleRemHall.SemanticObjects;
using consoleRemHall.SimpleObjects;

namespace consoleRemHall.CompoundObjects
{
    public class SysPart
    {
        public SysPart(Floor floor, Fluid fluid, Duct duct, NetPart netPart)
        {
            Floor = floor;
            Fluid = fluid;
            Duct = duct;
            NetPart = netPart;
        }

        public Floor Floor { get; set; }
        public Fluid Fluid { get; set; }
        public Duct Duct { get; set; }
        public NetPart NetPart { get; set; }
    }
}