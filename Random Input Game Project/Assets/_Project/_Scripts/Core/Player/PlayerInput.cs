using UnityEngine;
using UnityEngine.InputSystem;

namespace Game
{
    public class PlayerInput : MonoBehaviour
    {
        void Update()
        {
            Keyboard keyboard = Keyboard.current;

            if (keyboard == null)
            {
                print("Keyboard not detected.");
                return;
            }

            if (keyboard.aKey.isPressed)
            {
                print("A pressed");
            }

            if (keyboard.dKey.isPressed)
            {
                print("D pressed");
            }
        }
    }
}
