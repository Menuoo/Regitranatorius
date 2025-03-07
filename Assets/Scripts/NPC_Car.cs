using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC_Car : MonoBehaviour
{
    [SerializeField]
    GameObject wheel1, wheel2;

    float speed = 20f;


    // Start is called before the first frame update
    void Start()
    {
        transform.localPosition = new Vector3 (-20f, -0.5f, 0f);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(new Vector2(speed * Time.deltaTime, 0f)); // actually makes the car move
        wheel1.transform.Rotate(new Vector3(0f, 0f, -speed / 2f));  // rotates wheel1
        wheel2.transform.Rotate(new Vector3(0f, 0f, -speed / 2f));  // rotates wheel2

        if (transform.localPosition.x > 20f)
        {
            transform.localPosition = new Vector3(-20f, -0.5f, 0f);
        }
    }
}
