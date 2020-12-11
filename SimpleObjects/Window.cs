namespace wasmSmokeMan.Shared.RemoveHall
{
    public class Window
    {
        public Window()
        {
            
        }
        private double area;

        public Window(double width, double height)
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