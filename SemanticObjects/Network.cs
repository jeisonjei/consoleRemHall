using System;
using System.Collections.Generic;
using System.Linq;
namespace wasmSmokeMan.Shared.RemoveHall
{
    public class Network
    {
        public Network() { }
        public Network(int firstFloorIndex, int lastFloorIndex, double firstFloorLevel, Climate climate, Hall hall)
        {
            FirstFloorIndex = firstFloorIndex;
            LastFloorIndex = lastFloorIndex;
            FirstFloorLevel = firstFloorLevel;
            Climate = climate;
            Hall = hall;
            System = new SortedListFixed<int, SysPart>(Helpers.Range((firstFloorIndex, lastFloorIndex)));
        }

        public SortedListFixed<int, SysPart> System { get; set; }
        public int FirstFloorIndex { get; set; }
        public int LastFloorIndex { get; set; }
        public double FirstFloorLevel { get; set; }
        public int Qu
        {
            get
            {
                return Helpers.Range((FirstFloorIndex, LastFloorIndex));
            }
        }
        public Climate Climate { get; }
        public Hall Hall { get; }

        //параметр с таким же именем есть и в классе Valve, здесь этот параметр нужен для того, чтобы указывать Sdpsm для всех клапанов разом
        public double Sdpsm { get; set; } = 11000;/*дымогазопроницаемость клапана. в то время как размеры поэтажных клапанов задаются можно задавать для каждого этажа, дымогазопроницаемость назначается одна для всех клапанов. существует очень малая вероятность того, что понадобится выполнить расчёт для системы с клапанами различной дымогазопроницаемости на этажах*/

        public object HeightOverall { get; private set; }
        public object HeightBelowZero { get; private set; }
        public object HeightAboveZero { get; private set; }
        public double Lv { get; private set; }
        public double Psv { get; private set; }

        // с клапаном
        public void AddSingle(
            int index,
            double height,
            (double width, double height) ductDims,
            double additionalDuctLength,
            double ksiSum,
            (double width, double height) valveDims)
        {
            if (Helpers.CheckAddArguments(index, height, FirstFloorIndex, LastFloorIndex))
            {
                Floor floor = new Floor(index, height);
                Duct duct = new Duct(ductDims.width, ductDims.height);
                Valve valve = new Valve(valveDims.width, valveDims.height, Sdpsm);
                NetPart netPart = new NetPart(duct, additionalDuctLength, ksiSum, floor, Climate, valve);
                SysPart sysPart = new SysPart(floor, duct, netPart);
                System.Add(index, sysPart);
            }
            return;

        }
        // без клапана
        public void AddSingle(
            int index,
            double height,
            (double width, double height) ductDims,
            double additionalLDuctLength,
            double ksiSum)
        {
            if (Helpers.CheckAddArguments(index, height, FirstFloorIndex, LastFloorIndex))
            {
                Floor floor = new Floor(index, height);
                Duct duct = new Duct(ductDims.width, ductDims.height);
                NetPart netPart = new NetPart(duct, additionalLDuctLength, ksiSum, floor, Climate);
                SysPart sysPart = new SysPart(floor, duct, netPart);
                System.Add(index, sysPart);

            }
        }

        public void RemoveSingle(int index)
        {
            System.Remove(index);
        }
        // с клапаном
        public void AddRange(
            (int first, int last) range,
            double height,
            (double width, double height) ductDims,
            double additionalDuctLength,
            double ksiSum,
            (double width, double height) valveDims)
        {
            if (Helpers.CheckAddArguments((range.first, range.last), height, FirstFloorIndex, LastFloorIndex))
            {
                for (int i = range.first; i <= range.last; i++)
                {
                    if (i == 0) continue;

                    Floor floor = new Floor(i, height);
                    Duct duct = new Duct(ductDims.width, ductDims.height);
                    Valve valve = new Valve(valveDims.width, valveDims.height, Sdpsm);
                    NetPart netPart = new NetPart(duct, additionalDuctLength, ksiSum, floor, Climate, valve);
                    SysPart sysPart = new SysPart(floor, duct, netPart);
                    System.Add(i, sysPart);

                }
            }
            return;
        }
        // без клапана
        public void AddRange(
            (int first, int last) range,
            double height,
            (double width, double height) ductDims,
            double additionalDuctLength,
            double ksiSum)
        {
            if (Helpers.CheckAddArguments((range.first, range.last), height, FirstFloorIndex, LastFloorIndex))
            {
                for (int i = range.first; i <= range.last; i++)
                {
                    if (i == 0) continue;

                    Floor floor = new Floor(i, height);
                    Duct duct = new Duct(ductDims.width, ductDims.height);
                    NetPart netPart = new NetPart(duct, additionalDuctLength, ksiSum, floor, Climate);
                    SysPart sysPart = new SysPart(floor, duct, netPart);
                    System.Add(i, sysPart);

                }
            }
        }
        public void RemoveRange((int first, int last) range)
        {
            for (int i = range.first; i == Helpers.Range((range.first, range.last)); i++)
            {
                System.Remove(i);
            }

        }
        // метод расчета уровней вызывается вручную, так проще и надежнее
        public void CompLevels()
        {
            try
            {
                if (System.Count == Qu)
                {
                    //общая высота всех этажей
                    HeightOverall = System.Sum(lev => lev.Value.Floor.Height);
                    //высота подземных этажей (этажа с индексом 0 быть не может - в методе AddRange() в этом случае добавления не происходит
                    HeightBelowZero = System.Where(lev => lev.Key < 0).Sum(lev => lev.Value.Floor.Height);
                    //высота надземных этажей
                    HeightAboveZero = System.Where(lev => lev.Key > 0).Sum(lev => lev.Value.Floor.Height);
                    //пока подразумевается, что в здании есть надземные этажи, для случай, когда все этажи подземные, пока не обрабатывается
                    double _ = FirstFloorLevel;
                    for (int i = System.First().Key; i <= System.Last().Key; i++)
                    {
                        if (i == 0) continue;
                        System[i].Floor.Level = _;
                        _ += System[i].Floor.Height;
                    }
                }
                else // throw new ArgumentException($"Не все этажи добавлены. Диапазон этажей : ({FirstFloorIndex})-({LastFloorIndex}), добавлены этажи с ({System.First().Key})-({System.Last().Key})");
                    Console.WriteLine($"Не все этажи добавлены. Диапазон этажей : ({FirstFloorIndex})-({LastFloorIndex}), добавлены этажи с ({System.First().Key})-({System.Last().Key})");
            }
            catch (Exception ex)
            {
                // throw new ArgumentException(ex.Message);
                Console.WriteLine(ex.Message);
            }
        }
        public void CompSystem()
        {
            for (int i = System.First().Key; i <= System.Last().Key; i++)
            {
                if (i == 0) continue;
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
                    if (i == 1)
                    {
                        System[i].NetPart.Fluid.TempCels/*текущий участок*/ = System[i-2].NetPart.TsmEnd.ToCelsius()/*конец предыдущий участок*/;
                        System[i].NetPart.FlowStart/*начало текущего участка*/ = System[i-2].NetPart.FlowEnd/*конец предыдущего участок*/;
                        System[i].NetPart.PressureStart/*начало текущего участка*/ = System[i-2].NetPart.PressureEnd/*конец предыдущего участка*/;
                        System[i].NetPart.TsmStart/*начало текущего участка*/ = System[i-2].NetPart.TsmEnd/*конец предыдущего участка*/;

                    }
                    else
                    {
                        System[i].NetPart.Fluid.TempCels/*текущий участок*/ = System[i-1].NetPart.TsmEnd.ToCelsius()/*конец предыдущий участок*/;
                        System[i].NetPart.FlowStart/*начало текущего участка*/ = System[i-1].NetPart.FlowEnd/*конец предыдущего участок*/;
                        System[i].NetPart.PressureStart/*начало текущего участка*/ = System[i-1].NetPart.PressureEnd/*конец предыдущего участка*/;
                        System[i].NetPart.TsmStart/*начало текущего участка*/ = System[i-1].NetPart.TsmEnd/*конец предыдущего участка*/;

                    }
                }
            }

            var lastPart = System.Last().Value.NetPart;
            //так как результаты получаются меньше, чем в программе квм-дым (хотя и незначительно), временно добавим коэффициент. до тех пор, пока расчётная часть не будет доработана
            Lv = lastPart.FlowEnd.ToCubicMetersPerHour(lastPart.Fluid.Density);
            Psv = 1.2 * (lastPart.PressureEnd / lastPart.Fluid.Density);/*давление, приведённое к стандартным условиям. дополнительные потери Pd не добавляются, потому что есть возможность учесть эти потери при добавлении последнего участка сети*/

        }




    }
}