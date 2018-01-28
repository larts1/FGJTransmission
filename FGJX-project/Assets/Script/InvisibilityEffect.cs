using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Invisibility", menuName = "Effects/Invisibility", order = 1)]
public class InvisibilityEffect : BoostEffect
{
    public GameObject Notification;

    GameObject win;
    GameObject shipRender;
    public override void EndEffect()
    {
        shipRender = GameObject.Find("shipRender");
        shipRender.gameObject.GetComponent<Renderer>().enabled = true;

        if ( win != null ) {
            Destroy( win );
        }

    }
    public override void StartEffect()
    {
        shipRender = GameObject.Find("shipRender");
        shipRender.gameObject.GetComponent<Renderer>().enabled = false;

        win = Instantiate( Notification );
    }
}
