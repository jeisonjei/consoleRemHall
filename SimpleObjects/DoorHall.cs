namespace wasmSmokeMan.Shared.RemoveHall
{
    public class DoorHall
    {
        public DoorHall()
        {
            
        }
        private Type doorType;
        private double area;
        private double smokeResistance;

        public DoorHall(double width, double height, Type type, Climate climate)
        {
            Width = width;
            Height = height;
            DoorType = type;
            Climate = climate;
            Area = width * height;
        }
        public DoorHall(double width, double height, double smokeResistance)
        {
            Width = width;
            Height = height;
            SmokeResistance = smokeResistance;
            Area = width * height;
        }

        public double Width { get; set; }
        public double Height { get; set; }
        public Type Type1 { get; }
        public Climate Climate { get; set; }
        public double Area
        {
            get
            {
                area = Width * Height;
                return area;
            }

            set
            {
                area = value;
            }
        }
        public double SmokeResistance
        {
            get
            {
                if (DoorType == Type.Usual)
                {
                    smokeResistance = 5300 / Climate.DensitySupply;
                }
                else if (DoorType == Type.SmokeResistant)
                {
                    smokeResistance = 60000 / Climate.DensitySupply;
                }
                
                return smokeResistance;
            }

            set
            {
                smokeResistance = value;
            }
        }

        public Type DoorType { get; set; }

        public enum Type
        {
            Usual,
            SmokeResistant,
            Manual
        }

    }

}
