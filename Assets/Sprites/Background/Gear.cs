using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gear : MonoBehaviour
{
    [SerializeField]
    int speed = 10;
    [SerializeField]
    bool rotateClockwise = true;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.Rotate(new Vector3(0, 0, speed * (rotateClockwise ? -1 : 1) * Time.deltaTime));
    }
}
