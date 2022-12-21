using UnityEngine;


namespace Utils
{
    public abstract class MonoSingleton<T>: MonoBehaviour where T: MonoBehaviour
    {
        public static T Instance { get; private set; }

        protected virtual void Awake() => Instance = this as T;

        protected virtual void OnApplicationQuit() {
            Instance = null;
            Destroy(gameObject);
        }
    }

    public abstract class PersistentMonoSingleton<T> : MonoSingleton<T> where T : MonoBehaviour
    {
        protected override void Awake()
        {
            if (Instance != null)
            {
                Destroy(gameObject);
                return;
            }

            DontDestroyOnLoad(gameObject);
            base.Awake();
        }
    }
}