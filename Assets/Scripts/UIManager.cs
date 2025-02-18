using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get { return instance; } }

    private static UIManager instance;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
    }


}
