using UnityEngine;

namespace UnchordMetroidvania
{
    public class PlayerInputManager
    {
        private Player m_player;

        public PlayerInputManager(Player player)
        {
            m_player = player;
        }

        public void UpdateInputs(bool bCanInput)
        {
            if(bCanInput)
            {
                m_player.axisInput.x = Keyboard.GetAxis(
                    Keyboard.GetKeyPress(KeyboardKey.VK_LEFT),
                    Keyboard.GetKeyPress(KeyboardKey.VK_RIGHT)
                );
                m_player.axisInput.y = Keyboard.GetAxis(
                    Keyboard.GetKeyPress(KeyboardKey.VK_DOWN),
                    Keyboard.GetKeyPress(KeyboardKey.VK_UP)
                );
                // m_player.axisInput.x = Input.GetAxisRaw("Horizontal");
                // m_player.axisInput.y = Input.GetAxisRaw("Vertical");
                m_player.parryingDown = Input.GetKeyDown(KeyCode.V);
                m_player.parryingUp = Input.GetKeyUp(KeyCode.V);
                m_player.jumpDown = Input.GetKeyDown(KeyCode.Space);
                m_player.jumpUp = Input.GetKeyUp(KeyCode.Space);
                m_player.rushDown = Input.GetKeyDown(KeyCode.LeftShift);
                m_player.rushUp = Input.GetKeyUp(KeyCode.LeftShift);
                m_player.skill00 = Input.GetKeyDown(KeyCode.Z);
                m_player.skill01 = Input.GetKeyDown(KeyCode.X);
                m_player.skill02 = Input.GetKeyDown(KeyCode.C);
            }
            else
            {
                m_player.axisInput.x = 0;
                m_player.axisInput.y = 0;
                m_player.parryingDown = false;
                m_player.jumpDown = false;
                m_player.jumpUp = false;
                m_player.rushDown = false;
                m_player.rushUp = false;
                m_player.skill00 = false;
                m_player.skill01 = false;
                m_player.skill02 = false;
            }
        }
    }
}