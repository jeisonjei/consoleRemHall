using System;

namespace wasmSmokeMan.Shared.RemoveHall
{
    public static class Helpers
    {
        static bool noerrors=true;

        public static int Range((int first, int last) range)
        {
            if (noerrors)
            {
                if (range.first < 0)
                    return range.last - range.first;
                if (range.first > 0)
                    return range.last + 1 - range.first;
            }
            return 0;
        }
        public static bool CheckAddArguments((int first, int last) range)
        {
            if (range.first == 0 || range.last == 0)
            {
                noerrors = false;
                throw new ArgumentException($"Первый или последний этаж диапазона не могут иметь индекс {0}. Для подземных этажей используйте индекс '-1', '-2' итд");
                //Console.WriteLine($"Первый или последний этаж диапазона не могут иметь индекс ({0}). Для подземных этажей используйте индекс '-1', '-2' итд");
                return noerrors;
            }
            noerrors = true;
            return noerrors;

        }
        public static bool CheckAddArguments(int index, double height, int firstFloorIndex, int lastFloorIndex)
        {
            if (index == 0)
            {
                noerrors = false;
                throw new ArgumentException($"Нельзя добавить этаж с индексом {0}. Для подземных этажей используйте индекс '-1', '-2' итд");
                //Console.WriteLine($"Нельзя добавить этаж с индексом ({0}). Для подземных этажей используйте индекс '-1', '-2' итд");
                return noerrors;
            }
            if (index < firstFloorIndex || index > lastFloorIndex)
            {
                noerrors = false;
                throw new ArgumentException($"Индекс этажа вне диапазона между первым {firstFloorIndex} и последним {lastFloorIndex} этажами");
                //Console.WriteLine($"Индекс этажа ({index}) вне диапазона между первым ({firstFloorIndex}) и последним ({lastFloorIndex}) этажами");
                return noerrors;
            }
            noerrors = true;
            return noerrors;
        }
        public static bool CheckAddArguments((int first, int last) range, double height, int firstFloorIndex, int lastFloorIndex)
        {
            if (range.first == 0 || range.last == 0)
            {
                noerrors = false;
                throw new ArgumentException($"Первый или последний этаж диапазона не могут иметь индекс {0}. Для подземных этажей используйте индекс '-1', '-2' итд");
                //Console.WriteLine($"Первый или последний этаж диапазона не могут иметь индекс ({0}). Для подземных этажей используйте индекс '-1', '-2' итд");
                return noerrors;
            }
            if (range.first < firstFloorIndex || range.last > lastFloorIndex)
            {
                noerrors = false;
                throw new ArgumentException($"Добавляемый диапазон {range.first}-{range.last} выходит за пределы диапазона этажей между первым {firstFloorIndex} и последним {lastFloorIndex} этажами");
                //Console.WriteLine($"Добавляемый диапазон ({range.first})-({range.last}) выходит за пределы диапазона этажей между первым ({firstFloorIndex}) и последним ({lastFloorIndex}) этажами");
                return noerrors;
            }
            noerrors = true;
            return noerrors;
        }
    }
}