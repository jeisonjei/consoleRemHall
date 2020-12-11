namespace wasmSmokeMan.Shared.RemoveHall
{
    public class DoorRoom
    {
        public DoorRoom()
        {
            
        }
        private double area;

        public DoorRoom(double width, double height)
        {
            Width = width;
            Height = height;
        }

        public double Width { get; set; }
        public double Height { get; set; }
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

    }
}