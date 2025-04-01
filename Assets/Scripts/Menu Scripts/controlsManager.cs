using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using TMPro;
using System.Text.RegularExpressions;

public class controlsManager : MonoBehaviour
{
    static public Dictionary<string, KeyCode> controls = new Dictionary<string, KeyCode>();

    public bool listening = false;

    public string control_name="";

    public static bool done = false;

    public Button[] buttons;
    void Awake()
    {
        if(!done)
        {
            controls.Add("forward", KeyCode.D);
            controls.Add("backward", KeyCode.A);
            controls.Add("upward", KeyCode.W);
            controls.Add("downward", KeyCode.S);
            controls.Add("jump", KeyCode.Space);
            controls.Add("nitrous", KeyCode.Q);
            controls.Add("headlight", KeyCode.L);
            controls.Add("honk", KeyCode.H);
            controls.Add("gear", KeyCode.P);
            done = true;
        }

        foreach (Button button in buttons)
        {
            TextMeshProUGUI texts = button.GetComponentsInChildren<TextMeshProUGUI>()[0];

            string str = button.name;
            str = Regex.Replace(str, "Button", "");

            texts.text = controls[str].ToString();
        }

    }

    public void changeControls(string control)
    {
        listening = true;
        control_name = control;
        //print(controls[control]);
        foreach(KeyCode key in System.Enum.GetValues(typeof(KeyCode)))
        {
            
            if (Input.GetKeyDown(key))
            {
                controls[control] = key;
                print("changed "+control+" to "+key);
                listening = false;
                foreach(Button button in buttons)
                {
                    
                    if(button.name == control+"Button")
                    {
                        //button.GetComponent<TextMeshProUGUI>()[0].text = key.ToString();
                        button.GetComponentsInChildren<TextMeshProUGUI>()[0].text = key.ToString();
                        button.interactable = false;
                        button.interactable = true;
                    }
                }

                break;
            }
        }
    }
    void Update()
    {
        if(listening)
        {
            changeControls(control_name);
        }
    }
}
