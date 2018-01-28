using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Mirror", menuName = "Effects/Mirror", order =1)]
public class MirrorEffect : BoostEffect 
{

    public GameObject Notification;

    GameObject win;
    public override void EndEffect()
    {
        if ( win != null ) {
            Destroy( win );
        }
        Camera.main.transform.eulerAngles = new Vector3(73, 0, 0);
    }
    public override void StartEffect()
    {
        win = Instantiate( Notification );
        Camera.main.transform.eulerAngles = new Vector3(73, 180, 0);
    }
}
