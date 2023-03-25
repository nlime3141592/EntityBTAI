using System;
using System.Collections.Generic;
using UnityEngine;

namespace Unchord
{
    public class LTRBViewer : MonoBehaviour
    {
        public bool bShow = false;
        public bool flipX = false;
        public bool flipY = false;
        public List<_LTRB> ltrbs;

        [Serializable]
        public struct _LTRB
        {
            public Color color;
            public LTRB ltrb;
        }

        private void OnDrawGizmos()
        {
            if(!bShow)
                return;

            foreach(_LTRB range in ltrbs)
                range.ltrb.Draw(transform.position, flipX, flipY, range.color);
        }
    }
}