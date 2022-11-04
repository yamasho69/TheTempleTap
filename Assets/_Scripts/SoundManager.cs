using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SoundManager : MonoBehaviour
{
    [SerializeField] AudioMixer audioMixer;
    public Slider bgmSlider;
    public Slider seSlider;
    [SerializeField] AudioSource bgmAudioSource;
    [SerializeField] AudioSource seAudioSource;

    public static SoundManager instance;
    void Awake() {
        CheckInstance();
    }
    void CheckInstance() {
        if (instance == null) {
            instance = this;
        } else {
            Destroy(gameObject);
        }
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode sceneMode) {
        Debug.Log(scene.buildIndex);//sceneの番号はscene.buildIndexで分かる
    }

    void Start() {
        DontDestroyOnLoad(gameObject);
        SceneManager.sceneLoaded += OnSceneLoaded;//イベントにメソッドを登録
        GameObject slider1 = GameObject.FindGameObjectWithTag("BGM_Slider");
        bgmSlider = slider1.GetComponent<Slider>();

        GameObject slider2 = GameObject.FindGameObjectWithTag("SE_Slider");
        seSlider = slider2.GetComponent<Slider>();
        //スライダーを動かした時の処理を登録
        bgmSlider.onValueChanged.AddListener(SetAudioMixerBGM);
        seSlider.onValueChanged.AddListener(SetAudioMixerSE);
    }

    //BGM
    public void SetAudioMixerBGM(float value) {
        //5段階補正
        value /= 5;
        //-80~0に変換
        var volume = Mathf.Clamp(Mathf.Log10(value) * 20f, -80f, 0f);
        //audioMixerに代入
        audioMixer.SetFloat("BGM", volume);
        Debug.Log($"BGM:{volume}");
    }

    //SE
    public void SetAudioMixerSE(float value) {
        //5段階補正
        value /= 5;
        //-80~0に変換
        var volume = Mathf.Clamp(Mathf.Log10(value) * 20f, -80f, 0f);
        //audioMixerに代入
        audioMixer.SetFloat("SE", volume);
        Debug.Log($"SE:{volume}");
    }
    GameObject CheckOtherSoundManager() {
        return GameObject.FindGameObjectWithTag("SoundManager");
    }
    public void PlayBgm(AudioClip clip) {
        bgmAudioSource.clip = clip;
        if (clip == null) {
            return;
        }
        bgmAudioSource.Play();
    }
    public void PlaySe(AudioClip clip) {
        if (clip == null) {
            return;
        }
        seAudioSource.PlayOneShot(clip);
    }

    //ランダムで音声再生
    public void RandomizeSfx(params AudioClip[] clips) {
        var randomIndex = UnityEngine.Random.Range(0, clips.Length);
        seAudioSource.PlayOneShot(clips[randomIndex]);
    }
}
