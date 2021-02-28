using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : DamageableEntity {
    
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
    private Weapon weapon;
    public GameObject bulletPrefab;

    private void Awake() {
        weapon = GetComponentInChildren<Weapon>();
    }

    protected override void Start() {
        base.Start();
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = false;
    }

    private void Update() {
        float xMov = Input.GetAxisRaw("Horizontal");
        float yMov = Input.GetAxisRaw("Vertical");

        //PLAYER MOVEMENT
        playerHolder.transform.position = playerHolder.transform.position + new Vector3(xMov,yMov,0).normalized * Time.deltaTime * playerSpeed;

        //PLAYER ROTATION
        rotAngle = Vector3.SignedAngle(Vector3.up, crosshair.transform.position - playerHolder.transform.position, Vector3.forward);
        playerRotating.transform.rotation = Quaternion.Euler(0, 0, rotAngle);

        //SHOOTING
        if (Input.GetMouseButton(0) ) {
            weapon.Shoot(rotAngle);
        }

        //CROSSHAIR MOVEMENT
        Vector3 mPos = Input.mousePosition * mouseSens;
        mPos.z = 10;
        crosshair.transform.position = cam.ScreenToWorldPoint(mPos);

        //CAMERA MOVEMENT
        camTargetPos = playerHolder.transform.position + (crosshair.transform.position - playerHolder.transform.position) / camMovementWithCrosshair + new Vector3 (0, 0, -cameraDist);
        StartCoroutine(SmoothCam());
        cam.transform.position = currentTargetPos;
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
