using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEditor.SceneManagement;

public class GameManager : MonoSingleton<GameManager>
{
    // Start is called before the first frame update
    public Text textGem;
    public TMP_Text textCherry;
    public Canvas canvas;
    private int iGem = 0;
    private int iCherry = 1;
    public GameObject[]   image;
    void Start()
    {
        textGem.text = this.iGem.ToString();
        UpdateCherry();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateGem(int iGem)
    {
        this.iGem += iGem;
        textGem.text = this.iGem.ToString();
    }

    public void UpdateCherry(int iCherry = 0)
    {
        this.iCherry += iCherry;
        if(this.iCherry > 3)
        {
            this.iCherry = 3;
        }
        if (this.iCherry <= 0)
        {
            this.iCherry = 0;
        #if UNITY_EDITOR
            EditorSceneManager.LoadScene("Main");
        #endif
            
        }
        for (int i=2;i>=0;i--)
        {
            if(i+1> this.iCherry)
            {
                image[i].SetActive(false);
            }
            else
            {
                image[i].SetActive(true);
            }
        }
        //textCherry.text = this.iCherry.ToString();
        
    }
}
