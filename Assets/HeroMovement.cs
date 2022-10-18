using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HeroMovement : MonoBehaviour
{
    public float speed = 20f;
    public float rotateSpeed = 20f;
    // control mode
    private bool mode = false;
    public GameObject prefab;
    private bool shoot = true; 

    public TextMeshProUGUI heroText;
    public string driveMode;

    public TextMeshProUGUI eggText;
    public int eggsOnScreen;

    public TextMeshProUGUI touchedEnemyText;
    public int touchedEnemyCount;


    // Start is called before the first frame update
    void Start()
    {
        
    }
    // Update is called once per frame
    void Update() // called around 60 times a second
    {
        
        // mouse control   
        if(Input.GetKeyDown("m"))
        {
            mode = !mode;
        }

        if(mode) //mouse
        {
            Vector3 p = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            p.z = 0f; 
            transform.position = p;
            driveMode = "Mouse";
            heroText.text = "HERO: Drive(" + driveMode + ")";

        }
        else // keyboard - w/s
        {
            speed += Input.GetAxis("Vertical");
            transform.position += transform.up * speed * Time.smoothDeltaTime;
            driveMode = "Keyboard";
            heroText.text = "HERO: Drive(" + driveMode + ")";
        }
        transform.Rotate(Vector3.forward, -1f * Input.GetAxis("Horizontal") * (rotateSpeed * Time.smoothDeltaTime));


        if(Input.GetKey(KeyCode.Space))
        {
            if(shoot)
            {
                StartCoroutine(SpawnPrefab());
            }
        }
        if(Input.GetKey("q"))
        {
            Application.Quit();
        }
    }
    // shooting eggs
    protected IEnumerator SpawnPrefab()
    {
        shoot = false;
        GameObject spawnedEgg = Instantiate(prefab);
        spawnedEgg.transform.up = this.transform.up;
        spawnedEgg.transform.position = this.transform.position;

        spawnedEgg.GetComponent<EggBehavior>().eggCount = this;
        eggsOnScreen++;
        eggText.text = "EGG OnScreen(" + eggsOnScreen + ")";
        yield return new WaitForSeconds(.2f);
        shoot = true;
    }

    // count eggs on screen, decrease their number
    public void eggCount()
    {
        eggsOnScreen--;
        eggText.text = "EGG OnScreen(" + eggsOnScreen + ")";
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.gameObject.CompareTag("Enemy"))
        {
           touchedEnemyCount++;
           touchedEnemyText.text = "TouchedEnemy(" + touchedEnemyCount + ")";
        }
    }
}
