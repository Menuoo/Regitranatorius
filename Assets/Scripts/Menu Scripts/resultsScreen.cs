using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using TMPro;


public class resultsScreen : MonoBehaviour
{
    const string clear = "Laikas: ";
    const string icon1 = "Lygio perejimo bonusas";
    const string icon2 = "Laiko bonusas";
    const string icon3 = "Gyvybiu bonusas";

    [SerializeField]
    TMP_Text clearText, text1, text2, text3;

    // Start is called before the first frame update
    void Start()
    {
        clearText.SetText(clear + GlobalVariables.clearTime.ToString(@"mm\:ss\:ff"));
        text1.SetText(icon1);
        text2.SetText(icon2);
        text3.SetText(icon3);

        this.gameObject.SetActive(GlobalVariables.showResults);
        GlobalVariables.showResults = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            this.gameObject.SetActive(false);
        }

    }
}
