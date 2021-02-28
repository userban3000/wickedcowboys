using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour {

    public float rateOfFire;
    public GameObject bulletPrefab;
    private float timeSinceLastShot;

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

}
