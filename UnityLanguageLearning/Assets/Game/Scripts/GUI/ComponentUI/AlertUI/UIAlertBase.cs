using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

namespace M1PetGame
{
    public class UIAlertBase : UIPopupBase
    {
        [SerializeField] public Button btnLeft;
        [SerializeField] public Button btnRight;
        [SerializeField] public Text txtTitle;
        [SerializeField] public Text txtContent;
        [SerializeField] public Text txtLeft;
        [SerializeField] public Text txtRight;
        Action _acLeftCalback;
        Action _acRightCalback;

        bool _isConfirmed = false;
        private int _tag = 0;
        public override void Start()
        {
            base.Start();
            if (this.btnLeft) this.btnLeft.onClick.AddListener(this.OnBtnLeftClicked);
            if (this.btnRight) this.btnRight.onClick.AddListener(this.OnBtnRightClicked);
        }

        public void SetTitleAndContent(string title, string content)
        {
            if (this.txtTitle && title.Length > 0)
            {
                this.txtTitle.text = title;
            }

            if (this.txtContent)
            {
                this.txtContent.text = content;
            }
        }

        public void SetTextLeftNRight(string leftStr, string rightStr)
        {
            if (this.txtLeft)
            {
                this.txtLeft.text = leftStr;
            }

            if (this.txtRight)
            {
                this.txtRight.text = rightStr;
            }
        }

        public void SetPopupPosition(Vector3 position)
        {
            this.background.transform.position = position;
        }

        public void OnBtnLeftClicked()
        {
            if (this._isConfirmed) return;
            this._isConfirmed = true;

            this.OnClose(null, () =>
            {
                if (this._acLeftCalback != null)
                {
                    this._acLeftCalback();
                }
                this._isConfirmed = false;
            });
        }

        public void OnBtnRightClicked()
        {
            if (this._isConfirmed) return;
            this._isConfirmed = true;

            this.OnClose(null, () =>
            {
                if (this._acRightCalback != null)
                {
                    this._acRightCalback();
                }
                this._isConfirmed = false;
            });
        }

        public void SetLeftCallback(Action callback)
        {
            this._acLeftCalback = callback;
        }

        public void SetRightCallback(Action callback)
        {
            this._acRightCalback = callback;
        }

        public int alertTag
        {
            get
            {
                return this._tag;
            }

            set
            {
                this._tag = value;
            }
        }

        public void SetBtnLeftActive(bool isActive)
        {
            this.btnLeft.gameObject.SetActive(isActive);
        }

        public void SetBtnRightActive(bool isActive)
        {
            this.btnRight.gameObject.SetActive(isActive);
        }

    }
}
