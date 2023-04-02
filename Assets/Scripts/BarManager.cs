using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Manager.Game;
using Manager.Mahjong;
using Manager.Audio;
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
        public async void MoveTiles(List<MahjongTile> listMahjongTiles)
        {
            GameManager.Instance.GlobalMahjongAbleToInteract = false;
            foreach (MahjongTile item in listMahjongTiles)
            {
                if(item.transform.parent.GetSiblingIndex() == BarContainer.transform.childCount - 1)
                    break;
                int currentSlotIndex = item.transform.parent.GetSiblingIndex() ;
                int nextSlotIndex = item.transform.parent.GetSiblingIndex() + 1;
                Slots newSlotToTransfer = BarContainer.transform.GetChild(nextSlotIndex).GetComponent<Slots>();
                Slots currentSlot = BarContainer.transform.GetChild(currentSlotIndex).GetComponent<Slots>();
                currentSlot.TileMahjong = null;
                // await new WaitForSeconds(0.2f);
                newSlotToTransfer.AttachTile(item,true);
                
            }
            await new WaitForSeconds(0.1f);
            GameManager.Instance.GlobalMahjongAbleToInteract = true;
        }
        public async void ResetTilesPosition(List<MahjongTile> listMahjongTiles)
        {
            // foreach (Transform item in BarContainer.transform)
            // {
            //     Slots slot = item.GetComponent<Slots>();
            //     slot.TileMahjong = null;
            // }
            Debug.Log("reseting tileposition");
            int attachedIndex = 0;
            foreach (MahjongTile m in listMahjongTiles)
                m.transform.parent.GetComponent<Slots>().TileMahjong = null;
            await new WaitForSeconds(0.61f);
            AudioManager.Instance.UpSound.Play();
            for (int i = 0; i < BarContainer.transform.childCount; i++)
            {
                Slots slot = BarContainer.transform.GetChild(i).GetComponent<Slots>();
                if(slot.TileMahjong == null)    
                {
                    slot.AttachTile(listMahjongTiles[attachedIndex],false);
                    attachedIndex++;
                }
                if(listMahjongTiles.Count == attachedIndex ) break;
            }
            GameManager.Instance.GlobalMahjongAbleToInteract = true;
            GameManager.Instance.CheckTilesCleared();
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
        public async void DurationSetMahjongTileVisibility(bool visible, GameObject g)
        {
            await new WaitForSeconds(0.6f);
            g.gameObject.SetActive(visible);
        }
        public async Task ThreeSameTilesFound(List<MahjongTile> listMahjongTiles, List<MahjongTile> remainingTiles)
        {
            Debug.Log("Same three Tiles!!");
            await new WaitForSeconds(0.2f);
            AudioManager.Instance.ExplodeSound.Play();
            foreach (MahjongTile item in listMahjongTiles)
            {
                item.ExplosionPS.Play();
                Slots slot = item.transform.parent.GetComponent<Slots>();
                slot.TileMahjong = null;
                DurationSetMahjongTileVisibility(false,item.gameObject);
            }
            if(remainingTiles.Count > 0)    
            {
                ResetTilesPosition(remainingTiles);

            }
            else 
            {
                GameManager.Instance.GlobalMahjongAbleToInteract = true;
                GameManager.Instance.CheckTilesCleared();
            }
              
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
