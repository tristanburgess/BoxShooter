using UnityEngine;
using System.Collections;

public class BasicTargetMover : MonoBehaviour {

    public float spinSpeed = 180.0f;
    public float translateMagnitue = 0.1f;
    public float translateFrequency = 5;

    public bool doRotate;
    public bool doTranslate;
	
	// Update is called once per frame
	void Update () {
        if (doRotate)
        { 
            // rotate
            gameObject.transform.Rotate(Vector3.up * spinSpeed * Time.deltaTime);
        }

        if (doTranslate)
        {
            // translate
            gameObject.transform.Translate(Vector3.up * Mathf.Cos(translateFrequency*Time.timeSinceLevelLoad) * translateMagnitue);
        }
	}
}
