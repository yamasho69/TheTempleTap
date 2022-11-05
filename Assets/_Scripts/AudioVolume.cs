using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioVolume : MonoBehaviour {
    public AudioMixer audioMixer;
    public Slider bGMSlider;
    public Slider sESlider;

    private void Start() {
        float bgmVolume = ES3.Load<float>("BGM_VOLUME", 1);
        float seVolume = ES3.Load<float>("SE_VOLUME", 5);
        bGMSlider.value = bgmVolume;
        sESlider.onValueChanged.AddListener(SetSE);
        SetSE(seVolume);
    }

    public void SetBGM(float volume) {
        bGMSlider.value = volume;
        audioMixer.SetFloat("BGM", volume);
        ES3.Save<float>("BGM_VOLUME", volume);
    }

    public void SetSE(float volume) {
        //sESlider.value = volume;
        //audioMixer.SetFloat("SE", volume);
        //ES3.Save<float>("SE_VOLUME", volume);

        //5íiäKï‚ê≥
        var a = volume /= 5;

        //-80~0Ç…ïœä∑
        var marume = Mathf.Clamp(Mathf.Log10(a) * 20f, -80f, 0f); //marume 0.2  a = 3/5
        audioMixer.SetFloat("SE", marume);
        ES3.Save<float>("SE_VOLUME", volume);
    }
}