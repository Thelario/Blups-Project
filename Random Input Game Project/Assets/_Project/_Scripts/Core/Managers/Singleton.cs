using UnityEngine;

namespace PabloLario.Managers
{
    public class Singleton<T> : MonoBehaviour where T : Component
    {
        [SerializeField] private bool dontDestroyOnLoad;

        private static T instance;
        public static T Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = FindObjectOfType<T>();
                    if (instance == null)
                    {
                        GameObject obj = new GameObject();
                        obj.name = typeof(T).Name;
                        instance = obj.AddComponent<T>();
                    }
                }
                return instance;
            }
        }

        /* In order to use Awake, do it the next way
        * protected override void Awake()
        * {
        *     base.Awake();
        *     //Your code goes here
        * }
        * */

        protected virtual void Awake()
        {
            if (instance == null)
            {
                instance = this as T;
                
                if (dontDestroyOnLoad)
                    DontDestroyOnLoad(gameObject);
            }
            else if (instance != this as T)
            {
                Destroy(gameObject);
            }
            else 
            { 
                if (dontDestroyOnLoad) 
                    DontDestroyOnLoad(gameObject); 
            }
        }
    }
}