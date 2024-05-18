using UnityEngine;
using UnityEngine.UI;
using System;
using DG.Tweening;
using UMExtensions;

namespace M1PetGame
{
    [System.Serializable]
    public enum TRANSITION_TYPE
    {
        TOP,
        BOTTOM,
        LEFT,
        RIGHT,
        ZOOM
    }

    public class UIPopupBase : MonoBehaviour
    {
        [SerializeField] public GameObject background;
        [SerializeField] Image blackPanel;
        [SerializeField] public Button btnClose;
        public bool isDestroyWhenHide = false;
        public bool isFadeBackground = false;
        private TRANSITION_TYPE showType = TRANSITION_TYPE.ZOOM;
        private TRANSITION_TYPE hideType = TRANSITION_TYPE.ZOOM;
        public float DURATION_MOVE = 0.3f;
        public float DURATION_ZOOM = 0.3f;
        public float BLACK_OPACITY = 180.0f / 255.0f;
        private bool _isAnimRunning = false;
        private Vector3 _originPosition;

        public virtual void Awake()
        {
            this._originPosition = this.background.transform.position;
            // Debug.LogFormat("_originPosition => {0}", _originPosition.ToString());
        }

        public virtual void Start()
        {
            if (this.blackPanel)
            {
                var btnBlackClose = this.blackPanel.GetComponent<Button>();
                if (btnBlackClose)
                {
                    btnBlackClose.onClick.AddListener(delegate { OnBtnCloseClicked(btnBlackClose); });
                }
            }

            if (this.btnClose)
            {
                btnClose.onClick.AddListener(delegate { OnBtnCloseClicked(btnClose); });
            }
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
                this.background.transform.DOScale(Vector3.one, DURATION_ZOOM).SetEase(Ease.OutBack).OnComplete(() =>
                {
                    this.OnShowComplete(callback);
                });
            }
            else
            {
                this.ShowByMove(() =>
                {
                    this.OnShowComplete(callback);
                });
            }
        }

        public virtual void hide(Action callback)
        {
            if (this._isAnimRunning) return;
            this._isAnimRunning = true;

            if (this.hideType == TRANSITION_TYPE.ZOOM)
            {
                this.background.transform.DOScale(Vector3.one * 0.8f, DURATION_ZOOM).SetEase(Ease.InBack).OnComplete(() =>
                {
                    this.OnHideComplete(callback);
                });
            }
            else
            {
                this.HideByMove(() =>
                {
                    this.OnHideComplete(callback);
                });

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
                Destroy(this.gameObject);
            }
            else
            {
                if (this.blackPanel)
                {
                    this.blackPanel.gameObject.SetActive(false);
                }
                this.gameObject.SetActive(false);
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
            var width = Screen.width;
            var height = Screen.height;

            switch (this.showType)
            {
                case TRANSITION_TYPE.TOP:
                    start_posision = new Vector3(target_position.x, target_position.y + height * 1.5f);
                    break;

                case TRANSITION_TYPE.BOTTOM:
                    start_posision = new Vector3(target_position.x, target_position.y - height * 1.5f);
                    break;

                case TRANSITION_TYPE.LEFT:
                    start_posision = new Vector3(target_position.x - width * 1.5f, target_position.y);
                    break;

                case TRANSITION_TYPE.RIGHT:
                    start_posision = new Vector3(target_position.x + width * 1.5f, target_position.y);
                    break;

                default:
                    start_posision = new Vector3(target_position.x, target_position.y + height * 1.5f);
                    break;
            }

            // Debug.LogFormat("start_posision => {0}", start_posision.ToString());
            this.background.transform.position = start_posision;
            this.background.transform.DOMove(target_position, DURATION_MOVE).OnComplete(() =>
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
            var start_posision = this.background.transform.position;
            var width = Screen.width;
            var height = Screen.height;

            switch (this.hideType)
            {
                case TRANSITION_TYPE.TOP:
                    target_position = new Vector3(start_posision.x, start_posision.y + height * 1.5f);
                    break;

                case TRANSITION_TYPE.BOTTOM:
                    target_position = new Vector3(start_posision.x, start_posision.y - height * 1.5f);
                    break;

                case TRANSITION_TYPE.LEFT:
                    target_position = new Vector3(start_posision.x - width * 1.5f, start_posision.y);
                    break;

                case TRANSITION_TYPE.RIGHT:
                    target_position = new Vector3(start_posision.x + width * 1.5f, start_posision.y);
                    break;

                default:
                    target_position = new Vector3(start_posision.x, start_posision.y + height * 1.5f);
                    break;
            }

            this.background.transform.DOMove(target_position, DURATION_MOVE).OnComplete(() =>
            {
                if (callback != null)
                {
                    callback();
                    this.background.transform.position = start_posision;
                }
            });
        }

        private void FadeIn()
        {
            float duration = this.showType == TRANSITION_TYPE.ZOOM ? this.DURATION_ZOOM : this.DURATION_MOVE;
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
            float duration = this.showType == TRANSITION_TYPE.ZOOM ? this.DURATION_ZOOM : this.DURATION_MOVE;
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