using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


/// <summary>
/// Hero item mono behaviour
/// </summary>
public class HeroItem : MonoBehaviour
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
}
