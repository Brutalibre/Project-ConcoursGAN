using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour 
{
    public static GameController instance;
    public float reloadGameTime = 2f;

    void Awake()
    {
        // Implement singleton pattern
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
    }

    public void PlayerDied()
    {
        StartCoroutine(ReloadLevel());
    }

    public IEnumerator ReloadLevel()
    {
        yield return new WaitForSeconds(reloadGameTime);
        
        Application.LoadLevel(Application.loadedLevel);
    }
}
