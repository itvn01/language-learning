using System;
using UnityEngine;
using UnityEngine.UI;
using UMExtensions;
using DG.Tweening;

public class UIProgressBar : MonoBehaviour
{
    public enum DisplayTextTypeEnum
    {
        NONE = 0,
        CURRENT_VALUE_MAX_VALUE,
        PERCENT,
        CURRENT_VALUE,
    }

    [SerializeField] Image bar;
    [SerializeField] Image progress;
    [SerializeField] Text txtValue;
    [SerializeField] DisplayTextTypeEnum displayTextType;
    public bool isChangeColorBar = false;
    public string unitValue = "";
    [SerializeField] Color colorNormalBar = new Color(0, 63 / 255f, 21 / 255f);
    [SerializeField] Color colorWarningBar = new Color(84 / 255f, 2 / 255f, 0f);
    [SerializeField] Color colorNormalProgress = new Color(36 / 255f, 255 / 255f, 0f);
    [SerializeField] Color colorWarningProgress = new Color(255 / 255f, 0f, 21 / 255f);

    public Vector2 progressSize;

    public float maxValue = 1000;
    public float currentValue = 1000;
    public bool isTestProgress = false;
    private float currentProgressValue = 0f;

    public float MaxValue => this.maxValue;
    public float CurrentValue => this.currentValue;

    Tweener tweener;

    private void Awake()
    {
        var sizeDelta = this.progress.rectTransform.sizeDelta;
        if (!(sizeDelta.x > 0 || sizeDelta.y > 0))
            progressSize = sizeDelta;

        if (isTestProgress)
        {
            SetProgressBarWithValue(currentValue, maxValue);
        }
    }

    void ResetProgressBar(bool isAnimate = true, float duration = 0.35f)
    {
        this.currentValue = 0;
        this.UpdateProgress(isAnimate, duration);
    }

    public void SetMaxHPBar(float maxValue, bool isResetValue = true, bool isAnimate = true, float duration = 0.35f)
    {
        this.maxValue = maxValue;
        if (isResetValue)
            this.currentValue = maxValue;

        this.UpdateProgress(isAnimate, duration);
    }

    public void DecreaseHP(float value, bool isAnimate = true, float duration = 0.35f)
    {
        this.currentValue -= value;
        this.currentValue = Math.Max(0, this.currentValue);
        this.UpdateProgress(isAnimate, duration);
    }

    public void IncreaseHP(float value, bool isAnimate = true, float duration = 0.35f)
    {
        this.currentValue = Math.Max(0, this.currentValue);
        this.currentValue += value;
        this.currentValue = Math.Min(this.maxValue, this.currentValue);
        this.UpdateProgress(isAnimate, duration);
    }

    public void SetProgressBarWithValue(float currentValue, float maxValue, bool isAnimate = true, float duration = 0.35f)
    {
        this.currentValue = Math.Max(0, currentValue);
        this.maxValue = maxValue;
        this.UpdateProgress(isAnimate, duration);
    }

    void UpdateProgress(bool isAnimate = true, float duration = 0.35f)
    {
        var progress = this.currentValue / this.maxValue;
        progress = Math.Min(1, progress);

        if (isAnimate)
        {

            if (tweener != null)
                tweener.Kill();

            float fromValue = currentProgressValue;
            float toValue = progress;
            tweener = DOVirtual.Float(fromValue, toValue, duration, value =>
            {
                this.progress.rectTransform.sizeDelta = new Vector2(progressSize.x * value, progressSize.y);
                currentProgressValue = value;
            });
            
        }
        else
        {
            currentProgressValue = progress;
            this.progress.rectTransform.sizeDelta = new Vector2(progressSize.x * progress, progressSize.y);
            if (isChangeColorBar)
            {
                bool isWarning = progress > 0.4;
                this.progress.color = isWarning ? this.colorNormalProgress : this.colorWarningProgress;
                this.bar.color = isWarning ? this.colorNormalBar : this.colorWarningBar;
            }
            UpdateDisplayText();
        }

    }

    void UpdateDisplayText()
    {
        switch (displayTextType)
        {
            case DisplayTextTypeEnum.CURRENT_VALUE_MAX_VALUE:
                this.txtValue.text = $"{(int)this.currentValue}/{(int)this.maxValue}";
                break;

            case DisplayTextTypeEnum.PERCENT:
                this.txtValue.text = $"{Math.Floor(this.currentValue * 100 / this.maxValue)}%";
                break;

            case DisplayTextTypeEnum.CURRENT_VALUE:
                this.txtValue.text = $"{((int)this.currentValue).FormatNumber()}{this.unitValue}";
                break;

            default:
                this.txtValue.text = "";
                break;


        }
    }

}
