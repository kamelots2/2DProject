using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemFB : MonoBehaviour
{
    private void OnAnimatorEnd()
    {
        Destroy(gameObject);
    }
}
