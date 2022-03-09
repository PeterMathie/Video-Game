using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LazerEyes : MonoBehaviour
{
    public float shootRate;
    private float m_shootRateTimeStamp;

    public GameObject m_shotPrefab;
    RaycastHit hit;
    public Camera cam;

    //distance lazer can travel
    float range = 1000.0f;

    void Update()
    {
        //on mouse1 click
        if (Input.GetMouseButton(0))
        {
            //can only shoot the once the cooldown is complete
            if (Time.time > m_shootRateTimeStamp)
            {
                //shoot the lazer
                shootRay();
                m_shootRateTimeStamp = Time.time + shootRate;
            }
        }
    }

    void shootRay()
    {   
        
        //define the lazer as a ray -> moves in a straight line
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        //if lazer collids with a game object it will shoot (a box surrounds the island)
        if (Physics.Raycast(ray, out hit, range))
        {
            //instantiate the laser 
            GameObject laser = GameObject.Instantiate(m_shotPrefab, transform.position, transform.rotation) as GameObject;
            //fucntionality defined in shotBehaviour script
            laser.GetComponent<ShotBehavior>().setTarget(hit.point);

            //remove lazer if it hits nothing
            GameObject.Destroy(laser, 2f);
        }
    }


}
