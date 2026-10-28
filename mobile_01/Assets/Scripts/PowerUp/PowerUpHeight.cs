using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpHeight : PowerUpBase
{
    [Header("Power Up Height")]
    public float amountHeight = 2f;
    public float animationDuration = .1f;
    public DG.Tweening.Ease ease = DG.Tweening.Ease.OutBack;

    protected override void StartPowerUp()
    {
        base.StartPowerUp();
        PlayerController.Instance.ChangeHeight(amountHeight, duration, animationDuration, ease);
    }

    //FORMA ALTERNATIVA DE AJUSTAR A ANIMAÇÃO
    //protected override void EndPowerUp()
    //{
    //base.EndPowerUp();
    //PlayerController.Instance.ResetHeight(animationDuration);
    //}

    protected override void EndPowerUp()
    {
        base.EndPowerUp();
        PlayerController.Instance.ResetHeight();
    }
}
