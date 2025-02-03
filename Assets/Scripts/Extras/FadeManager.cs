
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class FadeManager : MonoBehaviour
{
    [SerializeField] List<FadeViewContainer>fadeTexts;

    private async void Awake()
    {
        await Task.Delay(500);
        GameService.Instance.startGame += OnGameStart;
        PlayerPrefs.DeleteAll();
    }

    private void Start()
    {
        foreach (var f in fadeTexts)
        {
            f.FadeView.gameObject.SetActive(false);
        }
        
    }

    private FadeView GetFadeView(FadeTextType fadeType)
    {
        for (int i = 0; i < fadeTexts.Count; i++)
        {
            if (fadeTexts[i].fadeTextType == fadeType)
            {
                return fadeTexts[i].FadeView;
            }
        }
        return null;
    }

    public void OnGameStart()
    {
        if(PlayerPrefs.GetInt("FirstTime",1)==1)
        {
            ShowText(FadeTextType.GRAPPLE);
        }
    }

    public void StartTextFading(FadeTextType fadeTextType)
    {
            ShowText(fadeTextType);
            GetFadeView(fadeTextType)?.StartFading();
    }

    public void ShowText(FadeTextType fadeTextType)
    {
        GetFadeView(fadeTextType)?.gameObject.SetActive(true);
    }
}

[Serializable]
public class FadeViewContainer
{
    public FadeTextType fadeTextType;
    public FadeView FadeView;
}

public enum FadeTextType
{
    GRAPPLE,
    SPACE
}
