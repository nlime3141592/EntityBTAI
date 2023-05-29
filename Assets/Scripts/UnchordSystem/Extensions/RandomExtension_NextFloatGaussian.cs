namespace System
{
    public static partial class RandomExtension
    {
        public static float NextFloatGaussian(this Random _prng, float _min, float _max)
        {
            return (float)_prng.NextDoubleGaussian(_min, _max);
        }
    }
}