using KadzApp.Models;

namespace KadzApp.FactorAlgorithms
{
	public class MoleWeightAlgorithm
	{
		public double CFactorIndustry(double densitySteel)
		{
			return GlobalVariables.MoleWeightAr / (GlobalVariables.DensityAr * densitySteel);
		}

		public double CFactorModel() => GlobalVariables.MoleWeightN2 / (GlobalVariables.DensityN2 * GlobalVariables.DensityWater);
	}
}
