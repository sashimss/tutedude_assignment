using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProfileScript : MonoBehaviour
{
    public NetworkScript profile; 
    public float transition_speed = 5;

    RectTransform rectTransform;
    public bool fullscreen = false;
    float orig_y, orig_x, orig_h, orig_w;
    float width, height;
    // Start is called before the first frame update
    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        GetComponent<Image>().sprite = profile.avatar;
        orig_y = rectTransform.anchoredPosition.y;
        orig_x = rectTransform.anchoredPosition.x;
        orig_h = rectTransform.sizeDelta.y;
        orig_w = rectTransform.sizeDelta.x;
    }

    // Update is called once per frame
    void Update()
    {
        float x, y;
        // Set position according to fullscreen mode
        x = Mathf.Lerp(rectTransform.anchoredPosition.x, fullscreen? 0 : orig_x, transition_speed * Time.deltaTime);
        y = Mathf.Lerp(rectTransform.anchoredPosition.y, fullscreen? 0 : orig_y, transition_speed * Time.deltaTime);
        rectTransform.anchoredPosition = new Vector2(x, y);

        // Set scale according to fullscreen mode
        height = Mathf.Lerp(rectTransform.sizeDelta.y, fullscreen?Screen.height : orig_h, transition_speed * Time.deltaTime);
        width = Mathf.Lerp(rectTransform.sizeDelta.x, fullscreen?Screen.height:orig_w, transition_speed * Time.deltaTime);
        rectTransform.sizeDelta = new Vector2(width, height);
    }

    public void Transition(){
        fullscreen = !fullscreen;
    }
}
