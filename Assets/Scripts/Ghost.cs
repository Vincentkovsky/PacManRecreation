using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ghost : MonoBehaviour
{

    public GenericAnimator anim;
    // Start is called before the first frame update
    void Start()
    {   
        anim = GetComponent<GenericAnimator>();
        StartCoroutine(demoAnimations());
    }

    IEnumerator demoAnimations(){
        yield return new WaitForSeconds(3.0f);
        this.transform.rotation = Quaternion.Euler(new Vector3(0.0f, 0.0f, 90.0f));
        yield return new WaitForSeconds(3.0f);
        this.transform.rotation = Quaternion.Euler(new Vector3(0.0f, 180.0f, 0.0f));
        yield return new WaitForSeconds(3.0f);
        this.transform.rotation = Quaternion.Euler(new Vector3(0.0f, 0.0f, -90.0f));
        yield return new WaitForSeconds(1.0f);
        this.transform.rotation = Quaternion.Euler(new Vector3(0.0f, 0.0f, 0.0f));
        yield return new WaitForSeconds(1.0f);
        this.anim.sprites = this.anim.scaredSprites;
        yield return new WaitForSeconds(3.0f);
        this.anim.sprites = this.anim.recoveringSprites;
        yield return new WaitForSeconds(3.0f);
        this.anim.sprites = this.anim.deathSprites;
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
