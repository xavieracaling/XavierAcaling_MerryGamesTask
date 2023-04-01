using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Tile;
using DG.Tweening;
using Manager.Game;
using Asyncoroutine;
using System.Threading.Tasks;
namespace Slot
{
    public class Slots : MonoBehaviour
    {
        public MahjongTile TileMahjong;
        public ParticleSystem SmokeEffect;
        public async Task AttachTile(MahjongTile mahjongTile, bool doLocalJump)
        {
            TileMahjong = mahjongTile;
            mahjongTile.transform.DOKill();
            mahjongTile.transform.SetParent(transform) ;
            mahjongTile.transform.localScale = GameManager.Instance.TileToSlotAutoScale;
            await MoveDoTween(mahjongTile,doLocalJump);

        }
        public async Task MoveDoTween(MahjongTile mahjongTile,bool doLocalJump)
        {
            if(doLocalJump)
                await mahjongTile.transform.DOLocalJump(GameManager.Instance.TileToSlotAutoPos,
                                            8f,0,GameManager.Instance.GlobalTileDuration).
                                            SetEase(GameManager.Instance.GlobalTileEaseAttachment).AsyncWaitForCompletion();
            else 
                await mahjongTile.transform.DOLocalMove(GameManager.Instance.TileToSlotAutoPos,
                                            GameManager.Instance.GlobalTileDuration).
                                            SetEase(GameManager.Instance.GlobalTileEaseAttachment).AsyncWaitForCompletion();
            SmokeEffect.Play();
        }
         void Start()
        {
             
        }

        void Update()
        {
            
        }
    }

}

