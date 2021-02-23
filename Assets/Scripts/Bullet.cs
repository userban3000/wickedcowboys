using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {
    
    public float initialSpeed;
    private float speed;
    public float lifetime;

    private void Start() {
        StartCoroutine(Slowdown());
    }

    private void Update() {
        transform.Translate ( Vector3.up * speed * Time.deltaTime );
    }

    IEnumerator Slowdown() {
        float t = 0;
        float percent = 0;

        while ( t < lifetime ) {
            t += Time.deltaTime;
            percent = Mathf.Pow(t,3);
            speed = Mathf.Lerp(initialSpeed, initialSpeed/2, percent);
            yield return null;
        }

        Destroy(this);
    }

}
