using System.Collections;
using System.Collections.Generic;
using consoleRemHall.CompoundObjects;

namespace consoleRemHall.SemanticObjects
{
    public interface INetwork
    {
        public int First { get; set; }
        public int Last { get; set; }
        public int Qu { get; set; }
        public SortedList<int,SysPart> System { get; set; }
        public double DpAdditional { get; set; }
        public void AddRange((int first,int last) range,double floorHeight,(double width,double heigth) ductDims,double additionalDuctLength,double ksiSum);
        public void AddRange((int first,int last) range,double floorHeight,double ductDiam,double additionalDuctLength,double ksiSum);
        public void AddSingle(int index, double floorHeight, (double width,double height) ductDims,
            double additionalLDuctLength, double ksiSum);
        public void AddSingle(int index, double floorHeight, double ductDiam,
            double additionalLDuctLength, double ksiSum);
        public void RemoveRange();
        public void RemoveSingle();
        public void CompLevels();
        public double CompSystem();
    }
}