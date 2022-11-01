using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// add rigid body
// collider
public class Enemy : MonoBehaviour
{
    public int health = 100;
    public EnemyManager manager;
    private int index = 0;
    private Vector3 positionWaypoint; 
    private float speed = 20f;
    private float rotateSpeed = 20f;

    // Start is called before the first frame update
    void Start()
    {
        // got the position of the next way point to go to 
        positionWaypoint = manager.letters[index].gameObject.transform.position;

    }

    // Update is called once per frame
    void Update(){
        // speed += Input.GetAxis("Vertical");
        transform.position += transform.up * speed * Time.smoothDeltaTime; 
        Vector3 curPosition = transform.position;

        Vector3 relativePosition = positionWaypoint - curPosition;
        Quaternion lookRotation = Quaternion.LookRotation(Vector3.forward, relativePosition);
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, rotateSpeed * Time.deltaTime);
        if(relativePosition.magnitude < 20)
        {
            findNextWaypoint();
        }

    }

    private void findNextWaypoint(){
        if(manager.isRandom)
        {
            index = Random.Range(0, manager.letters.Count - 1);
        }
        else
        {
            if(index < manager.letters.Count - 1)
            {
                index = index + 1;
            }
            else 
            {
                index = 0;
            }
        }
        positionWaypoint = manager.letters[index].gameObject.transform.position;
    }

    public void destroyWaypoint(int indexOfLetter)
    {
        if(indexOfLetter == index)
        {
            positionWaypoint = manager.letters[index].gameObject.transform.position;
        }
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.gameObject.CompareTag("Egg"))
        {
            health-=25;
            Color fadeColor = GetComponent<SpriteRenderer>().color;
            fadeColor.a = health * 0.01f;
            GetComponent<SpriteRenderer>().color = fadeColor;

            if(health == 0)
            {
                Destroy(this.gameObject);
                manager.spawnPlane();
                manager.destroyedEnemyCount();
            }
            
        }
        else if(collider.gameObject.CompareTag("Player"))
        {
            manager.spawnPlane();
            Destroy(this.gameObject);
            manager.destroyedEnemyCount();
        }
        else if(!collider.gameObject.CompareTag("Enemy") && !collider.gameObject.CompareTag("WayPoint"))
        {
            Destroy(this.gameObject);
            manager.spawnPlane();
        }

    }

}
