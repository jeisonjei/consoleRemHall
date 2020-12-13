using System;
using System.Collections.Generic;
namespace wasmSmokeMan.Shared.RemoveHall
{
    public class SortedListFixed<TKey, TValue> : SortedList<TKey, TValue>
    {
        public SortedListFixed(SortedListFixed<int, SysPart> system) : base() { }
        public SortedListFixed(int size = 10)
        {
            Size = size;
        }

        public int Size { get; set; }
        new public void Add(TKey key, TValue value)
        {
            try
            {
                Console.WriteLine($"this.Count : {this.Count}");
                Console.WriteLine($"Size : {Size}");
                if (this.Count < Size)
                {
                    base.Add(key, value);
                }

                else
                {
                    throw new ArgumentException($"Все этажи уже добавлены");
                    //else Console.WriteLine($"Все этажи уже добавлены");
                }
            }
            catch (Exception ex)
            {

                if (ex.Message.Contains("An item with the same key has already been added"))
                {
                    throw new ArgumentException($"Этаж с индексом {key} уже добавлен");
                }
                else
                {
                    throw new ArgumentException(ex.Message);
                }
                //Console.WriteLine($"Этаж с индексом {key} уже добавлен");
            }
        }
    }
}