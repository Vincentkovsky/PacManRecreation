using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Movement))]
public class PacBot : MonoBehaviour
{
    public Movement move{get;private set;}
    public GenericAnimator anim;

    void Awake()
    {
        this.move = GetComponent<Movement>();
        this.anim = GetComponent<GenericAnimator>();
    }

    // Update is called once per frame
    void Update()
    {
        demoMove();
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
            // this.die();
        }

        float rotationAngle = Mathf.Atan2(this.move.dir.y, this.move.dir.x);
        if(rotationAngle*Mathf.Rad2Deg == 180.0f){
            this.transform.rotation = Quaternion.Euler(new Vector3(0.0f, 180.0f, 0.0f));
        }else{
            this.transform.rotation = Quaternion.Euler(new Vector3(0.0f, 0.0f, rotationAngle*Mathf.Rad2Deg));
        }
    }

    public void die(){
        this.anim.sprites = this.GetComponent<GenericAnimator>().deathSprites;
        this.anim.loop = false;
    }

    void demoMove(){
        if(this.transform.position == new Vector3(-5,5,0)){
            this.move.SetDir(Vector2.right);
        }
        if(this.transform.position == new Vector3(6,5,0)){
            this.move.SetDir(Vector2.down);
        }
        if(this.transform.position == new Vector3(6,1,0)){
            this.move.SetDir(Vector2.left);
        }
        if(this.transform.position == new Vector3(-5,1,0)){
            this.move.SetDir(Vector2.up);
        }
    }
}
