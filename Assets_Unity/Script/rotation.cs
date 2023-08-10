using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rotation : MonoBehaviour
{
   [SerializeField] private Vector3 Vector3;
   [SerializeField] private float speed;
    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3*speed*Time.deltaTime);
    }
}
