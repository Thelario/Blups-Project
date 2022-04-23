using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Managers
{
    [System.Serializable]
    public class Secret
    {
        public string name;
        public bool found;
        public string content;
    }

    public class SecretsManager : Singleton<SecretsManager>
    {
        public List<Secret> secrets;
    }
}
