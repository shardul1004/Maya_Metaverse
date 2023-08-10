using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class sendnft : MonoBehaviour
{
    public Button btnClick;

    public InputField inputUser;
    int number=0;

    private void Start(){
        btnClick.onClick.AddListener(GetInputOnClickHandler);
    }

    public void GetInputOnClickHandler(){
        string address=inputUser.text;
        Debug.Log(inputUser.text+" "+(number+1));
    }
}
