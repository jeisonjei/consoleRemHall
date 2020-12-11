using System.Collections.Generic;

namespace wasmSmokeMan.Shared.RemoveHall
{
    public interface INetwork
    {
        public int First { get; set; }
        public int Last { get; set; }
        public int Qu { get; set; }
        public SortedList<int,SysPart> System { get; set; }
        public double DpAdditional { get; set; }
        public double Sdpsm { get; set; }
        public void AddRange((int first,int last) range,double floorHeight,(double width,double heigth) ductDims,double additionalDuctLength,double ksiSum,(double width,double height) valveDims);
        public void AddRange((int first,int last) range,double floorHeight,double ductDiam,double additionalDuctLength,double ksiSum,(double width,double height) valveDims);
        public void AddSingle(int index, double floorHeight, (double width,double height) ductDims,
            double additionalLDuctLength, double ksiSum,(double width,double height) valveDims);
        public void AddSingle(int index, double floorHeight, double ductDiam,
            double additionalLDuctLength, double ksiSum,(double width,double height) valveDims);
        public void RemoveRange((int first, int last) valueTuple);
        public void RemoveSingle(int index);
        public void CompLevels();
        public void CompSystem();
    }
}