using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class FxManager : MonoBehaviour
{
    public static FxManager Instance;

    public List<VisualEffect> VisualEffects;
    public Dictionary<int, VisualEffect> VisualFx;


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    private void Start()
    {
        VisualFx = new Dictionary<int, VisualEffect>();
        for (int i = 0; i < VisualEffects.Count; i++)
        {
            VisualFx.Add(i, VisualEffects[i]);
        }
    }
    /*
    public void Fireworks()
        {

        if (FireworksFeedback != null) 
        {
            FireworksFeedback.Stop();
            FireworksFeedback.Reinit();
            FireworksFeedback.Play();
        }
       
    }

    public void Fireworks_2()
    {
        if (FireworksFeedback_2 != null)
        {
            FireworksFeedback_2.Stop();
            FireworksFeedback_2.Reinit();
            FireworksFeedback_2.Play();
        }
    }
    */
    public void PlayFx(int index)
    {
        VisualEffects[index].Play();
    }
    
}
