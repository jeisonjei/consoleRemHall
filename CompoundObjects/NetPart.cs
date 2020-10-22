using System;
using consoleRemHall.NaturalPhenomenaDependent;
using consoleRemHall.SemanticObjects;
using consoleRemHall.SimpleObjects;
using consoleRemHall.Support;
using Main.NaturalPhenomenaIndependent;
using Type = consoleRemHall.SimpleObjects.Type;

namespace consoleRemHall.CompoundObjects
{
    //назначение этого класса - упростить построение сети. Данные о дополнительной длине и КМС можно было бы добавить в класс Duct, но по смыслу эти данные относятся не к участку воздуховода, а к участку сети, так что этот класс описывает как раз этот участок сети. При этом высота этажа, которая используется для расчёта потери давления dP, передаётся как аргумент метода 
    public class NetPart
    {
        //так как этот класс предполагается использовать совместно с данными об этаже, то можно определить, что этот класс является как-бы смысловым блоком, существующим в контексте данных об уровнях здания, то есть является зависимым от данных другого класса. Это к тому, что была идея использовать свойств FloorHeight, но так как это значение всё равно зависит от других данных, разумнее использовать floorHeight как параметр метода  
        public Duct Duct { get; set; }
        public double AdditionalDuctLength { get; set; }
        public double KsiSum { get; set; }

        public Floor Floor { get; set; }

        public Climate Climate { get; set; }

        //расчётные свойства из класса Duct
        public double dpDuct
        {
            get
            {
                if (Duct.Type==Type.Round)
                {
                    return Lambda * (Fluid.Density * Math.Pow(Velocity, 2)) / (2 * Duct.Diameter.ToMeters());
                }

                if (Duct.Type==Type.Rectangular)
                {
                    return Lambda * (Fluid.Density * Math.Pow(Velocity, 2)) / (2 * Duct.HidraulicDiameter.ToMeters());
                    
                }
                throw new ArithmeticException("Нельзя вычислить dP, так как не задан тип воздуховода - круглый или прямоугольный");
            }
        }
        public double FlowStart { get; set; }
        public Fluid Fluid { get; set; }=new Fluid(20);
        public double Lambda
        {
            get
            {
                if (Duct.Type==Type.Round)
                {
                    return 0.11 * Math.Pow((0.1 / Duct.Diameter + 0.68 / Re), 0.25);
                }

                if (Duct.Type==Type.Rectangular)
                {
                    return 0.11 * Math.Pow((0.1 / Duct.HidraulicDiameter + 0.68 / Re), 0.25);
                }
                throw new ArithmeticException("Нельзя вычислить Лямбда, так как не задан тип воздуховода");
            }
        }
        public double Re
        {
            get
            {
                if (Duct.Type==Type.Round)
                {
                    return (Velocity*Duct.Diameter.ToMeters()) / Fluid.Nu;
                }

                if (Duct.Type==Type.Rectangular)
                {
                    return (Velocity*Duct.HidraulicDiameter.ToMeters()) / Fluid.Nu;
                }

                throw  new ArithmeticException("Нельзя вычислить число Ренольдца, так как не задан тип воздуховода");
            }
        }
        public double Velocity
        {
            get
            {
                return FlowStart / (Duct.Area * Fluid.Density);
            }
        }
        //потеря давления на участке. включает в себя потери в прямом участке воздуховода, потери в местных сопротивлениях потери по длине в вертикальном воздуховоде и потери по длине в дополнительном участке при его наличии
        public double dpNetPart
        {
            get
            {
                //здаесь опущено гравитационное давление
                return Floor.Height * dpDuct + AdditionalDuctLength * dpDuct +
                       ((KsiSum * Fluid.Density * Math.Pow(Velocity, 2)) / 2);
            }
        }
        //давление в начале участка. при наличии естественной компенсации для первого участка сети эта величина будет равна потерям давления в сети естественной компенсации
        public double PressureStart { get; set; }
        //давление в конце участка. равняется сумме давления в начале участка и потери давлений на участке
        public double PressureEnd
        {
            get { return PressureStart + dpNetPart; }
        }

        //подсосы воздуха по длине участка
        public double Gda
        {
            get
            {
                //приложение 3, пункт 1
                return 3.556 * Math.Pow(10, -5) * Fluid.Density *(Math.Pow((PressureStart + PressureEnd) / 2, 0.65)) * (Duct.Area / Duct.HidraulicDiameter.ToMeters()) *
                       (Floor.Height + AdditionalDuctLength);
            }
            
        }

        public double TsmStart { get; set; }

        public double TsmEnd
        {
            get
            {
                //здесь опущены: коэффициент c и охлаждение воздуха по длине воздуховода, по крайней мере пока
                return (FlowStart * TsmStart + Gda * Climate.TempInside) / FlowEnd;
            }
        }

        //расход воздуха в конце участка
        public double FlowEnd
        {
            get
            {
                return FlowStart + Gda;
            }
        }

        

        //пока решено не усложнять слишком программу и обойтись без функции добавления по-отдельности местных сопротивлений. Посчитать сумму КМС на этаже нетрудно.
        public NetPart(Duct duct, /*воздуховод*/
            double additionalDuctLength, /*добавочная длина, если воздуховод не проходит на этаже прямо снизу вверх*/
            double ksiSum /*сумма КМС на участке*/, Floor floor, Climate climate)
        {
            Duct = duct;
            AdditionalDuctLength = additionalDuctLength;
            KsiSum = ksiSum;
            Floor = floor;
            Climate = climate;
        }
    }
}