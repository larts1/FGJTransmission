using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Invisibility", menuName = "Effects/Invisibility", order = 1)]
public class InvisibilityEffect : BoostEffect
{
    GameObject shipRender;
    public override void EndEffect()
    {
        Debug.Log("End invisibility");
        shipRender.gameObject.GetComponent<Renderer>().enabled = true;
    }
    public override void StartEffect()
    {
        Debug.Log("Start invisibility");
        shipRender = GameObject.Find("shipRender");
        shipRender.gameObject.GetComponent<Renderer>().enabled = false;
    }
}
