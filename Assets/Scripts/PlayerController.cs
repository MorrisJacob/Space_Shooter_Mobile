using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class Boundary
{
    public float xMin, xMax, zMin, zMax;
}

public class PlayerController : MonoBehaviour
{
    public float tilt;
    public float speed;
    public Boundary boundary;

    public GameObject shot;
    public Transform shotSpawn; //shotSpawn.transform.position
    public SimpleTouchPad touchPad;
    public SimpleTouchAreaButton areaButton;

    public float fireRate;
    private float nextFire;

    private Quaternion calibrationQuaternion;

    private void Start()
    {
        CalibrateAccellerometer();
    }

    private void Update()
    {
        if(areaButton.CanFire() && Time.time > nextFire)
        {
            nextFire = Time.time + fireRate;
            Instantiate(shot, shotSpawn.position, shotSpawn.rotation);
            var audio = GetComponent<AudioSource>();

           // if (!audio.isPlaying)
           // {
                GetComponent<AudioSource>().Play();
           // }
        }      
    }

    private void FixedUpdate()
    {

        //float moveHorizontal = Input.GetAxis("Horizontal");
        //float moveVertical = Input.GetAxis("Vertical");

        //get movement entered
        //Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);  //for webdev

        //Vector3 accelerationRaw = Input.acceleration;
        //line and function added for comfort in how you're initially holding your phone
        //Vector3 acceleration = FixAcceleration(accelerationRaw);

        Vector2 direction = touchPad.GetDirection();

        Vector3 movement = new Vector3(direction.x, 0.0f, direction.y);

        Rigidbody ship = GetComponent<Rigidbody>();
        //move ship
        ship.velocity = movement * speed;

        //clamp the player inside the screen
        ship.position = new Vector3
            (
            Mathf.Clamp(ship.position.x, boundary.xMin, boundary.xMax),
            0.0f,
            Mathf.Clamp(ship.position.z, boundary.zMin, boundary.zMax)
            );

        ship.rotation = Quaternion.Euler(0.0f, 0.0f, ship.velocity.x * -tilt);

    }


    //used to calibrate the acceleration input for the device
    void CalibrateAccellerometer()
    {
        Vector3 accelerationSnapshot = Input.acceleration;
        Quaternion rotateQuaternion = Quaternion.FromToRotation(new Vector3(0.0f, 0.0f, -1.0f), accelerationSnapshot);
        calibrationQuaternion = Quaternion.Inverse(rotateQuaternion);
    }

    //get the "calibrated" value from the phone's input
    Vector3 FixAcceleration(Vector3 acceleration)
    {
        Vector3 fixedAcceleration = calibrationQuaternion * acceleration;


        return fixedAcceleration;
    }
}