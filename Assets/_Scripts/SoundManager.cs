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
        Debug.Log(scene.buildIndex);//scene�̔ԍ���scene.buildIndex�ŕ�����
    }

    void Start() {
        DontDestroyOnLoad(gameObject);
        SceneManager.sceneLoaded += OnSceneLoaded;//�C�x���g�Ƀ��\�b�h��o�^
        GameObject slider1 = GameObject.FindGameObjectWithTag("BGM_Slider");
        bgmSlider = slider1.GetComponent<Slider>();

        GameObject slider2 = GameObject.FindGameObjectWithTag("SE_Slider");
        seSlider = slider2.GetComponent<Slider>();
        //�X���C�_�[�𓮂��������̏�����o�^
        bgmSlider.onValueChanged.AddListener(SetAudioMixerBGM);
        seSlider.onValueChanged.AddListener(SetAudioMixerSE);
    }

    //BGM
    public void SetAudioMixerBGM(float value) {
        //5�i�K�␳
        value /= 5;
        //-80~0�ɕϊ�
        var volume = Mathf.Clamp(Mathf.Log10(value) * 20f, -80f, 0f);
        //audioMixer�ɑ��
        audioMixer.SetFloat("BGM", volume);
        Debug.Log($"BGM:{volume}");
    }

    //SE
    public void SetAudioMixerSE(float value) {
        //5�i�K�␳
        value /= 5;
        //-80~0�ɕϊ�
        var volume = Mathf.Clamp(Mathf.Log10(value) * 20f, -80f, 0f);
        //audioMixer�ɑ��
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

    //�����_���ŉ����Đ�
    public void RandomizeSfx(params AudioClip[] clips) {
        var randomIndex = UnityEngine.Random.Range(0, clips.Length);
        seAudioSource.PlayOneShot(clips[randomIndex]);
    }
}
