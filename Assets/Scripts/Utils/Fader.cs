using System;
using DG.Tweening;
using GameLogic;
using UnityEngine;
using UnityEngine.UI;
using Utils;


[RequireComponent(typeof(Image))]
public class Fader : PersistentMonoSingleton<Fader>
{
    [SerializeField] private Color toColor;
    [SerializeField] private bool startWithColor = false;

    private Image _imageComponent;
    private Color _transparent;
    private Color _mainColor;

    protected override void Awake()
    {
        base.Awake();
        
        _transparent = new Color(toColor.r, toColor.g, toColor.b, 0);
        _mainColor = toColor;
        
        _imageComponent = GetComponent<Image>();
        _imageComponent.raycastTarget = false;
        _imageComponent.color = startWithColor ? _mainColor : _transparent;
    }

    private void OnEnable()
    {
        GameSignals.GameStart += () => FadeOut(0.3f);
    }
    
    private void OnDisable()
    {
        GameSignals.GameStart -= () => FadeOut(0.3f);
    }

    private void Fade(float value, float time) => 
        _imageComponent.DOFade(value, time);

    public void FadeIn(float time) =>
        Fade(1f, time);
    
    public void FadeOut(float time) =>
        Fade(0f, time);
}
