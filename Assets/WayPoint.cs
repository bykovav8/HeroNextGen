using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//add rigid body
// collider
public class WayPoint : MonoBehaviour
{
    public int health = 100;
    public EnemyManager manager;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown("h"))
        {
            Color fadeColor = GetComponent<SpriteRenderer>().color;
            if(fadeColor.a == 0.0f){
                fadeColor.a = 1.0f;
            }
            else{
                fadeColor.a = 0.0f;
            }
            GetComponent<SpriteRenderer>().color = fadeColor;
            
        }
    }

    void OnTriggerEnter2D(Collider2D egg)
    {
        if(egg.gameObject.CompareTag("Egg"))
        {
            health-=25;
            Color fadeColor = GetComponent<SpriteRenderer>().color;
            fadeColor.a = health * 0.01f;
            GetComponent<SpriteRenderer>().color = fadeColor;

            if(health == 0)
            {
                
                manager.spawnLetter(this);
                //manager.letterDestroy();
                Destroy(this.gameObject);
            }
            
        }
    }
}
