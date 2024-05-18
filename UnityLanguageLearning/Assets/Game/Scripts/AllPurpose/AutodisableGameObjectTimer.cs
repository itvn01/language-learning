using UnityEngine;
using System.Collections;

namespace M1PetGame
{
    public class AutodisableGameObjectTimer : MonoBehaviour
    {
        public float WaitTime;
        private float countdown;
        private bool firstCall = true;

        void Start()
        {
            countdown = WaitTime;
        }

        void Update()
        {
            if (firstCall)
            {
                firstCall = false;
                return;
            }
            countdown -= Time.deltaTime;
            if (countdown <= 0)
            {
                //reset variables, so this component can be re-used
                firstCall = true;
                countdown = WaitTime;

                //disable the gameobject
                this.gameObject.SetActive(false);
            }
        }

        public void InstantDisable()
        {
            firstCall = true;
            countdown = WaitTime;
            this.gameObject.SetActive(false);
        }
    }

}