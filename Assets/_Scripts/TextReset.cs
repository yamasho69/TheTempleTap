using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
//using DG.Tweening;

public class TextReset : MonoBehaviour
{
    public Text messagetext;
    public void MessegeReset()
    {
        messagetext.text = "";   
    }
}
