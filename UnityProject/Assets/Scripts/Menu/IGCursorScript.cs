using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class IGCursorScript : MonoBehaviour {

    private int pos;
    public Text[] menuText = new Text[3];
    public AudioSource loadSound;
    public AudioSource clicSound;
    public Color col;

    private bool fadeOut = false;
    private bool fadeIn = true;
    public SceneFadeInOut fadeScript;

    public Player player;

    // Use this for initialization
    void Start () {

        //Cursor.visible = false;
        
        pos = 0;
    }

    // Update is called once per frame
    void Update () {

        if (!fadeOut && !fadeIn) {
            if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                if (pos < 2)
                    pos++;

                else
                    pos = 0;

                TextColor();
                clicSound.Play();
            }

            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                if (pos > 0)
                    pos--;

                else
                    pos = 2;

                TextColor();
                clicSound.Play();
            }

            if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.Space))
            {
                loadSound.Play();

                switch (pos)
                {
                    // Retour au jeu
                    case 0:
                        player.menuOpen = false;
                        player.canMove = true;
                        player.canJump = true;
                        GameObject.Find("Menu").SetActive(false);
                        break;

                    // Dernier Checkpoint
                    case 1:
                        GameObject.Find("Menu").SetActive(false);
                        player.Die();
                        break;

                    case 2:
                        fadeOut = true;
                        break;
                }
            }
        }

        else if(fadeOut == true)
        {
            if (fadeScript.FadeToBlack())
            {
                // Retour au menu
                Application.LoadLevel("Main Menu");
            }
            
        }

        else
        {
            if (fadeScript.FadeToClear())
                fadeIn = false;
        }
    }

    public void TextColor()
    {

        for(int i=0; i< 3; i++)
        {
            if(i== pos)
            {
                menuText[i].color = GenerateColor();
            }

            else
                menuText[i].color = new Color(0.196f,0.196f,0.196f);

        }
    }

    public Color GenerateColor()
    {
        int rand = Random.Range(0, 6);
        col = Color.gray;

        switch (rand) {
            // red
            case 0:
                col = new Color(0.917f,0,0);
                break;

            //orange
            case 1:
                col = new Color(1, 0.474f, 0);
                break;

            //yellow
            case 2:
                col = new Color(1, 0.807f, 0);
                break;

            //green
            case 3:
                col = new Color(0.227f, 0.807f, 0);
                break;

            //blue
            case 4:
                col = new Color(0, 0.494f, 0.905f);
                break;

            //violet
            case 5:
                col = new Color(0.545f, 0.298f, 0.698f);
                break;
        }

        return col;

        /*Vector3 vec = Random.insideUnitSphere/2;
        return new Color(vec.x + .5f, vec.y + .5f, vec.z + .5f);*/
    }
    
}
