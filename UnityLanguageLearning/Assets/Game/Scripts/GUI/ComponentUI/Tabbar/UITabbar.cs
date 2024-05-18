using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
namespace M1PetGame
{
    public class UITabbar : MonoBehaviour
    {
        [SerializeField] Transform tabbarContainer;
        public int startIndex = 0;
        public Action<int> acTabClick;
        public bool isSetStartIndex = true;
        protected int _currentIndex = -1;
        protected List<UITabbarItem> listTabItems = new List<UITabbarItem>();
        protected virtual void Awake()
        {
            if (tabbarContainer == null)
                tabbarContainer = transform;

            foreach (Transform child in tabbarContainer)
            {
                var comp = child.GetComponent<UITabbarItem>();
                if (comp)
                {
                    listTabItems.Add(comp);
                    comp.acTabClick = this.OnTabItemClicked;
                }
            }

            if (this.isSetStartIndex)
            {
                this.OnTabItemClicked(startIndex);
            }
        }

        public virtual void OnTabItemClicked(int index)
        {
            // Debug.Log("OnTabItemClicked = " + index + " | " + this._currentIndex + " | " + transform.childCount);
            if (index >= transform.childCount || index < 0) return;
            if (this._currentIndex == index) return;
            for (int i = 0; i < listTabItems.Count; i++)
            {
                var item = listTabItems[i];
                if (item.index == index)
                {
                    item.SetChoose(true);
                }
                else
                {
                    item.SetChoose(false);
                }
            }
            this._currentIndex = index;

            if (this.acTabClick != null)
            {
                this.acTabClick(index);
            }
        }

        public virtual void clearDataAndActive()
        {
            this._currentIndex = -1;
            this.OnTabItemClicked(startIndex);
        }

        public virtual void setTabIndex(int index)
        {
            // Debug.LogError("setTabIndex => " + index);
            this._currentIndex = -1;
            this.OnTabItemClicked(index);
        }

        public virtual void clearChooseTab()
        {
            this._currentIndex = -1;
            for (int i = 0; i < listTabItems.Count; i++)
            {
                listTabItems[i].SetChoose(false);
            }
        }

    }
}

