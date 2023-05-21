using UnityEngine;

namespace Unchord
{
    public class ExcavatorRightArmMove : ExcavatorRightArmState
    {
        public override int idConstant => ExcavatorRightArm.c_st_MOVE;

        private float m_zDeg; // 현재 각도
        private float m_dzDeg; // 남은 각도
        private float m_dzDegCos;
        private float m_dzDegSin;
        private Vector2 m_beg;
        private Vector2 m_end;

        public override void OnStateBegin()
        {
            base.OnStateBegin();

            m_zDeg = 0;
            m_dzDeg = 0;
            m_dzDegCos = 1;
            m_dzDegSin = 0;
            m_beg.Set(
                instance.transform.position.x + instance.offset_TrackingBeg.x,
                instance.transform.position.y + instance.offset_TrackingBeg.y
            );
            m_end.Set(
                instance.transform.position.x + instance.offset_TrackingEnd.x,
                instance.transform.position.y + instance.offset_TrackingEnd.y
            );

            instance.transform.eulerAngles = Vector3.forward * m_zDeg;
            instance.gameObject.SetActive(true);
        }

        public override void OnFixedUpdate()
        {
            base.OnFixedUpdate();

            Entity target = instance.target_Tracking;

            m_dzDeg = Vector2.Angle(m_end - m_beg, (Vector2)target.transform.position - m_beg);
            float sin2t = Mathf.Sin(m_dzDeg * 2 * Mathf.Deg2Rad);
            float dVelocity = instance.degSpeed_Tracking * sin2t * Time.fixedDeltaTime;
            float dir = m_GetPointSide(m_beg, m_end, target.transform.position);
            m_zDeg = Utilities.Max<float>(0, m_dzDeg - dVelocity * dir);
            m_dzDegCos = Mathf.Cos(m_zDeg * Mathf.Deg2Rad);
            m_dzDegSin = Mathf.Sin(m_zDeg * Mathf.Deg2Rad);
            m_beg.Set(
                instance.transform.position.x + m_dzDegCos * instance.offset_TrackingBeg.x - m_dzDegSin * instance.offset_TrackingBeg.y,
                instance.transform.position.y + m_dzDegCos * instance.offset_TrackingBeg.y + m_dzDegSin * instance.offset_TrackingBeg.x
            );
            m_end.Set(
                instance.transform.position.x + m_dzDegCos * instance.offset_TrackingEnd.x - m_dzDegSin * instance.offset_TrackingEnd.y,
                instance.transform.position.y + m_dzDegCos * instance.offset_TrackingEnd.y + m_dzDegSin * instance.offset_TrackingEnd.x
            );

            instance.transform.eulerAngles = new Vector3(
                instance.transform.eulerAngles.x,
                instance.transform.eulerAngles.y,
                m_zDeg
            );
        }

        private float m_GetPointSide(Vector2 _beg, Vector2 _end, Vector2 _point)
        {
            Vector2 dV = _end - _beg;
            Vector2 dT = _point - _beg;
            float det = dV.x * dT.y - dV.y * dT.x;

            if(det < 0)
                return -1;
            else if(det > 0)
                return 1;
            else
                return 0;
        }
    }
}