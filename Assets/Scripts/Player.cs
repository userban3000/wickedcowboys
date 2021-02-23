using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
    
    [Header("Player")]
    public GameObject playerHolder;
    public GameObject playerRotating;
    public float playerSpeed;
    public float mouseSens;
    private float rotAngle;

    [Header("Camera & Mouse")]
    public Camera cam;
    public GameObject crosshair;
    public float cameraDist = 10;
    [Range(1,10)]
    public float camMovementWithCrosshair = 4;
    [Range(0,10)]
    public float camSmoothness = 4.5f;
    private Vector3 camTargetPos;
    private Vector3 currentTargetPos;

    [Header("Shooting")]
    public GameObject bulletPrefab;
    public float bulletSpawnDist;
    public float rateOfFire;
    public float timeSinceLastShot;

    private void Start() {
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = false;
    }

    private void Update() {
        float xMov = Input.GetAxisRaw("Horizontal");
        float yMov = Input.GetAxisRaw("Vertical");

        //PLAYER MOVEMENT
        playerHolder.transform.position = playerHolder.transform.position + new Vector3(xMov,yMov,0).normalized * Time.deltaTime * playerSpeed;

        //SHOOTING
        timeSinceLastShot += Time.deltaTime;
        if (Input.GetMouseButton(0) ) {
            Shoot();
        }

        //CROSSHAIR MOVEMENT
        Vector3 mPos = Input.mousePosition * mouseSens;
        mPos.z = 10;
        crosshair.transform.position = cam.ScreenToWorldPoint(mPos);

        //CAMERA MOVEMENT
        camTargetPos = playerHolder.transform.position + (crosshair.transform.position - playerHolder.transform.position) / camMovementWithCrosshair + new Vector3 (0, 0, -cameraDist);
        StartCoroutine(SmoothCam());
        cam.transform.position = currentTargetPos;
        //cam.gameObject.transform.position = playerHolder.transform.position + (crosshair.transform.position - playerHolder.transform.position) / camMovementWithCrosshair + new Vector3 (0, 0, -cameraDist);

        //PLAYER ROTATION
        rotAngle = Vector3.SignedAngle(Vector3.up, crosshair.transform.position - playerHolder.transform.position, Vector3.forward);
        playerRotating.transform.rotation = Quaternion.Euler(0, 0, rotAngle);
    }

    private void Shoot() {
        if ( timeSinceLastShot < rateOfFire ) {
            return;
        }
        timeSinceLastShot = 0;

        Instantiate(bulletPrefab, playerHolder.transform.position, Quaternion.Euler(0, 0, rotAngle));
    }

    IEnumerator SmoothCam() {
        float t = 0;
        float percent = 0;

        Vector3 originalPos = cam.gameObject.transform.position;

        while ( percent < 1 ) {
            t += Time.deltaTime;
            percent = t * camSmoothness;

            currentTargetPos = Vector3.Lerp(originalPos, camTargetPos, percent);

            yield return null;
        }
    }

}
