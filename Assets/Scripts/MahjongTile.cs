using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Classification;
using Manager.Game;
using DG.Tweening;
namespace Tile 
{
    public class MahjongTile : MonoBehaviour, IPointerDownHandler, IPointerEnterHandler, IPointerExitHandler,IPointerUpHandler
    {
        private MahjongType typeM;
        private SpriteRenderer spriteRenderer;
        public MahjongType TypeM { get => typeM; set => typeM = TypeM;}
        private void Awake() {
            spriteRenderer = GetComponent<SpriteRenderer>();
        }
        public void Tweening(Vector3 pos, float duration, Ease ease) => transform.DOScale(pos,duration).SetEase(ease);
       
        void IPointerDownHandler.OnPointerDown(PointerEventData eventData)
        {
            Tweening(GameManager.Instance.GlobalTileSizeDown,GameManager.Instance.GlobalTileDuration,GameManager.Instance.GlobalTileEase);
        }   

        void IPointerEnterHandler.OnPointerEnter(PointerEventData eventData)
        {
            Tweening(GameManager.Instance.GlobalTileSizeEnter,GameManager.Instance.GlobalTileDuration,GameManager.Instance.GlobalTileEase);
        }

        void IPointerExitHandler.OnPointerExit(PointerEventData eventData)
        {
            Tweening(GameManager.Instance.GlobalTileSizeLeave,GameManager.Instance.GlobalTileDuration,GameManager.Instance.GlobalTileEase);
      
        }

        void IPointerUpHandler.OnPointerUp(PointerEventData eventData)
        {
            Tweening(GameManager.Instance.GlobalTileSizeLeave,GameManager.Instance.GlobalTileDuration,GameManager.Instance.GlobalTileEase);
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
