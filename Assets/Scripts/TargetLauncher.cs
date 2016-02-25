using UnityEngine;
using System.Collections;

public class TargetLauncher : MonoBehaviour
{
	// public variables
	public float secondsBetweenLaunching = 0.2f;
    public float secondsBurstDuration = 3.0f;
    public float secondsBetweenLaunchBursts = 10.0f;
    public float mass = 1.0f;
    public float power = 35.0f;
	public GameObject[] launchObjects; // what prefabs to spawn
    public AudioClip launchSFX;       // Reference to AudioClip to play
    public GameObject parentObj;

    private float nextLaunchTime;
    private float nextBurstTime;
    private float burstEndTime;
    private bool bursting;

	// Use this for initialization
	void Start ()
	{
		// determine when to spawn the next object
		nextLaunchTime = Time.time + secondsBetweenLaunching;
        nextBurstTime = Time.time + secondsBetweenLaunchBursts;
        burstEndTime = nextBurstTime + secondsBurstDuration;
	}
	
	// Update is called once per frame
	void Update ()
	{
		// exit if there is a game manager and the game is over
		if (GameManager.gm) {
			if (GameManager.gm.gameIsOver)
				return;
		}

        if (Time.time >= nextBurstTime)
        {
            if (Time.time <= burstEndTime)
            {
                // if time to spawn a new game object
                if (Time.time >= nextLaunchTime)
                {
                    // Spawn the game object through function below
                    LaunchProjectile();

                    // determine the next time to spawn the object
                    nextLaunchTime = Time.time + secondsBetweenLaunching;
                }
            }
            else
            {
                nextBurstTime = Time.time + secondsBetweenLaunchBursts;
                burstEndTime = nextBurstTime + secondsBurstDuration;
            }
        } 
	}

	void LaunchProjectile()
	{
        if (launchObjects.Length > 0)
        {
            // determine which object to spawn
            int projectileToSpawn = Random.Range(0, launchObjects.Length);

            // actually spawn the game object
            GameObject projectile = Instantiate(launchObjects[projectileToSpawn], transform.position + transform.forward, transform.rotation) as GameObject;

            // if the projectile does not have a rigidbody component, add one
            if (!projectile.GetComponent<Rigidbody>())
            {
                projectile.AddComponent<Rigidbody>();
            }

            projectile.GetComponent<Rigidbody>().mass = mass;

            // Apply force to the newProjectile's Rigidbody component if it has one
            projectile.GetComponent<Rigidbody>().AddForce(transform.forward * power, ForceMode.VelocityChange);

            if (launchSFX)
            {
                if (projectile.GetComponent<AudioSource>())
                { // the projectile has an AudioSource component
                  // play the sound clip through the AudioSource component on the gameobject.
                  // note: The audio will travel with the gameobject.
                    projectile.GetComponent<AudioSource>().PlayOneShot(launchSFX);
                }
                else {
                    // dynamically create a new gameObject with an AudioSource
                    // this automatically destroys itself once the audio is done
                    AudioSource.PlayClipAtPoint(launchSFX, projectile.transform.position);
                }
            }

            if (parentObj)
            {
                projectile.transform.parent = parentObj.transform;
            }
        }
    }
}
