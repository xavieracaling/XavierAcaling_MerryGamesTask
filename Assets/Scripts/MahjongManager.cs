using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Manager.Game;
using Manager.Bar;
namespace Manager.Mahjong 
{
    public class MahjongManager : MonoBehaviour
    {
        public static MahjongManager Instance;
        public Transform MahjongContainer;
        private void Awake() {
            Instance = this;
        }
        void Start()
        {
            
        }

        void Update()
        {
            
        }
    }

}
