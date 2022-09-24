using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Movement))]
public class PacBot : MonoBehaviour
{
    public Movement move{get;private set;}
    public GenericAnimator animation;

    void Awake()
    {
        this.move = GetComponent<Movement>();
        this.animation = GetComponent<GenericAnimator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.A)){
            this.move.SetDir(Vector2.left);
        }
        if(Input.GetKeyDown(KeyCode.W)){
            this.move.SetDir(Vector2.up);
        }
        if(Input.GetKeyDown(KeyCode.S)){
            this.move.SetDir(Vector2.down);
        }
        if(Input.GetKeyDown(KeyCode.D)){
            this.move.SetDir(Vector2.right);
            this.die();
        }


        float rotationAngle = Mathf.Atan2(this.move.dir.y, this.move.dir.x);
        if(rotationAngle*Mathf.Rad2Deg == 180.0f){
            this.transform.rotation = Quaternion.Euler(new Vector3(0.0f, 180.0f, 0.0f));
        }else{
            this.transform.rotation = Quaternion.Euler(new Vector3(0.0f, 0.0f, rotationAngle*Mathf.Rad2Deg));
        }
    }

    public void die(){
        this.animation.sprites = this.animation.deathSprites;
        this.animation.loop = false;
    }
}
