using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mover : MonoBehaviour {

    public float speed;

    void Start()
    {
        var ship = GetComponent<Rigidbody>();
        ship.velocity = transform.forward * speed;
    }
}
