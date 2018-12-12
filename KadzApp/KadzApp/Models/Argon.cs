using System;

namespace KadzApp.Models
{
    class Argon
    {
        private readonly static double densityO2_ = 1.429;
        private readonly static double densityWater_ = 1000.0;
        private readonly static double densityAr_ = 1.784;
        public readonly static double moleWeightAr_ = 39.948;
        public readonly static double moleWeightO2_ = 28.96;

        private double densitySteel_ = 0;
        private double industryFlow_ = 0;
        private double scale_ = 0;
        private double flowModel_ = 0;

        internal void SetInduFlow(double value) => industryFlow_ = value;
        public void SetSteelDensity(double density) => densitySteel_ = density;
        public void SetScale(double scale) => scale_ = scale;
        public double CFactorIndustry() => moleWeightAr_ / (densityAr_ * densitySteel_);

        public double CFactorModel() => moleWeightO2_ / (densityO2_ * densityWater_);

        public double FlowFactor() => Math.Pow((CFactorModel() / CFactorIndustry()), -0.5);


        internal double GetFlowModel() => flowModel_;

        internal double GetFlowScale() => Math.Pow(scale_, 2.5D);

        internal void Calc()
        {
            flowModel_ = industryFlow_ * GetFlowScale() * FlowFactor();
        }
    }
}
