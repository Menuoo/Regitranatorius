using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Camera : MonoBehaviour
{
    [SerializeField]
    public Transform target;

    [SerializeField]
    TMP_Text timerText;

    void Update()
    {
        transform.position = new Vector3(target.position.x + 6, 0, -10);
        timerText.text = GlobalVariables.sw.Elapsed.ToString(@"mm\:ss\:ff");
    }
}
