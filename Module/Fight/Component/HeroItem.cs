using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


/// <summary>
/// Hero item (in the select bar) mono behaviour
/// </summary>
public class HeroItem : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
{

    Dictionary<string, string> heroData;

    public void Init(Dictionary<string, string> heroData) {
        this.heroData = heroData;
        gameObject.SetActive(true);
    }

    // Start is called before the first frame update
    void Start() {
        // Start will run after the SetActive
        transform.Find("icon").GetComponent<Image>().SetIcon(heroData["Icon"]); 
        // transform.Find("icon").GetComponent<Image>().sprite = Resources.Load<Sprite>($"Icon/{heroData["Icon"]}");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnBeginDrag(PointerEventData eventData) {
        GameApp.ViewManager.Open(ViewTypes.DragHeroView, heroData["Icon"]);
    }

    public void OnEndDrag(PointerEventData eventData) {
        GameApp.ViewManager.Close((int)ViewTypes.DragHeroView);

        Tools.ScreenPointToRay2D(Camera.main, (Collider2D col) => {
            if (col != null) {
                Block b = col.GetComponent<Block>();
                if (b != null && b.Type != BlockType.Obstacle) { 
                    GameApp.FightManager.AddHero(b, heroData);
                    Destroy(gameObject);
                }
            }
        });

    }

    public void OnDrag(PointerEventData eventData) {
    }
}
