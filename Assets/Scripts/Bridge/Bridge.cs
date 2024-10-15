using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bridge : MonoBehaviour
{

    [SerializeField] GameObject ExplosionObject;
    [SerializeField] GameObject Trigger; // assuming that the trigger is avaliable and provided from the editor only
    private Transform[] children;
    [SerializeField] private AudioSource explosionAudioSource;
    // set the reference to the player object
    void Start()
    {
        children = new Transform[transform.childCount];

        for (int i = 0; i < children.Length; i++)
        {
            children[i] = transform.GetChild(i);
        }

        
        // StartCoroutine(setExplosionPosition(1.7f));
    }
    // Play the explosion animation of all the points
    public IEnumerator setExplosionPosition(float timeDelay){
        
        for(int i=1; i<children.Length; i++){
            if(children[i] != null){
                if(explosionAudioSource != null) explosionAudioSource.Play();
                createExplosionAtPosition(children[i].gameObject.transform.position);
            }
            yield return new WaitForSeconds(timeDelay);
        }
    }
    // set where to play the explosion animation
    void createExplosionAtPosition(Vector3 position){
        GameObject objectAInstance = Instantiate(ExplosionObject);
        objectAInstance.transform.position = position;
    }
}
