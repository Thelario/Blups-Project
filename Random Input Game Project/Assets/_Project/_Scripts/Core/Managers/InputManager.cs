using UnityEngine;

namespace Game.Managers
{
    public class InputManager : Singleton<InputManager>
    {
        public float GetHorizontalInput()
        {
#if UNITY_STANDALONE || UNITY_WEBGL

            return Input.GetAxisRaw("Horizontal");

#endif

#if UNITY_ANDROID

            if (Mathf.Abs(Input.acceleration.x) > 0.1f)
            {
                if (Input.acceleration.x > 0f)
                    return 1f;
                else
                    return -1f;
            }
            else
                return 0f;

#endif
        }

        public float GetVerticalInput()
        {
#if UNITY_STANDALONE || UNITY_WEBGL

            return Input.GetAxisRaw("Vertical");

#endif

#if UNITY_ANDROID

            if (Input.acceleration.z < -0.6f)
                return 1f;
            else if (Input.acceleration.z > -0.4f)
                return -1f;
            else
                return 0f;

#endif
        }
    }
}
