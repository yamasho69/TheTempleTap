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
        Debug.Log(scene.buildIndex);//scene�̔ԍ���scene.buildIndex�ŕ�����
    }

    void Start() {
        DontDestroyOnLoad(gameObject);
        SceneManager.sceneLoaded += OnSceneLoaded;//�C�x���g�Ƀ��\�b�h��o�^
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

    //�����_���ŉ����Đ�
    public void RandomizeSfx(params AudioClip[] clips) {
        var randomIndex = UnityEngine.Random.Range(0, clips.Length);
        seAudioSource.PlayOneShot(clips[randomIndex]);
    }
}
