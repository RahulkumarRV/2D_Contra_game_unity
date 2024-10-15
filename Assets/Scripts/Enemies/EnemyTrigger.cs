using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTrigger : MonoBehaviour
{
    private Transform[] enemies;
    // Start is called before the first frame update
    void Start()
    {
        int childCount = transform.childCount;

        enemies = new Transform[childCount];
        for(int i=0; i<childCount; i++){
            enemies[i] = transform.GetChild(i);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D other){
        if(other.gameObject.tag == "Player"){
            StartCoroutine(triggerEnemies());
        }
    }

    IEnumerator triggerEnemies(){
        for(int i=0; i<enemies.Length; i++){
            yield return new WaitForSeconds(1f);
            if(enemies[i] != null) enemies[i].gameObject.SetActive(true);
        }
    }
}
