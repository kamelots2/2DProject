using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gem : MonoBehaviour
{
    public GameObject gameObj;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject.Find("GameMgr").GetComponent<GameManager>().UpdateGem(1);
        Destroy(gameObject);
        Instantiate(gameObj, transform.position, transform.rotation);
    }
}
