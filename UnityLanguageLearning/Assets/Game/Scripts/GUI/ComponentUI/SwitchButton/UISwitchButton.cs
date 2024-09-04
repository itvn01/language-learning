using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System;

namespace M1PetGame {
    public class UISwitchButton : MonoBehaviour
    {
        [SerializeField] Button btnSwitch;
        [SerializeField] Image dot;
        [SerializeField] Sprite spf_off;
        [SerializeField] Sprite spf_on;
        [SerializeField] Sprite spf_dot_off;
        [SerializeField] Sprite spf_dot_on;
        [SerializeField] List<GameObject> listTargetNode;
        public float animTime = 0.1f;
        private bool status = true;
        public Action<bool> OnAcSwitched;

        protected void OnEnable() {
            if (this.btnSwitch) 
                this.btnSwitch.onClick.AddListener(this.OnBtnSwitchClicked);
        }

        protected void OnDisable() {
            if (this.btnSwitch)
                this.btnSwitch.onClick.RemoveAllListeners();
        }


        public void InitWithStatus(bool status)
        {
            this.status = status;
            this.dot.transform.position = this.GetDotTarget(this.status);
            this.SetSpriteFrame(status);
        }

        protected void OnBtnSwitchClicked()
        {
            SoundBase.Instance.PlayOneShot(SoundBase.Instance.click);
            this.btnSwitch.interactable = false;
            this.status = !this.status;

            this.dot.transform.DOMove(this.GetDotTarget(this.status), this.animTime).OnComplete(() =>
            {
                this.btnSwitch.interactable = true;
                this.SetSpriteFrame(this.status);
            });

            this.OnAcSwitched?.Invoke(this.status);
        }

        Vector3 GetDotTarget(bool status)
        {
            return status ? this.listTargetNode[1].transform.position : this.listTargetNode[0].transform.position;
        }

        protected void SetSpriteFrame(bool status)
        {
            if (status)
            {
                this.btnSwitch.image.sprite = this.spf_on;
                this.dot.sprite = this.spf_dot_on;
            }
            else
            {
                this.btnSwitch.image.sprite = this.spf_off;
                this.dot.sprite = this.spf_dot_off;
            }
        }

    }
}

