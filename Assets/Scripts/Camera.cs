using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{
    [SerializeField]
    public Transform target;

    void Update()
    {
        transform.position = new Vector3(target.position.x + 6, 0, -10);
    }
}
