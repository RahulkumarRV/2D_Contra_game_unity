using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// controlle the fire effect and destroy the enemy if playe toches
public class Fire : MonoBehaviour
{
    private Animator anim;
    /*
    * timeBtnopen mean time taken between two open animations 
    * timeBtnclose means time taken between two close animations
    */
    [SerializeField] private float timeBtwOpen = 2f, timeBtnClose = 1f;
    [SerializeField] private AudioSource fireAudioSource;
    private float timeBtnAnimation;
    private GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        timeBtnAnimation = timeBtwOpen;
        player = GameObject.Find("player");
    }

    // Update is called once per frame
    void Update()
    {
        if(anim == null || player == null) return;
        if(timeBtnAnimation <= 0  ){
            StartCoroutine(fireControle());
            timeBtnAnimation = timeBtwOpen;
        }else{
            timeBtnAnimation -= Time.deltaTime;
        }
      
    }

    IEnumerator fireControle(){
        // yield return new WaitForSeconds(timeBtwOpen);
        if(fireAudioSource != null) {
            fireAudioSource.Play();
        }
        anim.SetTrigger("openfire");
        yield return new WaitForSeconds(timeBtnClose);
        anim.SetTrigger("closefire");
    }
}
