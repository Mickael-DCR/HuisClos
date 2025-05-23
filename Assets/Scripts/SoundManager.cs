using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;

    public AudioSource Src;

    [Header("Audio clips")]
    public AudioClip GearClip, WinGearClip, ObjectTake, ObjectDrop;

    public void PlaySFX(AudioClip clip)
    {
        Src.PlayOneShot(clip);
    }

    void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void PlayGear() => PlaySFX(GearClip);
    public void PlayGearWin() => PlaySFX(WinGearClip);
    public void PlayTake() => PlaySFX(ObjectTake);
    public void PlayDrop() => PlaySFX(ObjectDrop);

    
}
