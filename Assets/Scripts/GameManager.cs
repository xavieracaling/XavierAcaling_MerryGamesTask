using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Manager.Mahjong;
using Manager.Bar;
using Classification;
using UnityEngine.UI;
using Slot;
using Tile;
using Asyncoroutine;
using Manager.Audio;
namespace Manager.Game 
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField]
        private List<MahjongTile> AllMahjongTiles = new List<MahjongTile>();
        public static GameManager Instance;
        [Header("Status Game")]
        public Text StatusGameUI;  
        public Text StatusSubGameUI; 
        
        [Header("Mahjong List")]
        public Dictionary<MahjongType,Sprite> MahjongDictionarySprite = new Dictionary<MahjongType, Sprite>();
        public List<Sprite> MahjongSpriteTiles = new List<Sprite>();
        [Header("Mahjong Dotweens")]
        public Ease GlobalTileEaseAttachment;
        public Ease GlobalTileEaseInteraction;
        public float GlobalTileDuration;
        public Vector3 GlobalTileSizeEnter;
        public Vector3 GlobalTileSizeLeave;
        public Vector3 GlobalTileSizeDown;
        public Vector3 TileToSlotAutoPos;
        public Vector3 TileToSlotAutoScale;
        public bool GlobalMahjongAbleToInteract;
        public List<GameObject> ListOfMahjongContainers = new List<GameObject>();        
        private void Awake() {
            Instance = this;
            GlobalMahjongAbleToInteract = true;
        }
        void Start()
        {
            Initialization();
        }
        public void Initialization()
        {
            MahjongDictionarySprite.Add(MahjongType.Circle, MahjongSpriteTiles[0]);
            MahjongDictionarySprite.Add(MahjongType.House, MahjongSpriteTiles[1]);
            MahjongDictionarySprite.Add(MahjongType.Straight, MahjongSpriteTiles[2]);
            MahjongInitialize();
        }
        public void SetTileType(MahjongType mahjongType, SpriteRenderer spriteRenderer)
        {
            spriteRenderer.sprite = MahjongDictionarySprite[mahjongType];
        }
        public void SetStatusGameText(string head, string sub)
        {
            StatusGameUI.text = head;
            StatusSubGameUI.text = sub;
        }
        public void GameResult(bool winGame)
        {
            GameStatus.SetLastStatus(winGame);
            GlobalMahjongAbleToInteract = false;
            switch(GameStatus.LastStatus)
            {
                case true:
                GameStatus.SetLevelGame();
                SetStatusGameText("GREAT JOB.","PROCEED TO NEXT LEVEL");
                PlayableDirectorScene.Instance.NotShowGame.Play();
                PlayableDirectorScene.Instance.StatusShow.Play();
                break;

                case false:
                SetStatusGameText("LOST.","TRY AGAIN");
                PlayableDirectorScene.Instance.NotShowGame.Play();
                PlayableDirectorScene.Instance.StatusShow.Play();

                break;
            }
            
        }
        public void AddListMahjongTile(MahjongTile mahjongTile) => AllMahjongTiles.Add(mahjongTile);
        public List<MahjongTile> GetListMahjongTile() => AllMahjongTiles;  
        public void ClearMahjongTile() => AllMahjongTiles.Clear();
        public void ContinueGame()
        {
            GlobalMahjongAbleToInteract = true;
            PlayableDirectorScene.Instance.StatusLeave.Play();
            PlayableDirectorScene.Instance.ShowGame.Play();
            switch(GameStatus.LastStatus)
            {
                case true:
                    MahjongInitialize();
                break;

                case false:
                    RetryGame();
                break;
            }
        }
        public void RetryGame()
        {
            ClearBar();
            ResetMahjongTiles();
        }
        
        public  async void ResetMahjongTiles()
        {
            GlobalMahjongAbleToInteract = false;
            await new WaitForSeconds(1.2f);
            foreach (MahjongTile item in AllMahjongTiles)
            {
                if(item.transform.parent.GetComponent<Slots>() != null)
                {
                    item.AbleToInteract = true;
                    item.gameObject.SetActive(true);
                    item.transform.SetParent(MahjongManager.Instance.CurrentMahjongContainer);
                    item.transform.DOLocalJump(item.DesignatedLocalPosition,5f,0,0.2f).SetEase(Ease.Linear);
                    AudioManager.Instance.UpSound.Play();
                    await new WaitForSeconds(0.2f);
                }
                
            }
            GlobalMahjongAbleToInteract = true;
        }
        public void CheckTilesCleared()
        {
            if(MahjongManager.Instance.CurrentMahjongContainer.childCount == 0)
                GameResult(true);
        }
        public void CheckTilesUnCleared()
        {
            if(BarManager.Instance.BarContainer.GetChild(4).GetComponent<Slots>().TileMahjong != null)
                GameResult(false);
        }
        public void ClearBar()
        {
            foreach (Transform item in BarManager.Instance.BarContainer)
            {
                Slots slot = item.GetComponent<Slots>();
                if(slot.TileMahjong != null)
                    slot.TileMahjong = null;
            }
        }
        public void MahjongInitialize()
        {
            foreach (GameObject item in ListOfMahjongContainers)
                item.gameObject.SetActive(false);
            ListOfMahjongContainers[GameStatus.LevelGame].SetActive(true);
        }
        // Update is called once per frame
        void Update()
        {
            
        }
    }

}
