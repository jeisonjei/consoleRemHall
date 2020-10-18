using System.Collections;

namespace consoleRemHall.SemanticObjects
{
    public interface Network
    {
        public double dpAdditional { get; set; }
        public SortedList System { get; set; }
        public void AddRange();
        public void AddSingle();
        public void RemoveRange();
        public void RemoveSingle();
        public void CompLevels();
    }
}