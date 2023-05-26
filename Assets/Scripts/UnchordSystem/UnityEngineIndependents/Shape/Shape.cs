using System;

namespace Unchord
{
    [Serializable]
    public abstract class Shape
    {
        public abstract void Sync(Transform2 _transform);
    }
}