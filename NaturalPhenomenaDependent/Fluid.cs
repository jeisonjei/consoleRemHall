using consoleRemHall.Support;
using System;
using System.Collections.Generic;
using System.Text;

namespace consoleRemHall.NaturalPhenomenaDependent
{
    public class Fluid
    {
        //название fluid, но подразумевается воздух или дым
        public Fluid(double temp)
        {
            Temp = temp;
        }
        //температура воздуха
        public double Temp { get; set; }
        //плотность
        public double Density
        {
            get
            {
                return 353 / Temp.ToKelvin();
            }
        }
        //вязкость воздуха динамическая
        public double Mu
        {
            get
            {
                return 9.80665 * (1.745 * Math.Pow(10, -6) + 5.03 * Math.Pow(10, -9) * Temp);
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
