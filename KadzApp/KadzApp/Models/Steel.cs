using System;

namespace KadzApp.Models
{
    public enum WEIGHT_UNITS : int { KG = 1, T = 1000 };
    public enum DENSITY_UNITS : int { GCM3 = 1, KGM3 = 1000 };
    public enum SIZE_UNITS : int { DCM3 = 1, M3 = 1000 };
    public class Steel
    {
        //Private
        private static readonly string WRONG_VALUES = "Dla podanych danych nie można obliczyć wartości";
        //Variables
        private double weight;
        private WEIGHT_UNITS weightUnit;
        private double density;
        private DENSITY_UNITS densityUnit;
        private double size;
        private SIZE_UNITS sizeUnit;
        //Public Attributes
        public double Weight { get { return weight; } set { weight = value; } }
        public WEIGHT_UNITS WeightUnit { get { return weightUnit; } set { weightUnit = value; } }
        public double Density { get { return density; } set { density = value; } }
        public DENSITY_UNITS DensityUnit { get { return densityUnit; } set { densityUnit = value; } }

        public double Size { get { return size; } set { size = value * (int)sizeUnit / (int)SIZE_UNITS.M3; } }
        public SIZE_UNITS SizeUnit { get { return sizeUnit; } set { sizeUnit = value; } }

        //Methods

        public void Calc()
        {
            if (density != 0 && weight != 0)
            {
                double computedWeight = (weight * (int)weightUnit) / (double)((int)WEIGHT_UNITS.KG);
                double computedDensity = GetDensity();
                Size = computedWeight / computedDensity;
            }
            else
            {
                size = -1;
            }
        }
        public double GetDensity() => (density * (int)densityUnit) / (double)((int)DENSITY_UNITS.KGM3);
        private double GetSizeWithUnit() => size * (int)sizeUnit / (double)(int)SIZE_UNITS.M3;
        public string GetSize() => $"{(size < 0 ? WRONG_VALUES : GetSizeWithUnit().ToString())}";
    }
}
