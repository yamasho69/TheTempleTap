using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
//using DG.Tweening;

public class DeleteData : MonoBehaviour
{
    public GameObject deleteText;
    public GameObject window;
    void Start()
    {
        
    }

    public void OnClick()
    {
        ES3.DeleteKey("SCORE");
        ES3.DeleteKey("LEVEL");
        ES3.DeleteKey("CLEAR");
        deleteText.SetActive(true);
        Invoke("WindowClose", 2.8f);
    }

    void WindowClose() {
        window.SetActive(false);
        deleteText.SetActive(false);
    }
}
