using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace M1PetGame
{
    [Serializable]
    public class PetSound
    {
        public string name;
        public AudioClip clip;
        [HideInInspector]
        public AudioSource audioSource;
    }
}
