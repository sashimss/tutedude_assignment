using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ContainerScript : MonoBehaviour
{
    public List<NetworkScript> network;
    public List<GameObject> user_profiles;
    public NetworkScript player;
    public GameObject profile_prefab;
    public int page = 0;
    public float x_padding=5f;
    public Button left, right;
    float y_pos;

    float profile_width;
    
    void Start()
    {
        // Get width of profile and calculate ypos, also add player to the profile display container
        profile_width = profile_prefab.GetComponent<RectTransform>().sizeDelta.x+x_padding;
        y_pos = Screen.height/2-profile_prefab.GetComponent<RectTransform>().sizeDelta.y/2;
        AddProfile(player);
    }

    void Update(){
        // Get number of profiles to be displayed in this page
        int profiles_in_this_page = 6*page<=user_profiles.Count-6? 6 : user_profiles.Count%6;
        
        // Show the buttons to turn the page if more than 6 profiles are selected
        if (user_profiles.Count > 6){
            left.gameObject.SetActive(true);
            right.gameObject.SetActive(true);
        } else {
            left.gameObject.SetActive(false);
            right.gameObject.SetActive(false);      
        }

        // disable left button of first page and right button on last page
        left.interactable = page>0;
        right.interactable = page<=user_profiles.Count/6;

        // Iterate through all the profiles, enable the ones on the page and disable the other
        // Calculate the x position for each profile display
        for (int i = 0; i < user_profiles.Count; i++){
            if (i >= 6*page && i < 6*page+profiles_in_this_page){
                user_profiles[i].SetActive(true);
                if (!user_profiles[i].GetComponent<ProfileScript>().fullscreen){
                    RectTransform rt = user_profiles[i].GetComponent<RectTransform>();
                    float x_pos = profiles_in_this_page==1? 0 : Mathf.Lerp(-profile_width*(profiles_in_this_page-1)/2, profile_width*(profiles_in_this_page-1)/2, (i-6*page) /(profiles_in_this_page-1f));
                    rt.anchoredPosition = new Vector2(x_pos,rt.anchoredPosition.y);
                }
            } else {
                user_profiles[i].SetActive(false);
            }
        }
    }

    // This function adds a profile to the container
    public void AddProfile(NetworkScript profile)
    {
        if (!network.Contains(profile)){
            network.Add(profile);
            GameObject user_profile = Instantiate(profile_prefab, transform);
            user_profile.GetComponent<ProfileScript>().profile = profile;
            user_profile.GetComponent<RectTransform>().anchoredPosition = Vector2.up * y_pos;
            user_profiles.Add(user_profile);
        }
    }
    
    // This function removes a profile from the container
    public void RemoveProfile(NetworkScript profile)
    {
        network.Remove(profile);
        foreach(GameObject user_profile in user_profiles){
            if (user_profile.GetComponent<ProfileScript>().profile == profile){
                user_profiles.Remove(user_profile);
                Destroy(user_profile);
                break;
            }
        }
    }

    // Turn the page left
    public void Left(){
        page -= 1;
    }

    // Turn the page right
    public void Right(){
        page += 1;
    }
}
