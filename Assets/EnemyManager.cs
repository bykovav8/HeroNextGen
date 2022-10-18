using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EnemyManager : MonoBehaviour
{
    public int defaultPlanes = 10;
    public GameObject plane; //blueprint for planes = prefab
    public int enemyCount;
    public TextMeshProUGUI enemyText;

    // Start is called before the first frame update
    void Start()
    {

        for(int i = 0; i<defaultPlanes; i++)
        {
            createPlane();
        }
        
    }

    // Update is called once per frame
    void Update()
    {
    
    }

    public void createPlane()
    {
        GameObject spawnedPlane = Instantiate(plane);
        spawnedPlane.GetComponent<Enemy>().manager = this;
        float maxX = Camera.main.orthographicSize * Camera.main.aspect * 0.9f;
        float maxY = Camera.main.orthographicSize * 0.9f;

        float planeX = Random.Range(-maxX, maxX);
        float planeY = Random.Range(-maxY, maxY);

        spawnedPlane.transform.position = new Vector3(planeX, planeY, 0f);

    }

    public void enemyDestroy()
    {
        enemyCount++;
        enemyText.text = "ENEMY: Count(10) Destroyed(" + enemyCount + ")";
    }

}
