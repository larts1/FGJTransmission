using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Mirror", menuName = "Effects/Mirror", order =1)]
public class MirrorEffect : BoostEffect
{
    public override void EndEffect()
    {
        Debug.Log("End mirror effect");
        Camera.current.transform.eulerAngles = new Vector3(73, 0, 0);
    }
    public override void StartEffect()
    {
        Debug.Log("Start mirror effect");
        Camera.current.transform.eulerAngles = new Vector3(73, 180, 0);
    }
}
