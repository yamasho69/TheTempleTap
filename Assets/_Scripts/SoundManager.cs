using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SoundManager : MonoBehaviour
{
    [SerializeField] AudioMixer audioMixer;
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
