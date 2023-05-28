using System;
using UnityEngine;

namespace Unchord
{
    public static class TerrainSensor
    {
        private static int s_m_layerMask => 1 << LayerMask.NameToLayer("Terrain");
        private static int s_m_slabLayer => 1 << LayerMask.NameToLayer("Slab");

        public static bool CheckFloor(Vector2 origin, float checkLength)
        {
            int layer = s_m_layerMask;
            layer |= s_m_slabLayer;

            RaycastHit2D hit = Physics2D.Raycast(origin, Vector2.down, checkLength, layer);
            Debug.DrawLine(origin, origin + Vector2.down * checkLength, Color.white);

            bool bHit = hit;
            Slab slab;

            if(!bHit)
                return bHit;
            else if(hit.collider.gameObject.TryGetComponent<Slab>(out slab))
                return !slab.bIgnored;
            else
                return bHit;
        }

        public static bool CheckCeil(Vector2 origin, float checkLength)
        {
            RaycastHit2D hit = Physics2D.Raycast(origin, Vector2.up, checkLength, s_m_layerMask);
            Debug.DrawLine(origin, origin + Vector2.up * checkLength, Color.white);
            return hit;
        }

        public static bool CheckWallFront(Vector2 origin, float checkLength, float lookDirX)
        {
            Vector2 l = s_m_LookDirFrontX(lookDirX);
            RaycastHit2D hit = Physics2D.Raycast(origin, l, checkLength, s_m_layerMask);
            Debug.DrawLine(origin, origin + l * checkLength, Color.white);
            return hit;
        }

        public static bool CheckWallBack(Vector2 origin, float checkLength, float lookDirX)
        {
            Vector2 l = s_m_LookDirBackX(lookDirX);
            RaycastHit2D hit = Physics2D.Raycast(origin, l, checkLength, s_m_layerMask);
            Debug.DrawLine(origin, origin + l * checkLength, Color.white);
            return hit;
        }

        public static bool CheckLedgeHorizontal(Vector2 origin, float checkLength, float lookDirX)
        {
            Vector2 l = s_m_LookDirFrontX(lookDirX);
            RaycastHit2D hit = Physics2D.Raycast(origin, l, checkLength, s_m_layerMask);
            Debug.DrawLine(origin, origin + l * checkLength, Color.white);
            return hit;
        }

        public static bool CheckLedgeVerticalUp(Vector2 origin, float checkLength)
        {
            RaycastHit2D hit = Physics2D.Raycast(origin, Vector2.up, checkLength, s_m_layerMask);
            Debug.DrawLine(origin, origin + Vector2.up * checkLength, Color.white);
            return hit;
        }

        public static bool CheckLedgeVerticalDown(Vector2 origin, float checkLength)
        {
            RaycastHit2D hit = Physics2D.Raycast(origin, Vector2.down, checkLength, s_m_layerMask);
            Debug.DrawLine(origin, origin + Vector2.down * checkLength, Color.white);
            return hit;
        }

        private static Vector2 s_m_LookDirFrontX(float lookDirX)
        {
            Vector2 l = Vector2.zero;

            if(lookDirX < 0)
                l = Vector2.left;
            else if(lookDirX > 0)
                l = Vector2.right;

            return l;
        }

        private static Vector2 s_m_LookDirBackX(float lookDirX)
        {
            Vector2 l = Vector2.zero;

            if(lookDirX < 0)
                l = Vector2.right;
            else if(lookDirX > 0)
                l = Vector2.left;

            return l;
        }
    }
}