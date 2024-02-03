using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectilePicked : MonoBehaviour
{
    public pickObjects pickObjectse;
    public float velocity;

    private void Start()
    {
        pickObjectse = FindObjectOfType<pickObjects>();
    }
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("Dentro");
            GameObject pickedObject = pickObjectse.mainObject;
            pickedObject.transform.parent = null;
            pickedObject.GetComponent<Rigidbody2D>().velocity = new Vector2(velocity, 0.0f);
        }

    }
}
