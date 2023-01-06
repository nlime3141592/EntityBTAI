using System;
using UnityEngine;

namespace UnchordMetroidvania
{
    public class TerrainSensor
    {
        private int m_layerMask => 1 << LayerMask.NameToLayer("Terrain");

        public bool CheckFloor(Vector2 origin, float checkLength)
        {
            RaycastHit2D hit = Physics2D.Raycast(origin, Vector2.down, checkLength, m_layerMask);
            Debug.DrawLine(origin, origin + Vector2.down * checkLength, Color.white);
            return hit;
        }

        public bool CheckCeil(Vector2 origin, float checkLength)
        {
            RaycastHit2D hit = Physics2D.Raycast(origin, Vector2.up, checkLength, m_layerMask);
            Debug.DrawLine(origin, origin + Vector2.up * checkLength, Color.white);
            return hit;
        }

        public bool CheckWallFront(Vector2 origin, float checkLength, float lookDirX)
        {
            Vector2 l = m_LookDirFrontX(lookDirX);
            RaycastHit2D hit = Physics2D.Raycast(origin, l, checkLength, m_layerMask);
            Debug.DrawLine(origin, origin + l * checkLength, Color.white);
            return hit;
        }

        public bool CheckWallBack(Vector2 origin, float checkLength, float lookDirX)
        {
            Vector2 l = m_LookDirBackX(lookDirX);
            RaycastHit2D hit = Physics2D.Raycast(origin, l, checkLength, m_layerMask);
            Debug.DrawLine(origin, origin + l * checkLength, Color.white);
            return hit;
        }

        public bool CheckLedgeHorizontal(Vector2 origin, float checkLength, float lookDirX)
        {
            Vector2 l = m_LookDirFrontX(lookDirX);
            RaycastHit2D hit = Physics2D.Raycast(origin, l, checkLength, m_layerMask);
            Debug.DrawLine(origin, origin + l * checkLength, Color.white);
            return hit;
        }

        public bool CheckLedgeVerticalUp(Vector2 origin, float checkLength)
        {
            RaycastHit2D hit = Physics2D.Raycast(origin, Vector2.up, checkLength, m_layerMask);
            Debug.DrawLine(origin, origin + Vector2.up * checkLength, Color.white);
            return hit;
        }

        public bool CheckLedgeVerticalDown(Vector2 origin, float checkLength)
        {
            RaycastHit2D hit = Physics2D.Raycast(origin, Vector2.down, checkLength, m_layerMask);
            Debug.DrawLine(origin, origin + Vector2.down * checkLength, Color.white);
            return hit;
        }

        private Vector2 m_LookDirFrontX(float lookDirX)
        {
            Vector2 l = Vector2.zero;

            if(lookDirX < 0)
                l = Vector2.left;
            else if(lookDirX > 0)
                l = Vector2.right;

            return l;
        }

        private Vector2 m_LookDirBackX(float lookDirX)
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