using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Manager.Audio 
{
    public class AudioManager : MonoBehaviour
    {
        public static AudioManager Instance;
        public AudioSource PopSound;
        public AudioSource DownSound;
        public AudioSource UpSound;
        private void Awake() {
            Instance = this;
        }
        void Start()
        {
            
        }

        // Update is called once per frame
        void Update()
        {
            
        }
    }

}
