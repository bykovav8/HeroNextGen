using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EnemyManager : MonoBehaviour
{
    public int defaultPlanes = 10;
    public GameObject plane; //blueprint for planes
    public int enemyCount;
    public TextMeshProUGUI enemyText;
    public TextMeshProUGUI waypointsText;
    public string mode;
    public GameObject letter; //prefab for letters
    public List<WayPoint> letters;
    public List<Enemy> enemies;
    public bool isRandom = false;

    // Start is called before the first frame update
    void Start(){

        for(int i = 0; i<defaultPlanes; i++)
        {
            spawnPlane();
        }
        
    }

    // Update is called once per frame
    void Update(){
        if (Input.GetKeyDown("j"))
        {
            isRandom = !isRandom;
        }
        if (isRandom)
        {
            mode = "Random";
        }
        else 
        {
            mode = "Sequential";
        }
        waypointsText.text = "WAYPOINTS: (" + mode + ")";
    }

    public void spawnPlane() { // spawningNewEnemies
        GameObject spawnedPlane = Instantiate(plane);
        enemies.Add(spawnedPlane.GetComponent<Enemy>());
        spawnedPlane.GetComponent<Enemy>().manager = this;
        float maxX = Camera.main.orthographicSize * Camera.main.aspect * 0.9f;
        float maxY = Camera.main.orthographicSize * 0.9f;
        float planeX = Random.Range(-maxX, maxX);
        float planeY = Random.Range(-maxY, maxY);
        spawnedPlane.transform.position = new Vector3(planeX, planeY, 0f);
    }

    public void spawnLetter(WayPoint oldletter) {
        int index = letters.IndexOf(oldletter);
        letters.Remove(oldletter);
        Vector3 oldPosition = oldletter.gameObject.transform.position;
        GameObject spawnedLetter = Instantiate(letter);
        letters.Insert(index, spawnedLetter.GetComponent<WayPoint>());
        // setting the sprite of a new generated object (always A) 
        // as the old sprite (whichever other letter we want to generate)
        Sprite oldSprite = oldletter.gameObject.GetComponent<SpriteRenderer>().sprite;
        spawnedLetter.GetComponent<SpriteRenderer>().sprite = oldSprite;
        spawnedLetter.GetComponent<WayPoint>().manager = this;
        float letterX = Random.Range(oldPosition.x - 15, oldPosition.x + 15);
        float letterY = Random.Range(oldPosition.y - 15, oldPosition.y + 15);
        spawnedLetter.transform.position = new Vector3(letterX, letterY, 0f);
        for(int i = 0; i < enemies.Count; i++)
        {
            enemies[i].destroyWaypoint(index);
        }
    }


    public void destroyedEnemyCount(){
        enemyCount++;
        enemyText.text = "ENEMY: Count(10) Destroyed(" + enemyCount + ")";
    }

}
