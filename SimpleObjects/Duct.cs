using consoleRemHall.NaturalPhenomenaDependent;
using consoleRemHall.Support;
using System;
using System.Collections.Generic;
using System.Text;

namespace consoleRemHall.SimpleObjects
{
    public class Duct
    {
        private double width;
        private double height;
        private double diameter;

        public Duct(double width, double height)
        {
            Width = width;
            Height = height;
        }
        public Duct(double diameter)
        {
            Diameter = diameter;
        }

        public double Width
        {
            get => width; set
            {
                Type = Type.Rectangular;
                diameter = 0;
                width = value;
            }
        }
        public double Height
        {
            get => height; set
            {
                Type = Type.Rectangular;
                diameter = 0;
                height = value;
            }
        }

        public double HidraulicDiameter
        {
            get
            {
                return (2 * Width * Height) / (Width + Height);
            }
        }

        public Type Type { get; set; }
        
        

        public double Diameter
        {
            get => diameter; set
            {
                Type = Type.Round;
                width = 0;
                height = 0;
                diameter = value;
            }
        }
        public double Area
        {
            get
            {
                if (Type == Type.Round)
                {
                    return Math.PI * Math.Pow(Diameter.ToMeters() / 2, 2);
                }
                else if (Type == Type.Rectangular)
                {
                    return Width.ToMeters() * Height.ToMeters();
                }
                else
                {
                    throw new ArithmeticException(
                        $"Размеры воздуховода вероятно не заданы. Поиск выполнялся в следующих значениях: Диаметр={Diameter}, Ширина={Width}, Высота={Height}");
                }
            }
        }

        
        

        

        
    }
    public enum Type
    {
        Round,
        Rectangular
    }
}
