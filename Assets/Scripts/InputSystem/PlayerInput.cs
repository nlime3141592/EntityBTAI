using System.Collections.Generic;
using UnityEngine;

namespace UnchordMetroidvania
{
    public class PlayerInput : MonoBehaviour
    {
        // propertyies
        public float ix
        {
            // m_inputs[0] : Negative X
            // m_inputs[1] : Positive X
            get
            {
                if(m_inputs[0])
                    return m_inputs[1] ? 0 : -1;
                else if(m_inputs[1])
                    return 1;
                else
                    return 0;
            }
        }

        public float iy
        {
            // m_inputs[2] : Negative Y
            // m_inputs[3] : Positive Y
            get
            {
                if(m_inputs[2])
                    return m_inputs[3] ? 0 : -1;
                else if(m_inputs[3])
                    return 1;
                else
                    return 0;
            }
        }

        public bool jumpDown => m_inputs[4];
        public bool jumpUp => m_inputs[5];
        public bool rushDown => m_inputs[6];
        public bool rushUp => m_inputs[7];
        public bool attackOnFloor => m_inputs[8];
        public bool attackOnAir => m_inputs[9];
        public bool abilitySword => m_inputs[10];
        public bool abilityGun => m_inputs[11];

        // variables
        public bool canInput;
        private bool m_bPrevCanInput;

        private bool[] m_inputs;

        private void Start()
        {
            int capacity = 12;
            m_inputs = new bool[capacity];

            m_bPrevCanInput = !canInput;
        }

        private void Update()
        {
            canInput = GameManager.instance.bGameStarted;

            if(canInput)
            {
                m_inputs[0] = Input.GetKey(KeyCode.LeftArrow);
                m_inputs[1] = Input.GetKey(KeyCode.RightArrow);
                m_inputs[2] = Input.GetKey(KeyCode.DownArrow);
                m_inputs[3] = Input.GetKey(KeyCode.UpArrow);
                m_inputs[4] = Input.GetKeyDown(KeyCode.Space);
                m_inputs[5] = Input.GetKeyUp(KeyCode.Space);
                m_inputs[6] = Input.GetKeyDown(KeyCode.LeftShift);
                m_inputs[7] = Input.GetKeyUp(KeyCode.LeftShift);
                m_inputs[8] = Input.GetKeyDown(KeyCode.Z);
                m_inputs[9] = Input.GetKeyDown(KeyCode.Z);
                m_inputs[10] = Input.GetKeyDown(KeyCode.X);
                m_inputs[11] = Input.GetKeyDown(KeyCode.C);
            }
            else if(m_bPrevCanInput)
            {
                for(int i = 0; i < m_inputs.Length; ++i)
                    m_inputs[i] = false;
            }

            m_bPrevCanInput = canInput;
        }
    }
}