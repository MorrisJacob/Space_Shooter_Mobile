using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomRotator : MonoBehaviour {

    public float tumble;

    private void Start()
    {
        var asteroid = GetComponent<Rigidbody>();

        asteroid.angularVelocity = Random.insideUnitSphere * tumble;
    }
}
