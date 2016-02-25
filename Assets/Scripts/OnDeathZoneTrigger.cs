using UnityEngine;
using System.Collections;

public class OnDeathZoneTrigger : MonoBehaviour {
    
    void OnTriggerEnter(Collider other)
    {
        // only do stuff if hit by a projectile
        if (other.tag == "Player")
        {
            // call the NextLevel function in the game manager
            GameManager.gm.RestartLevel();
        }
    }
}
