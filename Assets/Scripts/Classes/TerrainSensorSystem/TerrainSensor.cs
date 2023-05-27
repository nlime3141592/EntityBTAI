using System;
using UnityEngine;

namespace Unchord
{
    public abstract class TerrainSensorBase
    {
        public static bool Sense(in TerrainSenseData _data)
        {
            if(_data == null)
                return false;

            _data.hitData = Physics2D.Raycast(_data.origin, _data.direction, _data.dLength, _data.targetLayer);

            if(_data.bOnDetected = _data.hitData)
                _data.bOnHit = _data.hitData.distance <= _data.hLength;
            else
                _data.bOnHit = false;

            return _data.bOnDetected;
        }
    }
}