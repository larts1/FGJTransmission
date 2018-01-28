using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Invisibility", menuName = "Effects/Invisibility", order = 1)]
public class InvisibilityEffect : BoostEffect
{
    public GameObject Notification;

    GameObject win;
    GameObject shipRender;
    GameObject[] antenna;
    public override void EndEffect()
    {
        shipRender = GameObject.Find("shipRender");
        shipRender.GetComponent<Renderer>().enabled = true;
        antenna = GameObject.FindGameObjectsWithTag("Antenna");
        for (int i = 0; i < antenna.Length; i++)
        {
            antenna[i].GetComponent<Renderer>().enabled = true;
        }

        if ( win != null ) {
            Destroy( win );
        }

    }
    public override void StartEffect()
    {
        shipRender = GameObject.Find("shipRender");
        shipRender.GetComponent<Renderer>().enabled = false;
        antenna = GameObject.FindGameObjectsWithTag("Antenna");
        for(int i=0; i< antenna.Length; i++)
        {
            antenna[i].GetComponent<Renderer>().enabled = false;
        }
        // antenna.SetActive(false);

        win = Instantiate( Notification );
    }
}
