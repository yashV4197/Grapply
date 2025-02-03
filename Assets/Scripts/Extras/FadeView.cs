using TMPro;
using UnityEngine;

public class FadeView: MonoBehaviour
{
    [SerializeField] TextMeshPro fadingText;
    private bool startFading;
    [SerializeField] float timer;
    private void Start()
    {
        startFading = false;
        this.gameObject.SetActive(false);
    }

    private void Update()
    {
        if(startFading)
        {
            if (timer >= 0)
            {
                fadingText.alpha = timer;
                timer -= Time.deltaTime;
            }
            else
            {
                this.gameObject.SetActive(false);
            }
        }
    }

    public void StartFading()
    {
        startFading = true;
    }

}