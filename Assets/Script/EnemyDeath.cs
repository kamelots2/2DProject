using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDeath : MonoBehaviour
{
    private void OnAnimatorEnd()
    {
        Destroy(gameObject);
    }
}
