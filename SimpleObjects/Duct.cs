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

        public Duct(double width, double height, double flow, Fluid fluid)
        {
            Width = width;
            Height = height;
            Flow = flow;
            Fluid = fluid;
        }
        public Duct(double diameter, double flow,Fluid fluid)
        {
            Diameter = diameter;
            Flow = flow;
            Fluid = fluid;
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
        public double Flow { get; set; }
        public Fluid Fluid { get; set; }

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

        public double Velocity
        {
            get
            {
                return Flow / (Area * Fluid.Density);
            }
        }
        public double Re
        {
            get
            {
                if (Type==Type.Round)
                {
                    return (Velocity*Diameter.ToMeters()) / Fluid.Nu;
                }

                if (Type==Type.Rectangular)
                {
                    return (Velocity*HidraulicDiameter.ToMeters()) / Fluid.Nu;
                }

                throw  new ArithmeticException("Нельзя вычислить число Ренольдца, так как не задан тип воздуховода");
            }
        }

        public double Lambda
        {
            get
            {
                if (Type==Type.Round)
                {
                    return 0.11 * Math.Pow((0.1 / Diameter + 0.68 / Re), 0.25);
                }

                if (Type==Type.Rectangular)
                {
                    return 0.11 * Math.Pow((0.1 / HidraulicDiameter + 0.68 / Re), 0.25);
                }
                throw new ArithmeticException("Нельзя вычислить Лямбда, так как не задан тип воздуховода");
            }
        }

        public double dP
        {
            get
            {
                if (Type==Type.Round)
                {
                    return Lambda * (Fluid.Density * Math.Pow(Velocity, 2)) / (2 * Diameter.ToMeters());
                }

                if (Type==Type.Rectangular)
                {
                    return Lambda * (Fluid.Density * Math.Pow(Velocity, 2)) / (2 * HidraulicDiameter.ToMeters());
                    
                }
                throw new ArithmeticException("Нельзя вычислить dP, так как не задан тип воздуховода - круглый или прямоугольный");
            }
        }
    }
    public enum Type
    {
        Round,
        Rectangular
    }
}
