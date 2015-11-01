using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameController : MonoBehaviour 
{
    public static GameController instance;
    public float reloadGameTime = 2f;
    
    private Player player;
    private Vector3 checkPoint;
    private ColorCollector colorCollector;
    private GameObject mainCamera;

    void Awake()
    {
        // Implement singleton pattern
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);

        // Set player start position as first check point
        player = GameObject.FindGameObjectWithTag(Tags.Player).GetComponent<Player>();
        SetCheckPoint(player.transform.position);

        // Fetch color collector
        colorCollector = GameObject.FindGameObjectWithTag(Tags.ColorCollector).GetComponent<ColorCollector>();

        // Fetch main camera
        mainCamera = GameObject.FindGameObjectWithTag(Tags.MainCamera);
    }
    

    public void PlayerDied()
    {
        StartCoroutine(ReloadAtCheckPoint());
    }

    public IEnumerator ReloadAtCheckPoint()
    {
        yield return new WaitForSeconds(reloadGameTime);

        colorCollector.CleanUp();

        // Set camera to player's position
        Vector3 position = mainCamera.transform.position;
        position.x = checkPoint.x;
        position.y = checkPoint.y;
        mainCamera.transform.position = position;

        player.Respawn(checkPoint);
    }

    public void SetCheckPoint(Vector3 position)
    {
        checkPoint = position;
    }
}
