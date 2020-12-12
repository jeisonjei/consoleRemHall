using System;
using System.Collections.Generic;
using System.Linq;

namespace wasmSmokeMan.Shared.RemoveHall
{
    public class Room
    {
        public Room(double area, double height, List<Window> windows, List<DoorRoom> doors,double materialHeatCombustion, double materialMass, Climate climate)
        {
            Area = area;
            Height = height;
            Windows = windows;
            Doors = doors;
            MaterialHeatCombustion = materialHeatCombustion;
            MaterialMass = materialMass;
            Climate = climate;
        }
        //добавление по отдельности окон и дверей усложняет расчёты, поэтому, учитывая то, что в подавляющем большинстве случаев в помещении одна дверь и одно окно (иногда несколько), удобней использовать общую коллекцию для всех проёмов. Вероятно это будет основным конструктором класса
        public Room(double area, double height, List<Opening> openings, double materialHeatCombustion, double materialMass, Climate climate)
        {
            Area = area;
            Height = height;
            Openings = openings;
            MaterialHeatCombustion = materialHeatCombustion;
            MaterialMass = materialMass;
            Climate = climate;
        }
        public double Area { get; set; }
        public double Height { get; set; }

        public List<Opening> Openings { get; set; }
        //площадь проёмов суммарная
        public double OpeningAreaSum
        {
            get
            {
                return Openings.Sum(opening => opening.Area);
            }
        }
        public List<Window> Windows { get; set; }
        public List<DoorRoom> Doors { get; set; }
        //объём помещения
        public double Volume => Area * Height;
        //площадь поверностей помещения - формула 103
        public double Surface => 6 * Math.Pow(Volume, (double)2 / 3);
        
        
        //проёмность помещения - формула 106
        public double OpeningRoomVal
        {
            get
            {
                return (Openings.Sum(opening=>opening.Area*Math.Pow(opening.Height,0.5))) / (Math.Pow(Volume, (double)2 / 3));
            }
        }
        //удельная теплота сгорания материалов помещения. в расчёте материалы по отдельности не задаются, а используется значение из таблицы методических указаний для типовых помещений
        public double MaterialHeatCombustion { get; set; }
        //удельная теплота сгорания древесины
        public double WoodHeatCombustion { get; set; } = 13.8;
        //масса материалов помещения
        public double MaterialMass { get; set; }
        public Climate Climate { get; set; }
        //удельная приведённая пожарная нагрузка, отнесённая к площади пола помещения - формула 101
        public double g0
        {
            get
            {
                return (MaterialMass * MaterialHeatCombustion) / ((Area) * WoodHeatCombustion);
            }
        }
        //удельная приведённая пожрная нагрузка, отнесённая к площади поверхностей помещения - формула 102
        public double gk
        {
            get
            {
                return (MaterialMass * MaterialHeatCombustion) / ((Surface - OpeningAreaSum) * WoodHeatCombustion);
            }
        }
        //удельное критическое количество пожарной нагрузки - формула 105
        public double gcrit
        {
            get
            {
                return ((4500 * Math.Pow(OpeningRoomVal, 3)) / (1 + 500 * Math.Pow(OpeningRoomVal, 3))) + (Math.Pow(Volume, (double)1 / 3)) / (6 * FlowAirCompleteCombustion);
            }
        }
        //максимальная температура в помещении - формулы 13 и 14
        public double T0max
        {
            get
            {
                if (gk>gcrit)
                {
                    return Climate.TempInside.ToKelvin() + 940 * Math.Exp(0.0047 * g0 - 0.141);
                }
                else if (gk<=gcrit)
                {
                    return Climate.TempInside.ToKelvin() + 224 * Math.Pow(gk, 0.528);
                }
                return 0;

            }
        }
        //температура дыма, вытекающего в коридор из помещения 15
        public double T0
        {
            get
            {
                return 0.8 * T0max;
            }
        }
        //удельное количество воздуха, необходимое для полного сгорания пожарной нагрузки помещения - формула 107
        public double FlowAirCompleteCombustion
        {
            get
            {
                return 0.263 * (MaterialHeatCombustion);
            }
        }
    }
    

}
