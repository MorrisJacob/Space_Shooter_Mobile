using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyByBoundary : MonoBehaviour {

    private void OnTriggerExit(Collider other)
    {
        //destroy the player's shot when it leaves the screen
        Destroy(other.gameObject);
    }
}
