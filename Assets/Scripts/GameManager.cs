using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Manager.Mahjong;
using Manager.Bar;
using Classification;
namespace Manager.Game 
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance;
        [Header("Mahjong List")]
        public Dictionary<MahjongType,Sprite> MahjongDictionarySprite = new Dictionary<MahjongType, Sprite>();
        public List<Sprite> MahjongSpriteTiles = new List<Sprite>();
        [Header("Mahjong Dotweens")]
        public Ease GlobalTileEaseAttachment;
        public Ease GlobalTileEaseInteraction;
        public float GlobalTileDuration;
        public Vector3 GlobalTileSizeEnter;
        public Vector3 GlobalTileSizeLeave;
        public Vector3 GlobalTileSizeDown;
        public Vector3 TileToSlotAutoPos;
        public Vector3 TileToSlotAutoScale;
        
        private void Awake() {
            Instance = this;
        }
        void Start()
        {
            Initialization();
        }
        public void Initialization()
        {
            MahjongDictionarySprite.Add(MahjongType.Circle, MahjongSpriteTiles[0]);
            MahjongDictionarySprite.Add(MahjongType.House, MahjongSpriteTiles[1]);
            MahjongDictionarySprite.Add(MahjongType.Straight, MahjongSpriteTiles[2]);
        }
        public void SetTileType(MahjongType mahjongType, SpriteRenderer spriteRenderer)
        {
            spriteRenderer.sprite = MahjongDictionarySprite[mahjongType];
        }
        // Update is called once per frame
        void Update()
        {
            
        }
    }

}
