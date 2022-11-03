using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
//using DG.Tweening;

public class ConveniButton : MonoBehaviour
{
    [Header("�\��������Web�y�[�W")]public String url;
    [Header("�ړ��������V�[��")]public String scene;
    [Header("�\���������E���������Q�[���I�u�W�F�N�g")]public GameObject gObject;

    
    public void GoToScene() {
        SceneManager.LoadScene(scene);
    }

    public void GoToWeb() {
        Application.OpenURL(url);
    }

    public void QuitGame() {
        Application.Quit();
    }

    public void OpenOrClose() {
        if (gObject.activeSelf) {
            gObject.SetActive(false);           
        } else { gObject.SetActive(true);
        }
    }
}
