using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class PacStudentController : MonoBehaviour
{
    public ParticleSystem dust;
    [SerializeField]private GameObject PacStudent;
    Vector2 dir;
    Vector2 nextDir;
    float walkSpeed = 1.5f;
    int[,] LevelMap = LevelGenerator.map;
    private int columns;
    private int rows;
    private int columnIndex;
    private int rowIndex;
    public AudioClip[] audioClips;
    public AudioSource eatPelletSource;
    public AudioSource moveSource;

    void Awake()
    {
        columns = LevelMap.GetLength(1);
        rows = LevelMap.GetLength(0);
        
    }

    void Update()
    {   
        Vector3 position = PacStudent.transform.position;
        columnIndex = ((int)position.x + (columns >> 1) - 1);
        rowIndex = -((int)position.y - (rows >> 1) + 1);
        getInput();
        if(checkWalkable(nextDir)){
            playDust();
            dir = nextDir;
        }
        if(checkWalkable(dir)){
            move();
        }
        rotate();
        AudioControl();
    }

    void rotate(){
        if(dir == Vector2.up){
            PacStudent.transform.rotation = Quaternion.Euler(0,0,90);
        }
        if(dir == Vector2.down){
            PacStudent.transform.rotation = Quaternion.Euler(0,0,-90);
        }
        if(dir == Vector2.left){
            PacStudent.transform.rotation = Quaternion.Euler(0,180,0);
        }
        if(dir == Vector2.right){
            PacStudent.transform.rotation = Quaternion.Euler(0,0,0);
        }
    }

    void move(){
        PacStudent.transform.position = Vector2.Lerp(PacStudent.transform.position, PacStudent.transform.position + new Vector3(dir.x, dir.y, 0), walkSpeed * Time.deltaTime);
    }

    bool checkWalkable(Vector2 direction){
        // if(rowIndex + direction.y < 0 || rowIndex + direction.y >= rows || columnIndex + direction.x < 0 || columnIndex + direction.x >= columns){
        //     return false;
        // }
        switch (direction){
            case Vector2 v when v.Equals(Vector2.left):
                if(LevelMap[rowIndex, columnIndex - 1] == 0 || LevelMap[rowIndex, columnIndex - 1] == 5 || LevelMap[rowIndex, columnIndex - 1] == 6){
                    return true;
                }else{
                    return false;
                }
            case Vector2 v when v.Equals(Vector2.right):
                if(LevelMap[rowIndex, columnIndex + 1] == 0 || LevelMap[rowIndex, columnIndex + 1] == 5 || LevelMap[rowIndex, columnIndex + 1] == 6){
                    return true;
                }else{
                    return false;
                }
            case Vector2 v when v.Equals(Vector2.up):
                if(LevelMap[rowIndex - 1, columnIndex] == 0 || LevelMap[rowIndex - 1, columnIndex] == 5 || LevelMap[rowIndex - 1, columnIndex] == 6){
                    return true;
                }else{
                    return false;
                }
            case Vector2 v when v.Equals(Vector2.down):
                if(LevelMap[rowIndex + 1, columnIndex] == 0 || LevelMap[rowIndex + 1, columnIndex] == 5 || LevelMap[rowIndex + 1, columnIndex] == 6){ 
                    return true;
                }else{
                    return false;
                }
            default:
                return false;
        }
    }

    void setDir(Vector2 direction){
        if(checkWalkable(direction)){
            playDust();
            dir = direction;
            nextDir = Vector2.zero;
        }else{
            nextDir = direction;
        }
    }


    void getInput(){
        if(Input.GetKeyDown(KeyCode.A)){
            setDir(Vector2.left);
        }else if(Input.GetKeyDown(KeyCode.D)){
            setDir(Vector2.right);
        }else if(Input.GetKeyDown(KeyCode.W)){
            setDir(Vector2.up);
        }else if(Input.GetKeyDown(KeyCode.S)){
            setDir(Vector2.down);
        }
    }

    void AudioControl(){
        if(dir != Vector2.zero){
            switch (dir){
                case Vector2 v when v.Equals(Vector2.left):
                    switch (LevelMap[rowIndex, columnIndex - 1]){
                        case 5:
                            eatPelletSource.PlayOneShot(audioClips[0]);
                            break;
                        case 6:
                            eatPelletSource.PlayOneShot(audioClips[1]);
                            break;
                        case 0:
                            moveSource.PlayOneShot(audioClips[2]);
                            break;
                        default:
                            break;
                    }
                    break;
                case Vector2 v when v.Equals(Vector2.right):
                    switch (LevelMap[rowIndex, columnIndex + 1]){
                        case 5:
                            eatPelletSource.PlayOneShot(audioClips[0]);
                            break;
                        case 6:
                            eatPelletSource.PlayOneShot(audioClips[1]);
                            break;
                        case 0:
                            moveSource.PlayOneShot(audioClips[2]);
                            break;
                        default:
                            break;
                    }
                    break;
                case Vector2 v when v.Equals(Vector2.up):
                    switch (LevelMap[rowIndex - 1, columnIndex]){
                        case 5:
                            eatPelletSource.PlayOneShot(audioClips[0]);
                            break;
                        case 6:
                            eatPelletSource.PlayOneShot(audioClips[1]);
                            break;
                        case 0:
                            moveSource.PlayOneShot(audioClips[2]);
                            break;
                        default:
                            break;
                    }
                    break;
                case Vector2 v when v.Equals(Vector2.down):
                    switch (LevelMap[rowIndex + 1, columnIndex]){
                        case 5:
                            eatPelletSource.PlayOneShot(audioClips[0]);
                            break;
                        case 6:
                            eatPelletSource.PlayOneShot(audioClips[1]);
                            break;
                        case 0:
                            moveSource.PlayOneShot(audioClips[2]);
                            break;
                        default:
                            break;
                    }
                    break;
                default:
                    break;
            }
        }
    }

    void playDust(){
        dust.Play();
    }
}