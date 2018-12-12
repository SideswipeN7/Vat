using System;


namespace KadzApp.Models
{
    public enum DIMENSIONS_UNITS : int { DCM = 1, M = 10 }
    class Vat
    {
        //Private
        //Variables
        private double height;
        private DIMENSIONS_UNITS heightUnit = DIMENSIONS_UNITS.M;
        private double upperDiameter;
        private DIMENSIONS_UNITS upperDiameterUnit = DIMENSIONS_UNITS.M;
        private double lowerDiameter;
        private DIMENSIONS_UNITS lowerDiameterUnit = DIMENSIONS_UNITS.M;
        private double steelSize;
        private SIZE_UNITS sizeUnit = SIZE_UNITS.M3;
        private bool compute;
        private bool useScale;
        private int vatScaleLeft = 1;
        private int vatScaleRight = 2;
        private int rScaleLeft = 1;
        private int rScaleRight = 2;

        //Public Attributes
        public double Height { get { return height; } set { height = value; } }
        public DIMENSIONS_UNITS HeightUnit { get { return heightUnit; } set { heightUnit = value; } }
        public double UpperDiamater { get { return upperDiameter; } set { upperDiameter = value; } }
        public DIMENSIONS_UNITS UpperDiamaterUnit { get { return upperDiameterUnit; } set { upperDiameterUnit = value; } }
        public double LowerDiamater { get { return lowerDiameter; } set { lowerDiameter = value; } }
        public DIMENSIONS_UNITS LowerDiamaterUnit { get { return lowerDiameterUnit; } set { lowerDiameterUnit = value; } }
        public double SteelSize { get { return steelSize; } set { steelSize = value; } }
        public SIZE_UNITS SteelSizeUnit { get { return sizeUnit; } set { sizeUnit = value; } }
        public bool Compute { get { return compute; } set { compute = value; } }
        public bool UseScale { get { return useScale; } set { useScale = value; } }
        public int RscalerLeft { get { return rScaleLeft; } set { rScaleLeft = value; } }
        public int RscalerRight { get { return rScaleRight; } set { rScaleRight = value; } }
        public int VatscaleLeft { get { return vatScaleLeft; } set { vatScaleLeft = value; } }
        public int VatscaleRight { get { return vatScaleRight; } set { vatScaleRight = value; } }

        public void Calc()
        {
            if (compute)
            {
                if (SteelSize != 0)
                {
                    double scale = rScaleLeft / (double)rScaleRight;
                    double computedSize = Math.Round(SteelSize * (int)SteelSizeUnit / (int)SIZE_UNITS.M3);
                    double lowerShred = Math.PI * ((4 * Math.Pow(scale, 2)) + (4 * scale) + 1);
                    double upperShred = 3 * computedSize;
                    double shred = upperShred / lowerShred;
                    double computedDiamter = Math.Pow(shred, 0.33) * 2;


                    upperDiameter = (computedDiamter * rScaleRight / (double)rScaleLeft) * ((int)upperDiameterUnit / (double)(int)DIMENSIONS_UNITS.M);
                    lowerDiameter = computedDiamter * ((int)lowerDiameterUnit / (double)(int)DIMENSIONS_UNITS.M);
                    height = 4 * computedDiamter * ((int)heightUnit / (double)(int)DIMENSIONS_UNITS.M);
                }
            }
            else
            {
                double computedHeight = height * ((int)heightUnit / (double)(int)DIMENSIONS_UNITS.M);
                double computedLowerR = (lowerDiameter / 2) * ((int)lowerDiameterUnit / (double)(int)DIMENSIONS_UNITS.M);
                double computedUpperR = (upperDiameter / 2) * ((int)upperDiameterUnit / (double)(int)DIMENSIONS_UNITS.M);

                steelSize = ((Math.PI / 3) * (computedHeight) +
                    (Math.Pow(computedUpperR, 2) + (computedUpperR * computedLowerR) + Math.Pow(computedLowerR, 2))) * (int)SteelSizeUnit / (int)SIZE_UNITS.M3;
            }
        }

        internal double UpperDiamaterInScale() => (UpperDiamater * vatScaleLeft * (int)UpperDiamaterUnit / (double)(int)DIMENSIONS_UNITS.M) / (double)vatScaleRight;

        internal double LowerDiamaterInScale() => (lowerDiameter * vatScaleLeft * (int)LowerDiamaterUnit / (double)(int)DIMENSIONS_UNITS.M) / (double)vatScaleRight;

        internal double HeightInScale() => (height * vatScaleLeft * (int)HeightUnit / (int)DIMENSIONS_UNITS.M) / (double)(double)vatScaleRight;

        internal double SteelSizeInScale() => (steelSize * vatScaleLeft * (int)SteelSizeUnit / (double)(int)SIZE_UNITS.M3) / (double)vatScaleRight;
        //public string Height => $"{Height * (int)HeightUnit / (int)DIMENSIONS_UNITS.M}";
        //public string UpperDiamater => $"{UpperDiamater * (int)UpperDiamaterUnit / (int)DIMENSIONS_UNITS.M}";
        //public string LowerDiameter => $"{LowerDiamater * (int)LowerDiamaterUnit / (int)DIMENSIONS_UNITS.M}";
        //public string SteelSize => $"{SteelSize * (int)SteelSizeUnit / (int)SIZE_UNITS.M3}";

        public double GetScale() => vatScaleLeft / (double)vatScaleRight;

    }



}
