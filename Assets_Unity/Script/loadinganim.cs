using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class loadinganim : MonoBehaviour
{
    public TMP_Text loading;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        for(;;){
            loading.text="Loading.";
            loading.text="Loading.";
            loading.text="Loading..";
            loading.text="Loading..";
            loading.text="Loading...";
            loading.text="Loading...";
            loading.text="Loading....";
            loading.text="Loading....";
            loading.text="Loading....";
            loading.text="Loading.....";
            loading.text="Loading.....";
            
        }
    }
}
