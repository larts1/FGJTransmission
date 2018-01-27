using UnityEngine;

public class AnimationAutoDestroy : MonoBehaviour {
    public float delay = 0f;

    // Use this for initialization
    void Start() {
        Destroy( gameObject, this.GetComponent<Animation>().clip.length + delay );
    }
}