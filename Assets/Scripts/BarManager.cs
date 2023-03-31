using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Manager.Game;
using Manager.Mahjong;
using Tile;
using Classification;
using Slot;
using System.Threading.Tasks;
using Asyncoroutine;
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
                if(item.transform.parent.GetSiblingIndex() == BarContainer.transform.childCount - 1)
                    break;
                int currentSlotIndex = item.transform.parent.GetSiblingIndex() ;
                int nextSlotIndex = item.transform.parent.GetSiblingIndex() + 1;
                Slots currentSlot = BarContainer.transform.GetChild(currentSlotIndex).GetComponent<Slots>();
                currentSlot.TileMahjong = null;
                Slots newSlotToTransfer = BarContainer.transform.GetChild(nextSlotIndex).GetComponent<Slots>();
                newSlotToTransfer.AttachTile(item,true);
            }
        }
        public async void ResetTilesPosition(List<MahjongTile> listMahjongTiles)
        {
            // foreach (Transform item in BarContainer.transform)
            // {
            //     Slots slot = item.GetComponent<Slots>();
            //     slot.TileMahjong = null;
            // }
            Debug.Log("reseting tileposition");
            await new WaitForSeconds(0.17f);
            int attachedIndex = 0;
            foreach (MahjongTile m in listMahjongTiles)
                m.transform.parent.GetComponent<Slots>().TileMahjong = null;
            for (int i = 0; i < BarContainer.transform.childCount; i++)
            {
                Slots slot = BarContainer.transform.GetChild(i).GetComponent<Slots>();
                Debug.Log("getting");
                if(slot.TileMahjong == null)
                {
                    Debug.Log("looking");
                    slot.AttachTile(listMahjongTiles[attachedIndex],false);
                    attachedIndex++;
                }
                if(listMahjongTiles.Count == attachedIndex ) break;
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
        public async Task ThreeSameTilesFound(List<MahjongTile> listMahjongTiles, List<MahjongTile> remainingTiles)
        {
            Debug.Log("Same three Tiles!!");
            await new WaitForSeconds(0.6f);
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
