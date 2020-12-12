using System;
using System.Collections.Generic;

namespace wasmSmokeMan.Shared.RemoveHall
{
    public class Floors
    {
        public Floors(int firstFloorIndex, int lastFloorIndex, double firstFloorLevel)
        {
            FirstFloorIndex = firstFloorIndex;
            LastFloorIndex = lastFloorIndex;
            FirstFloorLevel = firstFloorLevel;
            Levels = new SortedListFixed<int, Floor>(Helpers.Range((firstFloorIndex, lastFloorIndex)));
        }

        public SortedListFixed<int, Floor> Levels { get; set; }
        public int FirstFloorIndex { get; set; }
        public int LastFloorIndex { get; set; }
        public int Qu
        {
            get
            {
                return Helpers.Range((FirstFloorIndex, LastFloorIndex));
            }
        }
        public double FirstFloorLevel { get; set; }

        public void AddSingle(int index, double height)
        {
            if (Helpers.CheckAddArguments(index, height, FirstFloorIndex, LastFloorIndex))
            {
                Levels.Add(index, new Floor(index, height));
            }
            return;

        }

        public void RemoveSingle(int index)
        {
            Levels.Remove(index);
        }
        public void AddRange((int first, int last) range, double height)
        {
            if (Helpers.CheckAddArguments((range.first, range.last), height, FirstFloorIndex, LastFloorIndex))
            {
                for (int i = range.first; i <= range.last; i++)
                {
                    if (i == 0) continue;
                    Levels.Add(i, new Floor(i, height));
                }
            }
            return;
        }

        public void RemoveRange((int first, int last) range)
        {
            for (int i = range.first; i == Helpers.Range((range.first, range.last)); i++)
            {
                Levels.Remove(i);
            }

        }
        // метод расчета уровней вызывается вручную, так проще и надежнее
        public void CompLevels()
        {
            if (Levels.Count == Qu)
            {

            }
        }


    }
}