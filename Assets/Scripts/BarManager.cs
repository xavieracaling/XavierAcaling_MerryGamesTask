using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Manager.Game;
using Manager.Mahjong;
using Tile;
using Classification;
using Slot;
using System.Threading.Tasks;
namespace Manager.Bar 
{
    public class BarManager : MonoBehaviour
    {
        public Transform BarContainer;
        public static BarManager Instance;
        private void Awake() {
            Instance = this;
        }
        public void MoveTiles(List<MahjongTile> listMahjongTiles)
        {
            foreach (MahjongTile item in listMahjongTiles)
            {
                if(item.transform.parent.GetSiblingIndex() == transform.childCount - 1)
                    break;
                int nextSlotIndex = item.transform.parent.GetSiblingIndex() + 1;
                Slots newSlotToTransfer = BarContainer.transform.GetChild(nextSlotIndex).GetComponent<Slots>();
                newSlotToTransfer.AttachTile(item,false);
            }
        }
        public void ResetTilesPosition(List<MahjongTile> listMahjongTiles)
        {
            for (int i = 0; i < BarContainer.transform.childCount; i++)
            {
                Slots slot = BarContainer.transform.GetChild(i).GetComponent<Slots>();
                if(slot.TileMahjong != null)
                {
                    slot.AttachTile(listMahjongTiles[i],false);
                }
                if(listMahjongTiles.Count == i + 1) break;
            }
        }
        public List<MahjongTile>  GetRemainingTiles(int indexStart)
        {
            List<MahjongTile> listMahjongTiles = new List<MahjongTile>();
            for (int i = indexStart; i < BarContainer.childCount; i++)
            {
                Slots slot = BarContainer.GetChild(i).GetComponent<Slots>();
                if(slot.TileMahjong == null) break;
                listMahjongTiles.Add(slot.TileMahjong);
            }
            return listMahjongTiles;
        }
        public void ThreeSameTilesFound(List<MahjongTile> listMahjongTiles, List<MahjongTile> remainingTiles)
        {
            Debug.Log("Same three Tiles!!");
            foreach (MahjongTile item in listMahjongTiles)
                Destroy(item.gameObject);
            if(remainingTiles.Count > 0)
                ResetTilesPosition(remainingTiles);
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
