using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Classification;
using Manager.Game;
using Manager.Bar;
using Manager.Mahjong;
using Manager.Audio;
using DG.Tweening;
using Slot;
namespace Tile 
{
    public class MahjongTile : MonoBehaviour, IPointerDownHandler, IPointerEnterHandler, IPointerExitHandler,IPointerUpHandler
    {
        public MahjongType typeM;
        private SpriteRenderer spriteRenderer;
        public MahjongType TypeM { get => typeM; set => typeM = TypeM;}
        public bool AbleToInteract;
        public bool OnHover;
        public Vector3 DesignatedLocalPosition;
        public ParticleSystem ExplosionPS;

        private void Awake() {
            AbleToInteract = true;
            spriteRenderer = GetComponent<SpriteRenderer>();
        }
        private void OnEnable() {
        }
        private void Start() {
            DesignatedLocalPosition = transform.localPosition;
            gameObject.name = TypeM.ToString();
            GameManager.Instance.AddListMahjongTile(this);                
        }
        public void Tweening(Vector3 pos, float duration, Ease ease) => transform.DOScale(pos,duration).SetEase(ease);
       
        void IPointerDownHandler.OnPointerDown(PointerEventData eventData)
        {
            if(!AbleToInteract || !GameManager.Instance.GlobalMahjongAbleToInteract) return;
            AudioManager.Instance.DownSound.Play();
            Tweening(GameManager.Instance.GlobalTileSizeDown,GameManager.Instance.GlobalTileDuration,GameManager.Instance.GlobalTileEaseInteraction);
        }   

        void IPointerEnterHandler.OnPointerEnter(PointerEventData eventData)
        {
            if(!AbleToInteract ) return;
            AudioManager.Instance.PopSound.Play();
            Tweening(GameManager.Instance.GlobalTileSizeEnter,GameManager.Instance.GlobalTileDuration,GameManager.Instance.GlobalTileEaseInteraction);
            OnHover = true;
        }

        void IPointerExitHandler.OnPointerExit(PointerEventData eventData)
        {
            if(!AbleToInteract ) return;
            Tweening(GameManager.Instance.GlobalTileSizeLeave,GameManager.Instance.GlobalTileDuration,GameManager.Instance.GlobalTileEaseInteraction);
            OnHover = false;
        }
        
        async void IPointerUpHandler.OnPointerUp(PointerEventData eventData)
        {
            if(!AbleToInteract || !GameManager.Instance.GlobalMahjongAbleToInteract) return;
            Tweening(GameManager.Instance.GlobalTileSizeLeave,GameManager.Instance.GlobalTileDuration,GameManager.Instance.GlobalTileEaseInteraction);
            if(OnHover)
            {
                AudioManager.Instance.UpSound.Play();
                AbleToInteract = false;
                MahjongManager.Instance.MahjongLogic(this);
            }
        }
        public void SetMahjongType (MahjongType type)
        {
            TypeM = type;
            GameManager.Instance.SetTileType(type,spriteRenderer);
        }

    }

}
