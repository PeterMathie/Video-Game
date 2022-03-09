using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LazerCollider : MonoBehaviour
{
    void OnCollisionEnter(Collision collisionInfo)
    {
        Debug.Log(collisionInfo.collider.name);
        if(collisionInfo.collider.name == "FirstPersonPlayer")
        {   
           Debug.Log("HIT");
        }
    }
}
