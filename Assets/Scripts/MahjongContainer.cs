using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Manager.Game;
using Manager.Mahjong;
public class MahjongContainer : MonoBehaviour
{
    private void Awake() {
        if(GameManager.Instance != null)
            GameManager.Instance.GetListMahjongTile().Clear();
    }
    void Start()
    {
        MahjongManager.Instance.CurrentMahjongContainer = transform;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
