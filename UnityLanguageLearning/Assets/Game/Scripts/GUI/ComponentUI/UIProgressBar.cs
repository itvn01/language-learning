using System;
using UnityEngine;
using UnityEngine.UI;
using UMExtensions;
using DG.Tweening;

public class UIProgressBar : MonoBehaviour
{

    public enum ProgressType
    {
        FILL = 0,
        TILE
    }
    public enum DisplayTextTypeEnum
    {
        NONE = 0,
        CURRENT_VALUE_MAX_VALUE,
        PERCENT,
        CURRENT_VALUE,
    }

    [SerializeField] ProgressType progressType;
    [SerializeField] Image bar;
    [SerializeField] Image progress;
    [SerializeField] Text txtProgressValue;
    [SerializeField] Text txtDisplayValue;
    [SerializeField] DisplayTextTypeEnum displayTextType;
    public float valueChangeColor = 0f;
    public string unitValue = "";
    [SerializeField] Color colorNormalBar = new Color(0, 63 / 255f, 21 / 255f);
    [SerializeField] Color colorWarningBar = new Color(84 / 255f, 2 / 255f, 0f);
    [SerializeField] Color colorNormalProgress = new Color(36 / 255f, 255 / 255f, 0f);
    [SerializeField] Color colorWarningProgress = new Color(255 / 255f, 0f, 21 / 255f);

    public Vector2 progressSize;

    public float maxValue = 1000;
    public float currentValue = 1000;
    public bool isTestProgress = false;

    [Header("Convert-Progress-Value")]
    public bool isUseConvertProgress = false;
    public float minConvertProgress = 0.0f;
    public float maxConvertProgress = 1.0f;
    private float amplitudeConvertProgress = 1f;

    [Header("Full Value Animation")]
    public bool isShowFullValueAnimation = false;

    private float currentProgressValue = 0f;

    public float MaxValue => this.maxValue;
    public float CurrentValue => this.currentValue;
    Tweener tweener;
    bool isSetProgressSize = false;

    private void Awake()
    {
        if (isTestProgress)
        {
            SetProgressBarWithValue(currentValue, maxValue);
        }

        if (isUseConvertProgress)
        {
            maxConvertProgress = Mathf.Clamp(maxConvertProgress, 0f, 1f);
            minConvertProgress = Mathf.Clamp(minConvertProgress, 0f, maxConvertProgress);
            amplitudeConvertProgress = maxConvertProgress - minConvertProgress;
        }
    }

    private void OnEnable()
    {
        this.SetProgressSize();
    }

    private void OnDisable()
    {
        if (tweener != null)
            tweener.Kill();

        this.CallUpdateProgress(false);
    }

    void SetProgressSize()
    {
        if (isSetProgressSize)
            return;

        isSetProgressSize = true;
        var sizeDelta = this.progress.rectTransform.sizeDelta;
        if (sizeDelta.x > 0 || sizeDelta.y > 0)
            progressSize = sizeDelta;
        // Debug.Log("SetProgressSize => " + progressSize.x);
    }

    void ResetProgressBar(bool isAnimate = true, float duration = 0.35f)
    {
        this.currentValue = 0;
        this.CallUpdateProgress(isAnimate, duration);
    }

    public void SetMaxValueBar(float maxValue, bool isResetValue = true, bool isAnimate = true, float duration = 0.35f)
    {
        this.maxValue = maxValue;
        if (isResetValue)
            this.currentValue = maxValue;

        this.CallUpdateProgress(isAnimate, duration);
    }

    public void DecreaseValue(float value, bool isAnimate = true, float duration = 0.35f)
    {
        this.currentValue -= value;
        this.currentValue = Math.Max(0, this.currentValue);
        this.CallUpdateProgress(isAnimate, duration);
    }

    public void IncreaseValue(float value, bool isAnimate = true, float duration = 0.35f)
    {
        this.currentValue = Math.Max(0, this.currentValue);
        this.currentValue += value;
        this.currentValue = Math.Min(this.maxValue, this.currentValue);
        this.CallUpdateProgress(isAnimate, duration);
    }

    public void SetProgressBarWithValue(float currentValue, float maxValue, bool isAnimate = true, float duration = 0.35f)
    {
        // Debug.Log("SetProgressBarWithValue => " + currentValue);
        this.currentValue = Math.Max(0, currentValue);
        this.maxValue = maxValue;
        this.CallUpdateProgress(isAnimate, duration);
    }

    void CallUpdateProgress(bool isAnimate = true, float duration = 0.35f)
    {
        if (!gameObject.activeSelf)
            return;

        var progressValue = this.currentValue / this.maxValue;
        progressValue = Math.Clamp(progressValue, 0f, 1f);

        if (isUseConvertProgress)
        {
            progressValue = GetConvertProgress(progressValue);
        }

        if (tweener != null)
            tweener.Kill();


        if (isAnimate)
        {
            float fromValue = currentProgressValue;
            float toValue = progressValue;
            tweener = DOVirtual.Float(fromValue, toValue, duration, value =>
            {
                if (progressType == ProgressType.FILL)
                {
                    this.progress.fillAmount = value;
                }
                else
                {
                    this.progress.rectTransform.sizeDelta = new Vector2(progressSize.x * value, progressSize.y);
                }
                currentProgressValue = value;
                UpdateDisplayText(value * this.maxValue);

                if (value > -toValue)
                {
                    CheckAndShowFullProgressValueAnimation();
                }
            });

        }
        else
        {
            currentProgressValue = progressValue;
            if (progressType == ProgressType.FILL)
            {
                this.progress.fillAmount = progressValue;
            }
            else
            {
                this.progress.rectTransform.sizeDelta = new Vector2(progressSize.x * progressValue, progressSize.y);
            }

            if (valueChangeColor > 0)
            {
                bool isWarning = progressValue < valueChangeColor;
                this.progress.color = isWarning ? this.colorWarningProgress : this.colorNormalProgress;
                this.bar.color = isWarning ? this.colorWarningBar : this.colorNormalBar;
            }
            UpdateDisplayText(this.currentValue);

            CheckAndShowFullProgressValueAnimation();
        }
    }

    void UpdateDisplayText(float value)
    {
        if (this.txtProgressValue == null)
            return;
        switch (displayTextType)
        {
            case DisplayTextTypeEnum.CURRENT_VALUE_MAX_VALUE:
                this.txtProgressValue.text = $"{(int)value}/{(int)this.maxValue}";
                break;

            case DisplayTextTypeEnum.PERCENT:
                this.txtProgressValue.text = $"{Math.Ceiling(value * 100f / this.maxValue)}%";
                break;

            case DisplayTextTypeEnum.CURRENT_VALUE:
                this.txtProgressValue.text = $"{((int)value)}{this.unitValue}";
                break;

            default:
                this.txtProgressValue.text = "";
                break;


        }
    }

    private float GetConvertProgress(float progressValue)
    {
        return minConvertProgress + amplitudeConvertProgress * progressValue;
    }

    //Show other value not according to progress
    public void SetDisplayValueText(float value)
    {
        if (this.txtDisplayValue == null)
            return;

        this.txtDisplayValue.text = $"{((int)value)}{this.unitValue}";
    }

    public void SetDisplayTextActive(bool isActive)
    {
        if (this.txtDisplayValue == null)
            return;

        this.txtDisplayValue.gameObject.SetActive(isActive);
    }

    bool isFullValueAnimationRunning = false;
    private void CheckAndShowFullProgressValueAnimation()
    {
        if (!this.isShowFullValueAnimation)
            return;

        if (Math.Ceiling(this.currentValue * 100f / this.maxValue) <= 99)
        {
            if (isFullValueAnimationRunning)
            {
                this.progress.color = colorNormalProgress;
                this.progress.DOKill();
            }

            isFullValueAnimationRunning = false;
            return;
        }

        if (!isFullValueAnimationRunning)
        {
            isFullValueAnimationRunning = true;
            this.progress.DOColor(colorWarningProgress, 0.35f).SetLoops(-1, LoopType.Yoyo);
        }
    }

}
