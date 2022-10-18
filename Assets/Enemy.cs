using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//add rigid body
// collider
public class Enemy : MonoBehaviour
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
                Destroy(this.gameObject);
                manager.createPlane();
                manager.enemyDestroy();
            }
            
        }
        else if(egg.gameObject.CompareTag("Player"))
        {
            manager.createPlane();
            Destroy(this.gameObject);
            manager.enemyDestroy();
        }
        else 
        {
            manager.createPlane();
            Destroy(this.gameObject);
        }
    }
}
