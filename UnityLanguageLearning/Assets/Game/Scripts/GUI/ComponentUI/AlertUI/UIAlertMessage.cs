using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace UMLanguage
{
    public class UIAlertMessage : UIAlertBase
    {
        public void ShowAlert(string title, string content)
        {
            this.SetTitleAndContent(title, content);
            this.ShowAlert();
        }

        public void ShowAlert(string content)
        {
            this.SetTitleAndContent("", content);
            this.ShowAlert();
        }

        public void ShowAlert()
        {
            base.Show();
        }
    }
}
