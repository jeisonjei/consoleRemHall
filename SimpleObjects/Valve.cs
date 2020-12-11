namespace wasmSmokeMan.Shared.RemoveHall
{
    public class Valve
    {
        public Valve()
        {
            
        }
        public Valve(double width, double height,double sdpsm)
        {
            Width = width;
            Height = height;
            Sdpsm = sdpsm;
        }
        public double Width { get; set; }
        public double Height { get; set; }
        

        public double Area
        {
            get
            {
                //Fжс клапана рассчитывается по формуле 44 из АВОК. если сравнивать с расчётом квм-дым, то там принимается гораздо меньшая площадь живого сечения. поэтому результаты здесь получаются больше. если принимать площадь Fжс как в программе квм-дым, то результаты почти одинаковые
                return (Width.ToMeters()-0.03) * (Height.ToMeters()-0.05);
                // return Width.ToMeters() * Height.ToMeters();
            }
           
        }
        public double Sdpsm { get; set; }
        
    }
}