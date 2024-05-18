using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace M1PetGame
{
    public class AdsEmulatorUI : SingletonMB<AdsEmulatorUI>
    {
        [SerializeField] GameObject adsFull;
        [SerializeField] Button btnCloseAds;

        UnityAction success;

        protected override void Awake()
        {
            base.Awake();
            adsFull.SetActive(false);
        }


        private void OnEnable()
        {
            if (this.btnCloseAds) this.btnCloseAds.onClick.AddListener(this.OnBtnCloseAdsClicked);
        }

        private void OnDisable()
        {
            if (this.btnCloseAds) this.btnCloseAds.onClick.RemoveAllListeners();

        }

        public void OnBtnCloseAdsClicked()
        {
            Debug.Log("AdsEmulatorUI Close Ads");
            adsFull.SetActive(false);
            if (this.success != null)
                this.success?.Invoke();
        }

        public void ShowAdsFull(UnityAction success)
        {
            Debug.Log("AdsEmulatorUI ShowAdsFull");
            this.success = success;
            adsFull.SetActive(true);
        }
    }
}
