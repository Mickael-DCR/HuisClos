using UnityEngine;
using UnityEngine.VFX;

public class FxManager : MonoBehaviour
{
    public static FxManager Instance;

    public VisualEffect FireworksFeedback;

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

    public void Fireworks()
        {
        //FireworksFeedback.transform.position = position;
        //FireworksFeedback.Play();
        FireworksFeedback.SendEvent("OnPlay");
        FireworksFeedback.SendEvent("OnPlay");
        }
    
}
