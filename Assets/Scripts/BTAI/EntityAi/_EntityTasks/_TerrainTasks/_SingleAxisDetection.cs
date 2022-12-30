using UnityEngine;

namespace UnchordMetroidvania
{
    public class _SingleAxisDetection : _TerrainDetection
    {
        public int baseLookDir;

        public _SingleAxisDetection(int rayCount)
        : base(rayCount)
        {

        }

        public bool CheckMoveDirection(ref Vector2 moveDirection, int lookDir)
        {
            int iSelected = -1;
            float ySelected = 0.0f;

            for(int i = 0; i < hits.Length; ++i)
            {
                if(!hits[i] || hits[i].distance > hLength)
                    continue;

                float y = (float)lookDir * -(hits[i].normal.x);

                if(iSelected == -1 || y > ySelected)
                {
                    iSelected = i;
                    ySelected = y;
                }
            }

            if(iSelected == -1)
            {
                moveDirection.Set(0.0f, 0.0f);
                return false;
            }
            else
            {
                float dX = (float)lookDir * hits[iSelected].normal.y;
                float dY = ySelected;
                moveDirection.Set(dX, dY);
                return true;
            }
        }
    }
}