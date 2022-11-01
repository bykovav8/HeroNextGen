using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
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

    public Slider slider;
    public float sliderTime = 0f;

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
        if(Input.GetKeyDown("q"))
        {
            Application.Quit();
        }

        if(!shoot)
        {
            sliderTime += Time.deltaTime/0.2f;
            slider.transform.localScale = Vector3.Lerp(new Vector3(1, 2, 0), new Vector3(0, 2, 0), sliderTime);
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

        //slider.transform.localScale = new Vector3(1, 2, 0);

        yield return new WaitForSeconds(.2f);
        sliderTime = 0f;
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
