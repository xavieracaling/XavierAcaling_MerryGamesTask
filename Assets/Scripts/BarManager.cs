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
                newSlotToTransfer.AttachTile(item);
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
        public void ThreeSameTilesFound(List<MahjongTile> listMahjongTiles)
        {
            Debug.Log("Same three Tiles!!");
            foreach (MahjongTile item in listMahjongTiles)
                Destroy(item.gameObject);
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
