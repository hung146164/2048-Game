using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonEffect : MonoBehaviour
{
    [SerializeField] private ButtonEffectData enter;
    [SerializeField] private ButtonEffectData click;
    [SerializeField] private ButtonEffectData exit;

    private Image imageButton;

    private void Awake()
    {
        imageButton = GetComponent<Image>();
    }
    private void Start()
    {
        ExitButton();
    }
    public void EnterButton()
    {
        SoundManager.Instance.PlayEnterButtonSFX();
        imageButton.color=enter.color;
    }
    public void ClickButton()
    {
        SoundManager.Instance.PlayClickButtonSFX();
        imageButton.color = click.color;
    }
    public void ExitButton()
    {
        imageButton.color = exit.color;
    }
}
