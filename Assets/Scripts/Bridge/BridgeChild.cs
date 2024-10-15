using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BridgeChild : MonoBehaviour
{
    private int animationStep = 2;
    private Animator anim;
    
    // set the reference to the components 
    void Start()
    {
        anim = GetComponent<Animator>();

        // StartCoroutine(nextStep(2f));
    }

    // play the animation after givent secons
    public IEnumerator nextStep(float time){
        playNextAnimation();
        yield return new WaitForSeconds(time);
        playNextAnimation();
        yield return new WaitForSeconds(time);
        playNextAnimation();
        yield return new WaitForSeconds(time);
        playNextAnimation();
    }

    // play the animation of the given index and increment the animation step by 1
    void playNextAnimation(){
        triggerAnimation(animationStep);
        animationStep += 1;
    }


    // trigger the a given index associated animation
    void triggerAnimation(int index){
        if(transform == null) return;
        switch (index)
        {
            case 2:
                anim.SetTrigger("second");
                break;
            case  3:
                anim.SetTrigger("third");
                break;
            case 4:
                anim.SetTrigger("fourth");
                break;
            default: 
                
                Destroy(transform.parent.gameObject);
                break;
        }
    }
}
