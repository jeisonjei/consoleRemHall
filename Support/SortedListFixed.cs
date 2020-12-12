using System;
using System.Collections.Generic;
namespace wasmSmokeMan.Shared.RemoveHall
{
    public class SortedListFixed<TKey, TValue> : SortedList<TKey, TValue>
    {
        public SortedListFixed(int size)
        {
            Size = size;
        }
        public int Size { get; set; }
        new public void Add(TKey key, TValue value)
        {
            try
            {
                if (this.Count < Size) base.Add(key, value);
                else Console.WriteLine($"Все этажи уже добавлены");
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("An item with the same key has already been added"))
                    // throw new ArgumentException($"Этаж с индексом {key} уже добавлен");
                    Console.WriteLine($"Этаж с индексом {key} уже добавлен");
            }
        }
    }
}