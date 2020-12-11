using System;

namespace wasmSmokeMan.Shared.RemoveHall
{
    public class Fluid
    {
        public Fluid()
        {
            
        }
        //название fluid, но подразумевается воздух или дым
        public Fluid(double tempCels)
        {
            TempCels = tempCels;
        }
        //температура воздуха
        public double TempCels { get; set; }
        //плотность
        public double Density
        {
            get
            {
                return 353 / TempCels.ToKelvin();
            }
        }
        //вязкость воздуха динамическая
        public double Mu
        {
            get
            {
                return 9.80665 * (1.745 * Math.Pow(10, -6) + 5.03 * Math.Pow(10, -9) * TempCels);
            }
        }
        //вязкость воздуха кинематическая
        public double Nu
        {
            get
            {
                return Mu / Density;
            }
        }

    }
}
