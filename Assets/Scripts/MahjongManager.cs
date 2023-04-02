using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Manager.Game;
using Manager.Bar;
using Slot;
using Classification;
using Tile;
using Asyncoroutine;
namespace Manager.Mahjong 
{
    public class MahjongManager : MonoBehaviour
    {
        public static MahjongManager Instance;
        public Transform CurrentMahjongContainer;
        private void Awake() {
            Instance = this;
        }
        public async void MahjongLogic(MahjongTile currentMahjongTile)
        {
            foreach (Transform item in BarManager.Instance.BarContainer)
                {
                    Slots slot = item.GetComponent<Slots>();
                    if(slot.TileMahjong != null)
                    {
                        if(slot.TileMahjong.typeM == currentMahjongTile.typeM) //check if there's a 1 same tile
                        {
                            List<MahjongTile> listMajongSameTiles = new List<MahjongTile>();
                            listMajongSameTiles.Add(slot.TileMahjong);
                            Slots slot2 = BarManager.Instance.BarContainer.GetChild(item.GetSiblingIndex() + 1).GetComponent<Slots>(); 
                            if(slot2.TileMahjong != null) 
                            {
                                if(slot2.TileMahjong.typeM == currentMahjongTile.typeM) //check if there's another 2 same tile
                                {
                                    listMajongSameTiles.Add(slot2.TileMahjong);
                                    List<MahjongTile> listMajongRemainingTiles = BarManager.Instance.
                                                                                 GetRemainingTiles(slot2.transform.GetSiblingIndex() + 1);
                                    Slots emptySlot = BarManager.Instance.BarContainer.
                                                      GetChild(slot2.transform.GetSiblingIndex() + 1).GetComponent<Slots>();
                                    GameManager.Instance.GlobalMahjongAbleToInteract = false;
                                    if(listMajongRemainingTiles.Count != 0)
                                        BarManager.Instance.MoveTiles(listMajongRemainingTiles);
                                    await emptySlot.AttachTile(currentMahjongTile,true); 
                                    listMajongSameTiles.Add(currentMahjongTile);
                                    BarManager.Instance.ThreeSameTilesFound(listMajongSameTiles,listMajongRemainingTiles);
                                    break;
                                }
                                else 
                                {
                                    List<MahjongTile> listMajongRemainingTiles = BarManager.Instance.
                                                                                 GetRemainingTiles(slot2.transform.GetSiblingIndex());
                                    if(listMajongRemainingTiles.Count != 0)
                                    {
                                        BarManager.Instance.MoveTiles(listMajongRemainingTiles);
                                        await slot2.AttachTile(currentMahjongTile,true); 
                                        GameManager.Instance.CheckTilesUnCleared();
                                        break;
                                    }
                                    
                                }
                            }
                        }
                    }
                    else 
                    {
                        await slot.AttachTile(currentMahjongTile,true); 
                        GameManager.Instance.CheckTilesUnCleared();
                        break;
                    }
                    
                }
        }
    }

}
