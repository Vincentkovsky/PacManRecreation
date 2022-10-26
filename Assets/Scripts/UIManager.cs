using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class UIManager : MonoBehaviour
{
    // Start is called before the first frame update
    public void LoadFirstLevel(){
        SceneManager.LoadScene(1);
    }
    public void ExitGame(){
        SceneManager.LoadScene(0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
