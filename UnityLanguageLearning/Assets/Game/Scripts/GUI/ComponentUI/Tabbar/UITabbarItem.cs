using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

namespace M1PetGame
{
    public class UITabbarItem : MonoBehaviour
    {
        [SerializeField] Image imgBg;
        [SerializeField] Image imgIcon;
        [SerializeField] Text textTitle;
        [SerializeField] Sprite sprNormal;
        [SerializeField] Sprite sprChoose;
        [SerializeField] Sprite sprIconNormal;
        [SerializeField] Sprite sprIconChoose;
        public int index;
        public bool isEnable = true;
        public bool isChangeTextColor = false;
        public Color colorTextNormal = Color.white;
        public Color colorTextChoose = Color.black;
        public Color colorEnable = Color.white;
        public Color colorDisable = Color.black;

        public Action<int> acTabClick;

        public virtual void OnTabClicked()
        {
            if (this.acTabClick != null && isEnable)
            {
                this.acTabClick(this.index);
            }
        }

        public virtual void SetChoose(bool isChoose)
        {
            // Debug.Log("SetChoose Index = " + index + " ==> " + isChoose);
            if (!isEnable)
                return;

            if (isChoose)
            {
                if (this.imgIcon && sprIconChoose)
                {
                    this.imgIcon.sprite = sprIconChoose;
                    this.imgIcon.SetNativeSize();
                }

                if (this.imgBg && sprChoose)
                {
                    this.imgBg.sprite = sprChoose;
                }

            }
            else
            {
                if (this.imgIcon && sprIconNormal)
                {
                    this.imgIcon.sprite = sprIconNormal;
                    this.imgIcon.SetNativeSize();
                }

                if (this.imgBg && sprNormal)
                {
                    this.imgBg.sprite = sprNormal;
                }
            }

            if (this.isChangeTextColor)
            {
                this.textTitle.color = isChoose ? colorTextChoose : colorTextNormal;
            }
        }

        public virtual void SetTabbarEnable(bool isEnable)
        {
            this.isEnable = isEnable;
            this.imgIcon.color = isEnable ? colorEnable : colorDisable;
            this.textTitle.color = this.imgIcon.color;
        }
    }
}

