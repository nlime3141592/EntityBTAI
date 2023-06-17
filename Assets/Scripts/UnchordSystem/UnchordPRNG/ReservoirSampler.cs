using System;

namespace Unchord
{
    // NOTE: Reservoir Sampling Algorithm
    public class ReservoirSampler
    {
        public int sumWeight => m_sumWeight;
        public int idxSaved => m_idxSaved;

        private int m_sumWeight = 0;
        private int m_idxSaved = -1;

        public void Clear()
        {
            m_sumWeight = 0;
            m_idxSaved = -1;
        }

        // NOTE: Sampling을 1회 수행하고, Sampling에 성공하면 true를 반환합니다.
        public bool AddStream(Random _prng, int _weight)
        {
            return _prng.Next((m_sumWeight += _weight)) < _weight;
        }

        // NOTE:
        // Sampling을 1회 수행하고, Sampling에 성공하면 매개변수 _index를 반환합니다.
        // 그렇지 않으면 필드에 저장된 m_idxSaved를 반환합니다.
        public int AddStream(Random _prng, int _weight, int _index)
        {
            if(this.AddStream(_prng, _weight))
                return (m_idxSaved = _index);
            else
                return _index;
        }
    }
}