using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager _gameManager { get; set; }
    public PlayerInventory playerInventory;
    public PlayerController playerController;
    public AudioSource audioEmmiter;
    public AudioClip clickAudio;
    public AudioClip backAudio;
    public AudioClip closeAudio;
    public AudioClip badAudio;

    [SerializeField] private AnimationCurve focusCurve;
    [SerializeField] private AnimationCurve unfocusCurve;
    [SerializeField] private float focusTime;
    [SerializeField] private float unfocusTime;
    [SerializeField] private float currentFOV;
    [SerializeField] private float originalFOV;
    [SerializeField] private Vector3 camOrigin;
    [SerializeField] private Vector3 camCurrentPos;


    Coroutine cCamFocus;
    private void Awake()
    {
        if (Instance == null)
        {
            _gameManager = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        Application.targetFrameRate = 60;
        audioEmmiter = GetComponent<AudioSource>();
        camOrigin = Camera.main.transform.position;
        originalFOV = Camera.main.orthographicSize;
    }

    public void PlaySound(AudioClip _clip)
    {
        audioEmmiter.PlayOneShot(_clip);
    }

    public static GameManager Instance
    {
        get => _gameManager;
    }

    public void PlayClickSound()
    {
        audioEmmiter.PlayOneShot(clickAudio);
    }
    public void PlayBackSound()
    {
        audioEmmiter.PlayOneShot(backAudio);
    }
    public void PlayCloseSound()
    {
        audioEmmiter.PlayOneShot(closeAudio);
    }
    public void PlayBadSound()
    {
        audioEmmiter.PlayOneShot(badAudio);
    }

    public void FocusObjective(Transform _destiny, float _focusFOV)
    {
        cCamFocus = StartCoroutine(CameraFocus(_destiny, _focusFOV));
    }
    public void UnfocusCam()
    {
        cCamFocus = StartCoroutine(CameraUnfocus());
    }

    IEnumerator CameraFocus(Transform _destiny, float focusFOV)
    {
        float counter = 0;
        camCurrentPos = Camera.main.transform.position;
        currentFOV = Camera.main.orthographicSize;
        while (counter < focusTime)
        {
            Camera.main.transform.position = Vector3.Lerp(camCurrentPos, _destiny.position, focusCurve.Evaluate(counter / focusTime));
            Camera.main.orthographicSize = Mathf.Lerp(currentFOV, focusFOV, focusCurve.Evaluate(counter / focusTime));
            counter += Time.deltaTime;
            yield return null;
        }
        cCamFocus = null;
    }

    IEnumerator CameraUnfocus()
    {
        float counter = 0;
        camCurrentPos = Camera.main.transform.position;
        currentFOV = Camera.main.orthographicSize;
        while (counter < unfocusTime)
        {
            Camera.main.transform.position = Vector3.Lerp(camCurrentPos, camOrigin, unfocusCurve.Evaluate(counter / unfocusTime));
            Camera.main.orthographicSize = Mathf.Lerp(currentFOV, originalFOV, unfocusCurve.Evaluate(counter / unfocusTime));
            counter += Time.deltaTime;
            yield return null;
        }
        cCamFocus = null;
    }

}
