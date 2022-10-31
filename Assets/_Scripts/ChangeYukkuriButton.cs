using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
//using DG.Tweening;

public class ChangeYukkuriButton : MonoBehaviour
{
    // オブジェクト参照
    public GameObject gameManager;// ゲームマネージャー
    public GameObject[] yukkuris;//ゆっくり入れる 
    public string [] yukkuriText;//ボタンの文字
    public Text buttonText;
    public Sprite[] yukkuriButtons;
    public Image buttonImg;
    private GameManager gm;
    void Start()
    {
         gm = gameManager.GetComponent<GameManager>();
    }

    void Update()
    {
        
    }

    public void OnClick() {
        if(gm.placeLevel>gm.yukkuriNumber)
            gm.yukkuriNumber++;
        else {
            gm.yukkuriNumber = 0;
        }
        ChangeYukkuri();
        Debug.Log(gm.yukkuriNumber);
    }

    void ChangeYukkuri(){
        for(int i = 0; i < yukkuris.Length; i++) {
            yukkuris[i].SetActive(false);
        }
        yukkuris[gm.yukkuriNumber].SetActive(true);
        buttonText.text = yukkuriText[gm.yukkuriNumber];
        buttonImg.sprite = yukkuriButtons[gm.yukkuriNumber];

        /*
        //0はまりさ
        if (gm.yukkuriNumber == 0) {
            
        }
        //1はれいむ
        else if (gm.yukkuriNumber == 1) {

        }
        //2はまりちゃ
        else if (gm.yukkuriNumber == 2) {

        }
        //3はれいみゅ
        else if (gm.yukkuriNumber == 3) {

        }
        //4はつみゅり
        else if (gm.yukkuriNumber == 4) {

        }
        //5はわされいみゅ
        else {

        }*/
    }
}
