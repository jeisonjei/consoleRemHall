using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using consoleRemHall.NaturalPhenomenaDependent;
using consoleRemHall.SemanticObjects;
using consoleRemHall.SimpleObjects;
using Main.NaturalPhenomenaIndependent;

namespace consoleRemHall.CompoundObjects
{
    public class Network : INetwork
    {
        public Hall Hall { get; set; }
        public Climate Climate { get; set; }
        public int First { get; set; }
        public int Last { get; set; }
        public int Qu { get; set; }

        public SortedList<int, SysPart> System { get; set; } = new SortedList<int, SysPart>();

        public double DpAdditional { get; set; }


        public Network(int first, int qu, Hall hall, Climate climate)
        {
            First = first;
            Qu = qu;
            if (first > 0)
            {
                Last = First + Qu - 1;
            }
            if (first < 0)
            {
                Last = First + Qu;
            }
            Hall = hall;
            Climate = climate;
        }
        

        public void AddRange((int first, int last) range, double floorHeight, (double width, double heigth) ductDims,
            double additionalDuctLength, double ksiSum)
        {
            if (range.last < range.first)
            {
                throw new ArithmeticException("Неправильно указан диапазон: этажи указываются в порядке возрастания");
            }

            if (range.first < First)
            {
                throw new ArithmeticException(
                    $"Первый этаж диапазона {range.first} не может быть ниже первого этажа здания {First}. Измените диапазон или измените первый этаж здания");
            }

            if (range.last > Last)
            {
                throw new ArithmeticException(
                    $"Последний этаж диапазона {range.last} не может быть выше последнего этажа здания {Last}. Измените диапазон или измените количество здания");
            }

            for (int i = range.first; i <= range.last; i++)
            {
                if (i == 0)
                {
                    continue;
                }
                else
                {
                    Floor floor = new Floor(i, floorHeight, 0);
                    Duct duct = new Duct(ductDims.width, ductDims.heigth);
                    NetPart netPart = new NetPart(duct, additionalDuctLength, ksiSum, floor, Climate);
                    SysPart sysPart = new SysPart(floor, duct, netPart);
                    System.Add(i, sysPart);
                }
            }
        }

        public void AddRange((int first, int last) range, double floorHeight, double ductDiam,
            double additionalDuctLength,
            double ksiSum)
        {
            throw new System.NotImplementedException();
        }

        public void AddSingle(int index, double floorHeight, (double width, double height) ductDims,
            double additionalLDuctLength,
            double ksiSum)
        {
            if (index == 0)
            {
                throw new ArithmeticException(
                    $"В списке этажей не может быть этажа с индексом {index}. Этажи могут иметь индексы больше нуля (1,2,3...) или меньше нуля (-1,-2,-3...)");
            }

            if (index < First)
            {
                throw new ArithmeticException(
                    $"Добавляемый этаж {index} не может быть ниже первого этажа здания {First}. Измените добавляемый этаж или измените первый этаж здания");
            }

            if (index > Last)
            {
                throw new ArithmeticException(
                    $"Добавляемый этаж {index} не может быть выше последнего этажа здания {Last}. Измените добавляемый этаж или измените количество этажей здания");
            }

            Floor floor = new Floor(index, floorHeight, 0);
            Duct duct = new Duct(ductDims.width, ductDims.height);
            NetPart netPart = new NetPart(duct, additionalLDuctLength, ksiSum, floor, Climate);
            SysPart sysPart = new SysPart(floor, duct, netPart);
            System.Add(floor.Index, sysPart);
        }

        public void AddSingle(int index, double floorHeight, double ductDiam, double additionalLDuctLength,
            double ksiSum)
        {
            throw new System.NotImplementedException();
        }
        public void RemoveRange()
        {
            throw new System.NotImplementedException();
        }

        public void RemoveSingle()
        {
            throw new System.NotImplementedException();
        }

        public void CompLevels()
        {
            
        }

        //этот метод используется только после того, как сеть построена
        public double CompSystem()
        {
            for (int i = System.First().Key; i <= System.Last().Key; i++)
            {
                //для первого участка сети
                if (i == System.First().Key)
                {
                    System[i].NetPart.TsmStart = Hall.Tsm;
                    System[i].NetPart.Fluid.Temp = Hall.Tsm;
                    System[i].NetPart.FlowStart = Hall.Gsm;
                    System[i].NetPart.PressureStart = 0;
                }
                //для последующих участков сети
                else
                {
                    System[i].NetPart.Fluid.Temp = System[i - 1].NetPart.TsmEnd;
                    System[i].NetPart.FlowStart = System[i - 1].NetPart.FlowEnd;
                    System[i].NetPart.PressureStart = System[i - 1].NetPart.PressureEnd;
                    System[i].NetPart.TsmStart = System[i - 1].NetPart.TsmEnd;
                }
            }

            return 0;
        }
    }
}