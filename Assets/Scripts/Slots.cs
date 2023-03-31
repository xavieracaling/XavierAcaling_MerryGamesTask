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
        public async Task AttachTile(MahjongTile mahjongTile)
        {
            TileMahjong = mahjongTile;
            mahjongTile.transform.DOKill();
            mahjongTile.transform.SetParent(transform) ;
            mahjongTile.transform.localScale = GameManager.Instance.TileToSlotAutoScale;
            await mahjongTile.transform.DOLocalJump(GameManager.Instance.TileToSlotAutoPos,
                                            8f,0,GameManager.Instance.GlobalTileDuration).
                                            SetEase(GameManager.Instance.GlobalTileEaseAttachment).AsyncWaitForCompletion();
        }
        void Start()
        {
            
        }

        void Update()
        {
            
        }
    }

}

