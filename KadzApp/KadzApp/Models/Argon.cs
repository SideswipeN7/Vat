using KadzApp.FactorAlgorithms;
using System;

namespace KadzApp.Models
{
	public class Argon
	{
		private double densitySteel_ = 0;
		private double scale_ = 1;
		private double flowModel_ = 0;

		public double IndustryFlow { get; set; }

		public Algorithms Algorithms { get; set; }

		private MoleWeightAlgorithm moleWeightMlgorithm_;
		private DencityAlgorithm dencityAlgorithm_;

		public Argon()
		{
			moleWeightMlgorithm_ = new MoleWeightAlgorithm();
			dencityAlgorithm_ = new DencityAlgorithm();
		}

		internal void SetInduFlow(double value) => IndustryFlow = value;
		public void SetSteelDensity(double density) => densitySteel_ = density;
		public void SetScale(double scale) => scale_ = scale;
		public double CFactorIndustry() //=> GlobalVariables.MoleWeightAr / (GlobalVariables.DensityAr * densitySteel_);
		{
			if (Algorithms == Algorithms.Mole)
			{
				return moleWeightMlgorithm_.CFactorIndustry(densitySteel_);
			}

			return dencityAlgorithm_.CFactorIndustry(densitySteel_);
		}

		public double CFactorModel() //=> GlobalVariables.MoleWeightN2 / (GlobalVariables.DensityN2 * GlobalVariables.DensityWater);
		{
			if (Algorithms == Algorithms.Mole)
			{
				return moleWeightMlgorithm_.CFactorModel();
			}

			return dencityAlgorithm_.CFactorModel();
		}

		public double FlowFactor()
		{
			var factorModel = CFactorModel();
			var factorIndustry = CFactorIndustry();
			var division = factorModel / factorIndustry;

			return Math.Pow(division, -0.5);
		}


		internal double GetFlowModel() => flowModel_;

		internal double GetFlowScale() => Math.Pow(scale_, 2.5D);

		internal void Calc()
		{
			var flowScale = GetFlowScale();
			var flowFactor = FlowFactor();

			flowModel_ = IndustryFlow * flowScale * flowFactor;
		}
	}
}
