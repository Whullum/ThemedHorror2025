using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadBanks : MonoBehaviour
{
    private void Awake()
    {
        FMODUnity.RuntimeManager.LoadBank("Main-Music", true);
        FMODUnity.RuntimeManager.LoadBank("Main-Music.strings", true);
        FMODUnity.RuntimeManager.LoadBank("sfx", true);

        if (FMODUnity.RuntimeManager.HasBankLoaded("Main-Music") &&
    FMODUnity.RuntimeManager.HasBankLoaded("Main-Music.strings") &&
    FMODUnity.RuntimeManager.HasBankLoaded("sfx"))
        {
            SceneManager.LoadScene("MainLevel");
        }
    }
}
