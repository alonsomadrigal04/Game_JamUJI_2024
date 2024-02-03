using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallDestroyer : MonoBehaviour
{
    public pickObjects pickObjectse;

    void Update()
    {
        pickObjectse = FindObjectOfType<pickObjects>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Destroy(pickObjectse.mainObject);
    }

}
