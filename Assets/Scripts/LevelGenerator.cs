using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
   private int[,] map = {
            {1,2,2,2,2,2,2,2,2,2,2,2,2,7}, 
            {2,5,5,5,5,5,5,5,5,5,5,5,5,4}, 
            {2,5,3,4,4,3,5,3,4,4,4,3,5,4}, 
            {2,6,4,0,0,4,5,4,0,0,0,4,5,4}, 
            {2,5,3,4,4,3,5,3,4,4,4,3,5,3}, 
            {2,5,5,5,5,5,5,5,5,5,5,5,5,5}, 
            {2,5,3,4,4,3,5,3,3,5,3,4,4,4}, 
            {2,5,3,4,4,3,5,4,4,5,3,4,4,3}, 
            {2,5,5,5,5,5,5,4,4,5,5,5,5,4}, 
            {1,2,2,2,2,3,5,4,3,4,4,3,0,4}, 
            {0,0,0,0,0,2,5,4,3,4,4,3,0,3}, 
            {0,0,0,0,0,2,5,4,4,0,0,0,0,0}, 
            {0,0,0,0,0,2,5,4,4,0,3,4,4,0}, 
            {2,2,2,2,2,3,5,3,3,0,4,0,0,0}, 
            {0,0,0,0,0,0,5,0,0,0,4,0,0,0}, 
        };

    [SerializeField] private GameObject topLeftQuadrant;
    [SerializeField] private GameObject insideWall;
    [SerializeField] private GameObject insideCorner;
    [SerializeField] private GameObject outsideWall;
    [SerializeField] private GameObject outsideCorner;
    [SerializeField] private GameObject junctionWall;
    [SerializeField] private GameObject cherry;
    [SerializeField] private GameObject pellet;
    [SerializeField] private GameObject powerPellet;

    public int columns;
    public int rows;
    private float offset;

    public GameObject level01;




    void Awake() {
        level01 = GameObject.Find("Wall");
        Destroy(level01);
        offset = 24/insideCorner.GetComponent<SpriteRenderer>().sprite.pixelsPerUnit;
        columns = map.GetLength(1);
        rows = map.GetLength(0);
        Camera camera = Camera.main;
        camera.transform.position = new Vector3(((float)columns + 1.0f)/2, ((float)-rows - 1.0f)/2, -2);
        camera.orthographicSize = 15.0f;
    }

    void Start()
    {
        Initialize();
        GameObject topRightQuadrant = Instantiate(topLeftQuadrant, Vector3.zero, Quaternion.Euler(new Vector3(0.0f, 180.0f, 0.0f)), this.transform);
        topRightQuadrant.transform.position = new Vector3(columns+1, 0, 0);
        GameObject bottomLeftQuadrant = Instantiate(topLeftQuadrant, Vector3.zero, Quaternion.Euler(new Vector3(180.0f, 0.0f, 0.0f)), this.transform);
        bottomLeftQuadrant.transform.position = new Vector3(0, -rows-1, 0);
        GameObject bottomRightQuadrant = Instantiate(topLeftQuadrant, Vector3.zero, Quaternion.Euler(new Vector3(0.0f, 0.0f, 180.0f)), this.transform);
        bottomRightQuadrant.transform.position = new Vector3(columns+1, -rows-1, 0);
    }

    private void Initialize(){
        GameObject PacBot = GameObject.Find("PacBot");
        for(int i = 0; i < rows; i++){
            for(int j = 0; j < columns; j++){
                GameObject ele = null;
                Vector3 position = new Vector3((j-(columns >> 1)+1) * offset, -(i-(rows >> 1)+1) * offset, 0);
                switch (map[i,j])
                {
                    case 1:
                        ele = Instantiate(outsideCorner, position, Quaternion.identity, this.topLeftQuadrant.transform);
                        if(j == 0 && i == 0){
                            ele.transform.rotation = Quaternion.Euler(new Vector3(0.0f, 180.0f, 0.0f));
                        }
                        if(i > 0 && i < rows && map[i-1, j] == 2){
                            ele.transform.rotation = Quaternion.Euler(new Vector3(180.0f, 180.0f, 0.0f));
                        } 
                        
                        break;
                    case 2:
                        ele = Instantiate(outsideWall, position, Quaternion.identity, this.topLeftQuadrant.transform);
                        if(i > 0 && i < rows && map[i-1,j] != 0 && j == 0){
                            ele.transform.rotation = Quaternion.Euler(new Vector3(0.0f, 0.0f, 90.0f));
                        }
                        if(i >0 && i < rows && (map[i-1,j] == 5 || map[i-1,j] == 6)){
                            ele.transform.rotation = Quaternion.Euler(new Vector3(180.0f, 0.0f, 0.0f));
                        }
                        if(i >0 && i < rows && j >0 && map[i, j-1] == 0){
                            ele.transform.rotation = Quaternion.Euler(new Vector3(0.0f, 0.0f, 90.0f));
                        }
                        
                        break;
                    case 3:
                        ele = Instantiate(insideCorner, position, Quaternion.identity, this.topLeftQuadrant.transform);
                        if(i > 0 && i < rows && map[i-1,j] == 2 && map[i,j-1] == 2){
                            ele.transform.rotation = Quaternion.Euler(new Vector3(0.0f, 0.0f, 180.0f));
                        }else if(i > 0 && i < rows && map[i,j-1] == 2){
                            ele.transform.rotation = Quaternion.Euler(new Vector3(0.0f, 180.0f, 0.0f));
                        }
                        if(i < rows-1 && j < columns-1 && (map[i+1,j] == 4 || map[i+1,j] == 3) && (map[i,j+1] ==4 || map[i,j+1] ==3)){
                            ele.transform.rotation = Quaternion.Euler(Vector3.zero);
                        }else if(i >0 && j < columns-1 && (map[i-1,j] == 4 || map[i-1,j] == 3) && (map[i,j+1] ==4 || map[i,j+1] ==3)){
                            ele.transform.rotation = Quaternion.Euler(new Vector3(180.0f, 0.0f, 0.0f));
                        }else if(i < rows-1 && j >0 && (map[i+1,j] == 4 || map[i+1,j] == 3) && (map[i,j-1] ==4 || map[i,j-1] ==3)){
                            ele.transform.rotation = Quaternion.Euler(new Vector3(0.0f, 0.0f, -90.0f));
                        }else if(i > 0 && j > 0 && (map[i-1,j] == 4 || map[i-1,j] == 3) && (map[i,j-1] ==4  || map[i,j-1] ==3)){
                            ele.transform.rotation = Quaternion.Euler(new Vector3(0.0f, 0.0f, 180.0f));
                        }
                        if(i > 0 && i < rows -1 && j < columns-1 && map[i-1,j] ==4 && map[i,j+1]==4 && map[i+1,j] == 3){
                            ele.transform.rotation = Quaternion.Euler(new Vector3(180.0f, 0.0f, 0.0f));
                        } 
                        if(j == columns -1 && map[i-1,j] == 4){
                            ele.transform.rotation = Quaternion.Euler(new Vector3(0.0f, 0.0f, 90.0f));
                        }
                        if(i < rows-1 && j == columns -1 && map[i+1,j] == 4 && map[i,j-1] ==4){
                            ele.transform.rotation = Quaternion.Euler(new Vector3(0.0f, 180.0f, 0.0f));
                        }
                        break;
                    case 4:
                        ele = Instantiate(insideWall, position, Quaternion.identity, this.topLeftQuadrant.transform);
                        if(j == columns-1 && (map[i-1,j] == 7 || map[i-1,j] == 3 || map[i-1,j] == 4)){
                            ele.transform.rotation = Quaternion.Euler(new Vector3(0.0f,0.0f,-90.0f));
                        }
                        if(j > 0 && j < columns && (map[i-1,j] == 5 || map[i-1,j] == 6 )){
                            ele.transform.rotation = Quaternion.Euler(new Vector3(180.0f, 0.0f, 0.0f));
                        }
                        if(map[i-1,j] == 0 && map[i+1,j] == 0){
                            ele.transform.rotation = Quaternion.Euler(new Vector3(180.0f, 0.0f, 0.0f));
                        }
                        if(j < columns-1 && (map[i-1,j] == 3 || map[i-1,j] == 4) && (map[i,j+1] == 0 || map[i,j+1]== 5 || map[i,j+1] ==6)){
                            ele.transform.rotation = Quaternion.Euler(new Vector3(0.0f,0.0f,-90.0f));
                        }else if(j < columns-1 && (map[i-1,j] == 3 || map[i-1,j] == 4) && (map[i,j-1] == 0 || map[i,j-1]== 5 || map[i,j-1] ==6)){
                            ele.transform.rotation = Quaternion.Euler(new Vector3(0.0f,0.0f,90.0f));
                        }
                        break;
                    case 5:
                        if(position != PacBot.transform.position){
                            ele = Instantiate(pellet, position, Quaternion.identity, this.topLeftQuadrant.transform);
                        }
                        break;
                    case 6:
                        ele = Instantiate(powerPellet, position, Quaternion.identity, this.topLeftQuadrant.transform);
                        break;
                    case 7:
                        ele = Instantiate(junctionWall, position, Quaternion.identity, this.topLeftQuadrant.transform);
                        break;


                }
            }
        }

    }
}
