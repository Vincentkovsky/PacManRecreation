using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class GenericAnimator : MonoBehaviour
{
    public SpriteRenderer sr {get; private set;}

    public Sprite[] sprites;
    public Sprite[] deathSprites;
    public Sprite[] scaredSprites;
    public Sprite[] recoveringSprites;

    public float time = 0.25f;
    public int frame{get;private set;}
    public bool loop = true;

    void Start() {
        InvokeRepeating(nameof(Animate), this.time, this.time);
    }

    void Animate(){
        if(!this.sr.enabled){
            return;
        }
        this.frame++;
        if(this.loop && this.frame >= this.sprites.Length){
            this.frame = 0;
        }
        if(this.frame >= 0 && this.frame < this.sprites.Length){
            this.sr.sprite = this.sprites[this.frame];
        }
    }

    void Awake()
    {
        this.sr = GetComponent<SpriteRenderer>();
    }

    void Restart(){
        this.frame = -1;
        Animate();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
