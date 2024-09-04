using UnityEngine;
using UnityEngine.UI;
using System;
using DG.Tweening;
using UMExtensions;

namespace M1Game
{
    [System.Serializable]
    public enum TRANSITION_TYPE
    {
        TOP,
        BOTTOM,
        LEFT,
        RIGHT,
        ZOOM,
        NONE
    }

    public class UIPopupBase : MonoBehaviour
    {
        [SerializeField] public GameObject popupObj;
        [SerializeField] public GameObject background;
        [SerializeField] Image blackPanel;
        [SerializeField] public Button btnClose;
        public bool isDestroyWhenHide = false;
        public bool isFadeBackground = false;
        private TRANSITION_TYPE showType = TRANSITION_TYPE.ZOOM;
        private TRANSITION_TYPE hideType = TRANSITION_TYPE.ZOOM;
        public float DURATION_MOVE_SHOW = 0.3f;
        public float DURATION_MOVE_HIDE = 0.3f;
        public float DURATION_ZOOM_SHOW = 0.3f;
        public float DURATION_ZOOM_HIDE = 0.3f;
        public float BLACK_OPACITY = 180.0f / 255.0f;
        private bool _isAnimRunning = false;
        private Vector3 _originPosition;

        protected virtual void Awake()
        {
            this._originPosition = this.background.transform.localPosition;
            // Debug.LogFormat("_originPosition => {0}", _originPosition.ToString());
        }

        protected virtual void Start()
        {
            if (this.btnClose != null)
            {
                btnClose.onClick.AddListener(delegate { OnBtnCloseClicked(btnClose); });
            }

            if (this.blackPanel)
            {
                var btnBlackClose = this.blackPanel.GetComponent<Button>();
                if (btnBlackClose != null)
                {
                    btnBlackClose.onClick.AddListener(delegate { OnBtnCloseClicked(btnBlackClose); });

                    if (btnClose == null)
                        btnClose = btnBlackClose;
                }
            }
        }

        bool IsTransitionMove(TRANSITION_TYPE type)
        {
            return type != TRANSITION_TYPE.ZOOM && type != TRANSITION_TYPE.NONE;
        }

        public virtual void Show(TRANSITION_TYPE showType = TRANSITION_TYPE.ZOOM, TRANSITION_TYPE hideType = TRANSITION_TYPE.ZOOM, Action callback = null)
        {
            if (this._isAnimRunning)
                return;
            this._isAnimRunning = true;
            this.showType = showType;
            this.hideType = hideType;

            this.FadeIn();

            if (this.showType == TRANSITION_TYPE.ZOOM)
            {

                this.background.transform.localScale = Vector3.one * 0.8f;
                this.background.transform.DOScale(Vector3.one, DURATION_ZOOM_SHOW).SetEase(Ease.OutBack).OnComplete(() =>
                {
                    this.OnShowComplete(callback);
                });
            }
            else if (this.IsTransitionMove(this.showType))
            {
                this.ShowByMove(() =>
                {
                    this.OnShowComplete(callback);
                });
            }
            else if (this.hideType == TRANSITION_TYPE.NONE)
            {
                this.ActionWaitTime(DURATION_ZOOM_SHOW, () => this.OnShowComplete(callback));
            }
        }

        public virtual void hide(Action callback = null)
        {
            if (this._isAnimRunning) return;
            this._isAnimRunning = true;

            if (this.hideType == TRANSITION_TYPE.ZOOM)
            {
                this.background.transform.DOScale(Vector3.one * 0.8f, DURATION_ZOOM_HIDE).SetEase(Ease.InBack).OnComplete(() =>
                {
                    this.OnHideComplete(callback);
                });
            }
            else if (this.IsTransitionMove(this.hideType))
            {
                this.HideByMove(() =>
                {
                    this.OnHideComplete(callback);
                });
            }
            else if (this.hideType == TRANSITION_TYPE.NONE) {
                this.ActionWaitTime(DURATION_ZOOM_SHOW, () => this.OnHideComplete(callback));
            }

            this.FadeOut();
        }

        private void OnHideComplete(Action callback)
        {
            this._isAnimRunning = false;
            if (callback != null)
            {
                callback();
            }

            if (this.isDestroyWhenHide)
            {
                if (this.popupObj == null)
                    this.popupObj = gameObject;

                Destroy(this.popupObj);
            }
            else
            {
                if (this.blackPanel)
                {
                    this.blackPanel.gameObject.SetActive(false);
                }
                if (this.popupObj == null)
                    this.popupObj = gameObject;

                this.popupObj.SetActive(false);
            }
        }

        private void OnShowComplete(Action callback)
        {
            this._isAnimRunning = false;
            // Debug.Log("==> OnShowComplete");
            if (callback != null)
            {
                callback();
            }
        }

        private void ShowByMove(Action callback)
        {
            var target_position = this._originPosition;
            var start_posision = new Vector3();
            var width = Math.Max(Screen.width, 1242);
            var height = Math.Max(Screen.height, 2688);

            switch (this.showType)
            {
                case TRANSITION_TYPE.TOP:
                    start_posision = new Vector3(target_position.x, target_position.y + height* 1.5f);
                    break;

                case TRANSITION_TYPE.BOTTOM:
                    start_posision = new Vector3(target_position.x, target_position.y - height* 1.5f);
                    break;

                case TRANSITION_TYPE.LEFT:
                    start_posision = new Vector3(target_position.x - width* 1.5f, target_position.y);
                    break;

                case TRANSITION_TYPE.RIGHT:
                    start_posision = new Vector3(target_position.x + width* 1.5f, target_position.y);
                    break;

                default:
                    start_posision = new Vector3(target_position.x, target_position.y + height* 1.5f);
                    break;
            }

            // Debug.LogFormat("start_posision => {0}", start_posision.ToString());
            this.background.transform.localPosition = start_posision;
            this.background.transform.DOLocalMove(target_position, DURATION_MOVE_SHOW).SetEase(Ease.OutCirc).OnComplete(() =>
            {
                if (callback != null)
                {
                    callback();
                }
            });
        }

        private void HideByMove(Action callback)
        {
            var target_position = new Vector3();
            var start_posision = this.background.transform.localPosition;
            var width = Math.Max(Screen.width, 1242);
            var height = Math.Max(Screen.height, 2688);

            switch (this.hideType)
            {
                case TRANSITION_TYPE.TOP:
                    target_position = new Vector3(start_posision.x, start_posision.y + height* 1.5f);
                    break;

                case TRANSITION_TYPE.BOTTOM:
                    target_position = new Vector3(start_posision.x, start_posision.y - height* 1.5f);
                    break;

                case TRANSITION_TYPE.LEFT:
                    target_position = new Vector3(start_posision.x - width* 1.5f, start_posision.y);
                    break;

                case TRANSITION_TYPE.RIGHT:
                    target_position = new Vector3(start_posision.x + width* 1.5f, start_posision.y);
                    break;

                default:
                    target_position = new Vector3(start_posision.x, start_posision.y + height* 1.5f);
                    break;
            }

            this.background.transform.DOLocalMove(target_position, DURATION_MOVE_HIDE).SetEase(Ease.OutCubic).OnComplete(() =>
            {
                if (callback != null)
                {
                    callback();
                    this.background.transform.localPosition = start_posision;
                }
            });
        }

        private void FadeIn()
        {
            float duration = this.showType == TRANSITION_TYPE.ZOOM ? this.DURATION_ZOOM_SHOW : this.DURATION_MOVE_SHOW;
            if (this.isFadeBackground)
            {
                var canvasGroup = this.background.GetComponent<CanvasGroup>();
                if (canvasGroup == null)
                {
                    canvasGroup = this.background.AddComponent<CanvasGroup>();
                }
                canvasGroup.alpha = 0;

                canvasGroup.DOFade(1.0f, duration);
            }

            if (this.blackPanel)
            {
                this.blackPanel.gameObject.SetActive(true);
                this.blackPanel.gameObject.SetObjectAlpha(0.0f);
                // Debug.LogFormat("BLACK_OPACITY => {0}", BLACK_OPACITY);
                TweenUtils.FadeTo(this.blackPanel.gameObject, BLACK_OPACITY, duration);
            }
        }

        private void FadeOut()
        {
            float duration = this.showType == TRANSITION_TYPE.ZOOM ? this.DURATION_ZOOM_HIDE : this.DURATION_MOVE_HIDE;
            if (this.isFadeBackground)
            {
                var canvasGroup = this.background.GetComponent<CanvasGroup>();
                if (canvasGroup == null)
                {
                    canvasGroup = this.background.AddComponent<CanvasGroup>();
                }

                canvasGroup.DOFade(0.0f, duration + 0.2f);
            }

            if (this.blackPanel)
            {
                TweenUtils.FadeOut(this.blackPanel.gameObject, duration);
            }
        }

        public void OnClose(Button btn, Action callback)
        {
            //if (btn)
            //{
            //    //btn.interactable = false;
            //}

            this.hide(() =>
            {
                //if (btn)
                //{
                //    btn.interactable = true;
                //}

                if (callback != null)
                {
                    callback();
                }
            });
        }

        public virtual void OnBtnCloseClicked(Button btn)
        {
            this.OnClose(btn, null);
        }

        public void setDestroyWhenHide(bool isDestroyWhenHide = true)
        {
            this.isDestroyWhenHide = isDestroyWhenHide;
        }

    }
}