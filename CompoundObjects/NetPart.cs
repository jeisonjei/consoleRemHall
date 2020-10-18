using System;
using consoleRemHall.SemanticObjects;
using consoleRemHall.SimpleObjects;

namespace consoleRemHall.CompoundObjects
{
    //назначение этого класса - упростить построение сети. Данные о дополнительной длине и КМС можно было бы добавить в класс Duct, но по смыслу эти данные относятся не к участку воздуховода, а к участку сети, так что этот класс описывает как раз этот участок сети. При этом высота этажа, которая используется для расчёта потери давления dP, передаётся как аргумент метода 
    public class NetPart
    {
        private double dp;

        //так как этот класс предполагается использовать совместно с данными об этаже, то можно определить, что этот класс является как-бы смысловым блоком, существующим в контексте данных об уровнях здания, то есть является зависимым от данных другого класса. Это к тому, что была идея использовать свойств FloorHeight, но так как это значение всё равно зависит от других данных, разумнее использовать floorHeight как параметр метода  
        public Duct Duct { get; set; }
        public double AdditionalDuctLength { get; set; }
        public double KsiSum { get; set; }

        public double DP
        {
            get => Floor.Height*Duct.dP+AdditionalDuctLength*Duct.dP+((KsiSum*Duct.Fluid.Density*Math.Pow(Duct.Velocity,2))/2);
            set => dp = value;
        }

        public Floor Floor { get; set; }

        //пока решено не усложнять слишком программу и обойтись без функции добавления по-отдельности местных сопротивлений. Посчитать сумму КМС на этаже нетрудно.
        public NetPart(Floor floor /*текущий этаж*/,
            Duct duct /*воздуховод*/,
            double additionalDuctLength/*добавочная длина, если воздуховод не проходит на этаже прямо снизу вверх*/, 
            double ksiSum /*сумма КМС на участке*/)
        {
            Duct = duct;
            AdditionalDuctLength = additionalDuctLength;
            KsiSum = ksiSum;
            Floor = floor;
        }
    }
}