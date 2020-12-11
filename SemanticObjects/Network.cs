using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using System.ComponentModel;
namespace wasmSmokeMan.Shared.RemoveHall
{
    public class Network : INetwork
    {


        public Network Clone()
        {
            Network clone = new Network();
            var json = JsonConvert.SerializeObject(this);
            clone = JsonConvert.DeserializeObject<Network>(json);
            clone.System = new SortedList<int, SysPart>(this.System);

            return clone;
        }

        private int last;
        private event DoComputations LevelsFilled;
        public Hall Hall { get; set; }
        public Climate Climate { get; set; }
        public int First { get; set; }

        public int Last
        {
            get
            {
                if (First > 0)
                {
                    last = First + Qu - 1;
                }
                if (First < 0)
                {
                    last = First + Qu;
                }
                return last;
            }
            set { last = value; }
        }

        public int Qu { get; set; }

        public SortedList<int, SysPart> System { get; set; } = new SortedList<int, SysPart>();

        public double DpAdditional { get; set; }
        //параметр с таким же именем есть и в классе Valve, здесь этот параметр нужен для того, чтобы указывать Sdpsm для всех клапанов разом
        public double Sdpsm { get; set; } = 11000;/*дымогазопроницаемость клапана. в то время как размеры поэтажных клапанов задаются можно задавать для каждого этажа, дымогазопроницаемость назначается одна для всех клапанов. существует очень малая вероятность того, что понадобится выполнить расчёт для системы с клапанами различной дымогазопроницаемости на этажах*/
        public double Lv { get; set; }
        public double Psv { get; set; }

        public Network()
        {

        }
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
            LevelsFilled += CompLevels;
            LevelsFilled += CompSystem;
        }

        protected void AddToSystem(int index, SysPart sysPart)
        {
            try
            {
                System.Add(index, sysPart);
                if (System.Count == Qu)
                {
                    if (LevelsFilled != null) LevelsFilled();
                }
            }
            catch (ArgumentException ex) when (ex.HResult.ToString()== "-2147024809")
            {
                throw new ArgumentException($"Элемент сети с индексом этажа \"{index}\" уже существует");

            }
        }


        public void AddRange((int first, int last) range, double floorHeight, (double width, double heigth) ductDims,
            double additionalDuctLength, double ksiSum, (double width, double height) valveDims)
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
                        Valve valve = new Valve(valveDims.width, valveDims.height, Sdpsm);
                        NetPart netPart = new NetPart(duct, additionalDuctLength, ksiSum, floor, Climate, valve);
                        SysPart sysPart = new SysPart(floor, duct, netPart);

                        AddToSystem(i, sysPart);

                }
            }
        }
        //версия метода AddRange без этажного клапана
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
                    AddToSystem(i, sysPart);
                }
            }
        }

        public void AddRange((int first, int last) range, double floorHeight, double ductDiam, double additionalDuctLength,
            double ksiSum, (double width, double height) valveDims)
        {
            throw new NotImplementedException();
        }


        public void AddSingle(int index, double floorHeight, (double width, double height) ductDims,
            double additionalLDuctLength,
            double ksiSum, (double width, double height) valveDims)
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
            Valve valve = new Valve(valveDims.width, valveDims.height, Sdpsm);
            NetPart netPart = new NetPart(duct, additionalLDuctLength, ksiSum, floor, Climate, valve);
            SysPart sysPart = new SysPart(floor, duct, netPart);
            AddToSystem(index, sysPart);
        }
        //версия метода AddSingle без этажного клапана
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
            AddToSystem(index, sysPart);
        }

        public void AddSingle(int index, double floorHeight, double ductDiam, double additionalLDuctLength, double ksiSum,
            (double width, double height) valveDims)
        {
            throw new NotImplementedException();
        }


        public void RemoveRange((int first, int last) range)
        {
            for (int i = range.first; i <= range.last; i++)
            {
                System.Remove(i);
            }

            foreach (var part in System)
            {
                part.Value.Floor.Level = 0;
            }
            //так как участок сети удаляется, вычисленные расход и давление становятся неактуальны, сбросим их значение
            Lv = 0;
            Psv = 0;
        }

        public void RemoveSingle(int index)
        {
            System.Remove(index);
            foreach (var part in System)
            {
                part.Value.Floor.Level = 0;
            }
            //так как участок сети удаляется, вычисленные расход и давление становятся неактуальны, сбросим их значение
            Lv = 0;
            Psv = 0;
        }

        public void CompLevels()
        {
            //общая высота всех этажей
            BuildingHeightOverall = System.Sum(lev => lev.Value.Floor.Height);
            //высота подземных этажей (этажа с индексом 0 быть не может - в методе AddRange() в этом случае добавления не происходит
            BuildingHeightBelowZero = System.Where(lev => lev.Key < 0).Sum(lev => lev.Value.Floor.Height);
            //высота надземных этажей
            BuildingHeightAboveZero = System.Where(lev => lev.Key > 0).Sum(lev => lev.Value.Floor.Height);
            //пока подразумевается, что в здании есть надземные этажи, для случай, когда все этажи подземные, пока не обрабатывается
            FirstFloorLevel = BuildingHeightAboveZero - BuildingHeightOverall;
            LastFloorLevel = BuildingHeightAboveZero - System.Last().Value.Floor.Height;
            RoofFloorLevel = BuildingHeightAboveZero;
            double _ = FirstFloorLevel;
            for (int i = System.First().Key; i <= System.Last().Key; i++)
            {
                if (i == 0)
                {
                    continue;
                }

                System[i].Floor.Level = _;
                _ += System[i].Floor.Height;
            }
        }

        public double RoofFloorLevel { get; set; }

        public double LastFloorLevel { get; set; }

        public double FirstFloorLevel { get; set; }

        public double BuildingHeightAboveZero { get; set; }

        public double BuildingHeightBelowZero { get; set; }

        public double BuildingHeightOverall { get; set; }

        //этот метод используется только после того, как сеть построена
        public void CompSystem()
        {
            for (int i = System.First().Key; i <= System.Last().Key; i++)
            {
                //для первого участка сети
                if (i == System.First().Key)
                {
                    System[i].NetPart.TsmStart = Hall.Tsm;/*приравниваем температуру дыма в начале участка к температуре дыма в коридоре*/
                    System[i].NetPart.Fluid.TempCels = Hall.Tsm.ToCelsius();
                    System[i].NetPart.FlowStart = Hall.Gsm;
                    System[i].NetPart.PressureStart = 0;
                }
                //для последующих участков сети
                else
                {
                    System[i].NetPart.Fluid.TempCels/*текущий участок*/ = System[i - 1].NetPart.TsmEnd.ToCelsius()/*конец предыдущий участок*/;
                    System[i].NetPart.FlowStart/*начало текущего участка*/ = System[i - 1].NetPart.FlowEnd/*конец предыдущего участок*/;
                    System[i].NetPart.PressureStart/*начало текущего участка*/ = System[i - 1].NetPart.PressureEnd/*конец предыдущего участка*/;
                    System[i].NetPart.TsmStart/*начало текущего участка*/ = System[i - 1].NetPart.TsmEnd/*конец предыдущего участка*/;
                }
            }

            var lastPart = System.Last().Value.NetPart;
            //так как результаты получаются меньше, чем в программе квм-дым (хотя и незначительно), временно добавим коэффициент. до тех пор, пока расчётная часть не будет доработана
            Lv = lastPart.FlowEnd.ToCubicMetersPerHour(lastPart.Fluid.Density);
            Psv = 1.2 * (lastPart.PressureEnd / lastPart.Fluid.Density);/*давление, приведённое к стандартным условиям. дополнительные потери Pd не добавляются, потому что есть возможность учесть эти потери при добавлении последнего участка сети*/

        }
    }

    internal delegate void DoComputations();
}