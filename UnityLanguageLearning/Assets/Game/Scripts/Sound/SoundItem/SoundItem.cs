using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UMLanguage
{
    [Serializable]
    public class SoundItem
    {
        public string name;
        public AudioClip clip;
        [HideInInspector]
        public AudioSource audioSource;
    }
}
