using KadzApp.Models;

namespace KadzApp.FactorAlgorithms
{
	public class DencityAlgorithm
	{
		public double CFactorIndustry(double densitySteel)
		{
			return GlobalVariables.DensityAr / (densitySteel - GlobalVariables.DensityAr);
		}

		public double CFactorModel() => GlobalVariables.DensityN2 / (GlobalVariables.DensityWater - GlobalVariables.DensityN2);
	}
}
