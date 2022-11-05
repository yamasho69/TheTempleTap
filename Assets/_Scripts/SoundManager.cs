using UnityEngine;
using UnityEngine.Audio;
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

    void Start() {
        DontDestroyOnLoad(gameObject);//ƒV[ƒ“‘JˆÚ‚µ‚Ä‚à”jŠü‚³‚ê‚È‚¢
    }

    /// <summary>
    /// BGM‚ğÄ¶
    /// </summary>
    /// <param name="clip"></param>
    public void PlayBgm(AudioClip clip) {
        bgmAudioSource.clip = clip;
        bgmAudioSource.Play();
    }

    /// <summary>
    /// BGM‚ğ’â~
    /// </summary>
    public void StopBgm() {
        bgmAudioSource.Stop();
    }

    /// <summary>
    /// Œø‰Ê‰¹‚ğÄ¶
    /// </summary>
    /// <param name="clip"></param>
    public void PlaySe(AudioClip clip) {
        seAudioSource.PlayOneShot(clip);
    }

    /// <summary>
    /// ˆø”‚ÌAudioClip‚Ì’†‚©‚çƒ‰ƒ“ƒ_ƒ€‚ÉÄ¶
    /// </summary>
    /// <param name="clips"></param>

    public void RandomizeSfx(params AudioClip[] clips) {
        var randomIndex = UnityEngine.Random.Range(0, clips.Length);
        seAudioSource.PlayOneShot(clips[randomIndex]);
    }
}
