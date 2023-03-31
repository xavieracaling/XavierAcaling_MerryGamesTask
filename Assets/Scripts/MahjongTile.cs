using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Classification;
using Manager.Game;
using Manager.Bar;
using DG.Tweening;
using Slot;
namespace Tile 
{
    public class MahjongTile : MonoBehaviour, IPointerDownHandler, IPointerEnterHandler, IPointerExitHandler,IPointerUpHandler
    {
        [field:SerializeField]
        private MahjongType typeM;
        private SpriteRenderer spriteRenderer;
        public MahjongType TypeM { get => typeM; set => typeM = TypeM;}
        public bool AbleToInteract;
        public bool OnHover;
        private void Awake() {
            AbleToInteract = true;
            spriteRenderer = GetComponent<SpriteRenderer>();
        }
        public void Tweening(Vector3 pos, float duration, Ease ease) => transform.DOScale(pos,duration).SetEase(ease);
       
        void IPointerDownHandler.OnPointerDown(PointerEventData eventData)
        {
            if(!AbleToInteract) return;
            Tweening(GameManager.Instance.GlobalTileSizeDown,GameManager.Instance.GlobalTileDuration,GameManager.Instance.GlobalTileEaseInteraction);
        }   

        void IPointerEnterHandler.OnPointerEnter(PointerEventData eventData)
        {
            if(!AbleToInteract) return;
            Tweening(GameManager.Instance.GlobalTileSizeEnter,GameManager.Instance.GlobalTileDuration,GameManager.Instance.GlobalTileEaseInteraction);
            OnHover = true;
        }

        void IPointerExitHandler.OnPointerExit(PointerEventData eventData)
        {
            if(!AbleToInteract) return;
            Tweening(GameManager.Instance.GlobalTileSizeLeave,GameManager.Instance.GlobalTileDuration,GameManager.Instance.GlobalTileEaseInteraction);
            OnHover = false;
        }
        
        async void IPointerUpHandler.OnPointerUp(PointerEventData eventData)
        {
            if(!AbleToInteract) return;
            Tweening(GameManager.Instance.GlobalTileSizeLeave,GameManager.Instance.GlobalTileDuration,GameManager.Instance.GlobalTileEaseInteraction);
            if(OnHover)
            {
                AbleToInteract = false;
                foreach (Transform item in BarManager.Instance.BarContainer)
                {
                    Slots slot = item.GetComponent<Slots>();
                    
                    if(slot.TileMahjong != null)
                    {
                        Debug.Log("no null");
                        if(slot.TileMahjong.typeM == typeM) //check if there's a 1 same tile
                        {
                            List<MahjongTile> listMajongSameTiles = new List<MahjongTile>();
                            listMajongSameTiles.Add(slot.TileMahjong);
                            Slots slot2 = BarManager.Instance.BarContainer.GetChild(item.GetSiblingIndex() + 1).GetComponent<Slots>(); 
                            if(slot2.TileMahjong != null) 
                            {
                                if(slot2.TileMahjong.typeM == typeM) //check if there's another 2 same tile
                                {
                                    Debug.Log("the same 3");
                                    listMajongSameTiles.Add(slot2.TileMahjong);
                                    List<MahjongTile> listMajongRemainingTiles = BarManager.Instance.
                                                                                 GetRemainingTiles(slot2.transform.GetSiblingIndex() + 1);
                                    Slots emptySlot = BarManager.Instance.BarContainer.
                                                      GetChild(slot2.transform.GetSiblingIndex() + 1).GetComponent<Slots>();
                                    if(listMajongRemainingTiles.Count != 0)
                                        BarManager.Instance.MoveTiles(listMajongRemainingTiles);
                                    await emptySlot.AttachTile(this); 
                                    listMajongSameTiles.Add(this);
                                    BarManager.Instance.ThreeSameTilesFound(listMajongSameTiles);
                                    break;
                                }
                                else 
                                {
                                    Debug.Log("not the same");
                                    List<MahjongTile> listMajongRemainingTiles = BarManager.Instance.
                                                                                 GetRemainingTiles(slot2.transform.GetSiblingIndex());
                                    if(listMajongRemainingTiles.Count != 0)
                                    {
                                        BarManager.Instance.MoveTiles(listMajongRemainingTiles);
                                        await slot2.AttachTile(this); 
                                        break;
                                    }
                                    
                                }
                            }
                        }
                    }
                    else 
                    {
                        Debug.Log("To attch null");
                        await slot.AttachTile(this); 
                        break;
                    }
                    
                }
                //BarManager.Instance.BarContainer.transform.GetChild(0).GetComponent<Slots>().AttachTile(this);
                
            }
        }
        public void SetMahjongType (MahjongType type)
        {
            TypeM = type;
            GameManager.Instance.SetTileType(type,spriteRenderer);
        }

        // Update is called once per frame
        void Update()
        {
            
        }
    }

}
