using System;

namespace wasmSmokeMan.Shared.RemoveHall
{
    public class Hall
    {
        public Hall(double area, double length, double height, DoorHall door, Room room, Climate climate,BuildingType buildingType)
        {
            Area = area;
            Length = length;
            Height = height;
            Door = door;
            Room = room;
            Climate = climate;
            BuildingType = buildingType;
        }

        public Hall()
        {
            
        }
        public double Area { get; set; }
        public double Length { get; set; }
        public double Height { get; set; }
        public DoorHall Door { get; set; }
        public Room Room { get; set; }
        public Climate Climate { get; set; }
        public BuildingType BuildingType { get; set; }
        public double Tsm
        {
            get
            {
                var hsm = 0.55 * Height;
                return Climate.TempInside.ToKelvin() + (1.22 * (Room.T0 - Climate.TempInside.ToKelvin()) * (2 * hsm + (Area / Length))) / Length * (1 - Math.Exp((-0.58 * Length) / (2 * hsm + (Area / Length))));
            }
        }
        public double Gsm
        {
            get
            {
                return Ksm * (Door.Area * (Math.Pow(Door.Height, 0.5)));
            }
        }
        public double Ksm
        {
            get
            {
                if (BuildingType == BuildingType.Residential)
                {
                    return 1;
                }
                else if (BuildingType == BuildingType.Public)
                {
                    return 1.2;
                }
                else
                {
                    throw new ArithmeticException("Тип здания не назначен, поэтому невозможно определить коэффициент Ksm (для жилых зданий Ksm=1, для общественных Ksm=1.2)");
                }

            }
        }
    }
    public enum BuildingType
    {
        Residential,
        Public
    }
}
    
