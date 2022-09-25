using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Movement : MonoBehaviour
{
    public Vector3 initialPos {get; private set;}
    public Vector2 initialDir;
    public Vector2 dir {get; private set;}
    public Vector2 nextDir {get; private set;}
    public Rigidbody2D rd {get; private set;}
    public float speed = 5.0f;
    public float speedMultiplier = 1.0f;



    void Awake(){
        this.rd = GetComponent<Rigidbody2D>();
        this.initialPos = this.transform.position;
    }

    void Start(){
    }

    public void SetDir(Vector2 inputDir){
        this.dir = inputDir;
        this.nextDir = Vector2.zero;
    }

    void FixedUpdate(){
        Vector2 translation = this.dir * Time.fixedDeltaTime * this.speed * this.speedMultiplier;
        Vector2 position = this.rd.position;
        this.rd.MovePosition(position+translation);
    }

}
