using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class Weapon : MonoBehaviour {

    public SpriteRenderer sr;

    public float rateOfFire;
    public GameObject bulletPrefab;
    private float timeSinceLastShot;

    private void Start() {
        sr = GetComponent<SpriteRenderer>();
    }

    private void Update() {
        timeSinceLastShot += Time.deltaTime;
    }
    
    public void Shoot(float rotAngle) {
        if ( timeSinceLastShot < rateOfFire ) {
            return;
        }
        timeSinceLastShot = 0;

        Instantiate(bulletPrefab, this.transform.position, Quaternion.Euler(0, 0, rotAngle));
    }

    public void Drop() {
        WorldItem item = new WorldItem(this);
        Instantiate(item, transform.position, Quaternion.identity);
        GameObject.Destroy(gameObject);
    }

}
