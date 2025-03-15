using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class NPC_Car : MonoBehaviour
{
    [SerializeField]
    GameObject wheel1, wheel2;

    [SerializeField]
    float speed = 20f;

    [SerializeField]
    float distance = 40f;

    float3 startPos;

    // Start is called before the first frame update
    void Start()
    {
        startPos = transform.localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(new Vector2(speed * Time.deltaTime, 0f)); // actually makes the car move
        wheel1.transform.Rotate(new Vector3(0f, 0f, -speed / 2f));  // rotates wheel1
        wheel2.transform.Rotate(new Vector3(0f, 0f, -speed / 2f));  // rotates wheel2

        if (transform.localPosition.x - startPos.x > distance)
        {
            transform.localPosition = startPos;
        }
    }
}
