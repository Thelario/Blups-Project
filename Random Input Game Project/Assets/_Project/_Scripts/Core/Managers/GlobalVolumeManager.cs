using Game.Managers;

public class GlobalVolumeManager : Singleton<GlobalVolumeManager>
{
    protected override void Awake()
    {
        base.Awake();
            
        #if UNITY_ANDROID
        Destroy(gameObject);
        #endif
    }
}
