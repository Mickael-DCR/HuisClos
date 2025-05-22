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

    public void PlayGear() => Src.PlayOneShot(GearClip);
    public void PlayGearWin() => Src.PlayOneShot(WinGearClip);
    public void PlayTake() => Src.PlayOneShot(ObjectTake);
    public void PlayDrop() => Src.PlayOneShot(ObjectDrop);

}
