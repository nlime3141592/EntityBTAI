using System.Collections.Generic;
using UnityEngine;

namespace UnchordMetroidvania
{
    public class ExcavatorArm : MonoBehaviour
    {
        #region Variables
        [Header("Joint Options")]
        public List<Transform> joints;
        public float angleSpeed1 = 10.0f;
        public float angleSpeed2 = 540.0f;
        [Range(0.000f, 1.000f)] public float allowance = 0.01f;

        [Header("Target Options")]
        public Transform targetTransform;

        [Header("Runtime Options")]
        public int selectedJoint;
        [HideInInspector] public Vector2 debug_joint;
        [HideInInspector] public Vector2 debug_hand;
        [HideInInspector] public Vector2 debug_target;
        [HideInInspector] public float yAngle;
        private List<int> m_sortIdxs;
        private List<int> m_tmp_sortIdxs;
        private List<int> m_tmp_mergeIdxs;
        private Vector3 m_startPos;
        #endregion

        #region Unity Event Functions
        private void Start()
        {
            m_sortIdxs = new List<int>(joints.Capacity);
            m_tmp_sortIdxs = new List<int>(joints.Capacity);
            m_tmp_mergeIdxs = new List<int>(joints.Capacity);
            m_startPos = transform.localPosition;
        }

        private void Update()
        {
            m_SyncList<int>(m_sortIdxs);
            m_SyncList<int>(m_tmp_sortIdxs);
            m_SyncList<int>(m_tmp_mergeIdxs);
            m_SortIdxs();

            transform.localPosition = m_startPos;

            if(targetTransform == null)
                return;

            selectedJoint = m_SelectJoint(targetTransform.position);
            m_Trace(targetTransform.position, selectedJoint);
        }
        #endregion

        #region AI
        private void m_Trace(Vector2 target, int idxJoint)
        {
            // 관절-손 관절-타겟 간 정렬
            Vector2 joint = joints[idxJoint].position;
            Vector2 hand = joints[joints.Count - 1].position;

            float dirRot = m_RotateDirection(joint, hand, target);
            float yWeight = m_yAngleWeight();
            float zWeight = m_zAngleWeight(joint, hand, target);
            float dT = Time.deltaTime;

            float finalSpeed = angleSpeed1 + zWeight * angleSpeed2;
            float dAngleSpeed = dirRot * yWeight * finalSpeed * dT;

            debug_joint = joint;
            debug_hand = hand;
            debug_target = target;

            if(zWeight > allowance)
                transform.eulerAngles += Vector3.forward * dAngleSpeed;
        }

        private int m_SelectJoint(Vector2 target)
        {
            int jointIdx = m_sortIdxs.Count;
            int _i = 0;
            float dJ = 0;
            float dT = 0;

            do
            {
                _i = m_sortIdxs[--jointIdx];
                dJ = m_SqrDistance(joints[0].position, joints[_i].position);
                dT = m_SqrDistance(joints[0].position, target);
            } while(dT <= dJ && jointIdx > 0);

            return _i % (m_sortIdxs.Count - 1);
        }

        // NOTE: 인덱스 병합 정렬
        private void m_SortIdxs()
        {
            for(int i = 0; i < joints.Count; ++i)
                m_tmp_sortIdxs[i] = i;

            m_rec_SortIdxs(0, joints.Count - 1);
            m_CopyShallow<int>(m_tmp_mergeIdxs, m_sortIdxs);
        }

        // NOTE: 인덱스 병합 정렬(재귀 호출)
        private void m_rec_SortIdxs(int beg, int end)
        {
            if(beg == end)
                return;

            int mid = (beg + end) / 2;
            int pl = beg;
            int pr = mid + 1;
            int k = beg - 1;

            m_rec_SortIdxs(beg, mid);
            m_rec_SortIdxs(mid + 1, end);

            while(pl <= mid && pr <= end)
            {
                float dL = m_SqrDistance(joints[0].position, joints[pl].position);
                float dR = m_SqrDistance(joints[0].position, joints[pr].position);

                if(dL < dR)
                    m_tmp_mergeIdxs[++k] = m_tmp_sortIdxs[pl++];
                else
                    m_tmp_mergeIdxs[++k] = m_tmp_sortIdxs[pr++];
            }
            while(pl <= mid) m_tmp_mergeIdxs[++k] = m_tmp_sortIdxs[pl++];
            while(pr <= end) m_tmp_mergeIdxs[++k] = m_tmp_sortIdxs[pr++];

            for(int i = beg; i <= end; ++i)
                m_tmp_sortIdxs[i] = m_tmp_mergeIdxs[i];
        }

        /// <summary>
        /// 대상이 벡터의 어느 쪽에 있는지 판단합니다.
        /// </summary>
        /// <param name="beg">벡터 시점</param>
        /// <param name="end">벡터 종점</param>
        /// <param name="target">대상 점의 위치</param>
        /// <returns>-1: 오른쪽</returns>
        /// <returns>1: 왼쪽</returns>
        private float m_RotateDirection(Vector2 beg, Vector2 end, Vector2 target)
        {
            Vector2 dV = end - beg;
            Vector2 dT = target - beg;
            float y = dV.x * dT.y - dV.y * dT.x;
            float sqrDist = dV.x * dV.x + dV.y * dV.y;

            if(y < 0)
                return -1;
            else if(y > 0)
                return 1;
            else
                return 0;
        }

        // 좌, 우 방향 회전에 따라 변함
        private float m_yAngleWeight()
        {
            return yAngle == 0 ? 1 : -1;
        }

        private float m_zAngleWeight(Vector2 joint, Vector2 hand, Vector2 target)
        {
            return Vector2.Angle(target - joint, hand - joint) / 180.0f;
        }

        private float m_SqrDistance(Vector2 p, Vector2 q)
        {
            float dx = q.x - p.x;
            float dy = q.y - p.y;
            return dx * dx + dy * dy;
        }
        #endregion

        #region Collection Options
        private void m_SyncList<T>(List<T> collection)
        {
            if(collection.Count > joints.Count)
                collection.RemoveRange(joints.Count, joints.Count - collection.Count);
            else if(collection.Count < joints.Count)
                while(collection.Count < joints.Count)
                    collection.Add(default(T));
        }

        private void m_CopyShallow<T>(List<T> from, List<T> to)
        {
            for(int i = 0; i < from.Count; ++i)
                to[i] = from[i];
        }
        #endregion
    }
}