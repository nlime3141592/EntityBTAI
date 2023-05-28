using System;

namespace System
{
    public static class RandomExtension
    {
        /// <summary>
        /// 표준정규분포표의 확률변수 별 기댓값에 기반해 임의의 난수를 생성합니다.
        /// </summary>
        public static double SigmaNextDouble(this Random prng, double average, double sigma)
        {
            double k, d, v;

            // NOTE: 표준정규분포(평균: 0, 표준편차: 1)의 확률밀도함수를 k = f(d)라 하면, d = Inverse of f(k)
            // TODO: k = 1.0 - prng.NextDouble();로 대체하여 사용할지 고민해보아야 한다.
            k = prng.NextDouble();
            d = Math.Sqrt(-2.0 * Math.Log(k));

            // NOTE:
            // 6-Sigma 이론에 기반해 최대로 얻을 수 있는 Sigma 범위를 4.5 Sigma로 설정함.
            if(d > 4.5) d = 4.5;
            if(prng.Next(2) == 0) d *= -1.0;

            // NOTE: Inverse of Normalization.
            v = sigma * d + average;

            return v;
        }

        public static double RandomGaussian(this Random prng, double min, double max)
        {
            double u, v, S;

            do
            {
                u = 2 * prng.NextDouble() - 1;
                v = 2 * prng.NextDouble() - 1;
                S = u * u + v * v;
            }
            while(S >= 1);

            double x = u * Math.Sqrt(-2 * Math.Log(S) / S);

            double mean = (min + max) / 2;
            double sigma = (max - mean) / 3;
            double finalValue = x * sigma + mean;

            if(finalValue < min)
                return min;
            else if(finalValue > max)
                return max;
            else
                return finalValue;
        }

        /// <summary>
        /// 표준정규분포표의 확률변수 별 기댓값에 기반해 임의의 난수를 생성합니다.
        /// 양 극단 값(평균을 기준으로 4.5-Sigma에 해당하는 값)은 약 0.0006% 확률로 등장합니다.
        /// </summary>
        public static double RangeNextDouble(this Random prng, double average, double range)
        {
            return prng.SigmaNextDouble(average, range * 0.111111111111111111111111);
        }
    }
}