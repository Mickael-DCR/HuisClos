using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

public class SceneFader : MonoBehaviour
{
    public float FadeTime;
    [SerializeField] private Image _fadeOutUIImage;
    [SerializeField] private GameObject _victoryScreen;

    public enum FadeDirection
    {
        In,
        Out
    }

    private void Start()
    {
        StartCoroutine(Fade(FadeDirection.In));
    }

    void SetColorImage(ref float alpha, FadeDirection fadeDirection)
    {
        _fadeOutUIImage.color = new Color(_fadeOutUIImage.color.r, _fadeOutUIImage.color.g, _fadeOutUIImage.color.b, alpha);
        alpha+=Time.deltaTime*(1/FadeTime)* (fadeDirection==FadeDirection.Out?-1:1);
    }

    public IEnumerator Fade(FadeDirection fadeDirection)
    {
        float alpha = fadeDirection == FadeDirection.In ? 1 : 0;
        float fadeEndValue = fadeDirection == FadeDirection.In ? 0 : 1;

        _fadeOutUIImage.enabled = true;

        while ((fadeDirection == FadeDirection.In && alpha >= fadeEndValue) ||
               (fadeDirection == FadeDirection.Out && alpha <= fadeEndValue))
        {
            _fadeOutUIImage.color = new Color(
                _fadeOutUIImage.color.r,
                _fadeOutUIImage.color.g,
                _fadeOutUIImage.color.b,
                alpha
            );

            alpha += Time.deltaTime * (1f / FadeTime) * (fadeDirection == FadeDirection.In ? -1 : 1);
            yield return null;
        }

        if (fadeDirection == FadeDirection.In)
        {
            _fadeOutUIImage.enabled = false;
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        StartCoroutine(Fade(FadeDirection.Out));
        _victoryScreen.SetActive(true);
        UIManager.Instance.ToggleCursor();
        PlayerController.InputSystemActions.Disable();
    }
}