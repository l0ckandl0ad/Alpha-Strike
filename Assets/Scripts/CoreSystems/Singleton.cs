using UnityEngine;
using UnityEngine.SceneManagement;

public class Singleton<T> : MonoBehaviour where T : Singleton<T>
{
    public static T Instance { get; private set; }

    protected virtual void Awake()
    {
        if (Instance == null)
        {
            Instance = (T)this;

            Debug.Log(this + ": Awaking Singleton<" + GetType().ToString()+ "> in scene "
                + SceneManager.GetActiveScene().name);
        }
        else
        {
            Debug.LogError(this + ": Error trying to instatiate a Singleton<" + GetType().ToString() 
                + "> when another instance already exists. Destroying " + gameObject.name + "...");
            Destroy(gameObject);
        }

    }

    protected virtual void OnDestroy()
    {
        if (Instance == this)
        {
            Instance = null;

            Debug.Log(this + ": Destroying Singleton<"+ GetType().ToString() + "> instance in active scene " 
                + SceneManager.GetActiveScene().name);
        }
    }


}
