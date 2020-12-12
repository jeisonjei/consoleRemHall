namespace wasmSmokeMan.Shared.RemoveHall
{
    public class Floor
    {
        public int Index { get; set; }
        public double Height { get; set; }
        public double Level { get; set; }

        public Floor(int index, double height, double level=0)
        {
            Index = index;
            Height = height;
            Level = level;
        }
    }
}