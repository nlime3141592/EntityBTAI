using UnityEngine;

namespace Unchord
{
    public class PlayerInputManager
    {
        public float ix { get; private set; }
        public float iy { get; private set; }
        public bool parryingDown;
        public bool parryingUp;
        public bool jumpDown;
        public bool jumpUp;
        public bool rushDown;
        public bool rushUp;
        public bool active000;
        public bool active001;
        public bool active002;

        public void UpdateInputs(bool bCanInput)
        {
            if(bCanInput)
            {
                ix = Keyboard.GetAxis(
                    Keyboard.GetKeyPress(KeyboardKey.VK_LEFT),
                    Keyboard.GetKeyPress(KeyboardKey.VK_RIGHT)
                );
                iy = Keyboard.GetAxis(
                    Keyboard.GetKeyPress(KeyboardKey.VK_DOWN),
                    Keyboard.GetKeyPress(KeyboardKey.VK_UP)
                );
                // m_player.axisInput.x = Input.GetAxisRaw("Horizontal");
                // m_player.axisInput.y = Input.GetAxisRaw("Vertical");
                parryingDown = Input.GetKeyDown(KeyCode.V);
                parryingUp = Input.GetKeyUp(KeyCode.V);
                jumpDown = Input.GetKeyDown(KeyCode.Space);
                jumpUp = Input.GetKeyUp(KeyCode.Space);
                rushDown = Input.GetKeyDown(KeyCode.LeftShift);
                rushUp = Input.GetKeyUp(KeyCode.LeftShift);
                active000 = Input.GetKeyDown(KeyCode.Z);

                // m_player.skill01 = Input.GetKeyDown(KeyCode.X);
                // m_player.skill02 = Input.GetKeyDown(KeyCode.C);
                active001 = false;
                active002 = false;
            }
            else
            {
                ix = 0;
                iy = 0;
                parryingDown = false;
                jumpDown = false;
                jumpUp = false;
                rushDown = false;
                rushUp = false;
                active000 = false;
                active001 = false;
                active002 = false;
            }
        }
    }
}