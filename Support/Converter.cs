using System;

namespace wasmSmokeMan.Shared.RemoveHall
{
    public static class Converter
    {
        public static double ToKelvin(this double prop)
        {
            return prop + 273;
        }
        public static double ToCelsius(this double prop)
        {
            return prop - 273;
        }
        public static double RoundTo0(this double prop)
        {
            return Math.Round(prop, 0);
        }
        public static double RoundTo1(this double prop)
        {
            return Math.Round(prop, 1);
        }
        public static double RoundTo2(this double prop)
        {
            return Math.Round(prop, 2);
        }
        public static double RoundTo3(this double prop)
        {
            return Math.Round(prop, 3);
        }
        public static double RoundTo4(this double prop)
        {
            return Math.Round(prop, 4);
        }
        public static double ToMeters(this double prop)
        {
            return prop/1000;
        }
        public static double ToMillimeters(this double prop)
        {
            return prop*1000;
        }
        public static double ToKgHour(this double prop)
        {
            return prop * 3600;
        }
        public static double ToCubicMetersPerHour(this double prop,double density)
        {
            return (prop * 3600) / density;
        }

        public static double ToDouble(this string prop)
        {
            return Convert.ToDouble(prop);
        }

        public static int ToInt(this string prop)
        {
            return Convert.ToInt32(prop);
        }
    }
}
