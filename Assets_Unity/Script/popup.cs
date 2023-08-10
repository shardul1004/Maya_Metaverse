using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class popup : MonoBehaviour
{
    public GameObject anyname;
    public void opencanvas(){
        anyname.SetActive(true);
    }

    public void closecanvas(){
        anyname.SetActive(false);
    }
}
