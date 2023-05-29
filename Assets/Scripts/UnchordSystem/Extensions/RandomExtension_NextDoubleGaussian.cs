namespace System
{
    public static partial class RandomExtension
    {
        public static double NextDoubleGaussian(this Random _prng, double _min, double _max)
        {
            double u, v, S;

            do
            {
                u = 2 * _prng.NextDouble() - 1;
                v = 2 * _prng.NextDouble() - 1;
                S = u * u + v * v;
            }
            while(S >= 1);

            double x = u * Math.Sqrt(-2 * Math.Log(S) / S);

            double mean = (_min + _max) / 2;
            double sigma = (_max - mean) / 3;
            double finalValue = x * sigma + mean;

            if(finalValue < _min)
                return _min;
            else if(finalValue > _max)
                return _max;
            else
                return finalValue;
        }
    }
}