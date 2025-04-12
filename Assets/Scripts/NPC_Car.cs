using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;
using UnityEngine.UIElements;

public class NPC_Car : MonoBehaviour
{
    [SerializeField]
    GameObject wheel1, wheel2;

    [SerializeField]
    float speed = 1f;

    float3 startPos;
    int pathLength, current;

    [SerializeField]
    GameObject[] path;

    Vector3[] paths;

    Vector3 carPos;

    // Start is called before the first frame update
    void Start()
    {
        startPos = transform.position;
        pathLength = path.Length;

        current = 0;


        paths = new Vector3 [pathLength];
        int i = 0;
        foreach (GameObject obj in path)
        {
            paths[i] = obj.transform.position;

            i++;
        }

        carPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        wheel1.transform.Rotate(new Vector3(0f, 0f, -speed / 2f));  // rotates wheel1
        wheel2.transform.Rotate(new Vector3(0f, 0f, -speed / 2f));  // rotates wheel2

        CarMove();
    }

    private void CarMove()
    {
        transform.position = Vector3.MoveTowards(transform.position, paths[current], speed * Time.deltaTime);

        if ( math.abs(transform.position.x - paths[current].x) < 0.01f)
        {
            NextPath();
        }
    }

    private void NextPath()
    {
        current++;
        if (current == pathLength)
        {
            current = 0;
            transform.position = startPos;
        }
        carPos = transform.position;
    }    
}
