using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using Image = UnityEngine.UI.Image;

public class CutscenePlayer : MonoBehaviour
{
    [SerializeField] public Transform panelParent;
    [SerializeField] public float panelDuration =  10f; 

    [SerializeField] public GameObject button;

    private Image[] panels;
    private int panelIndex = 0;
    private float timer = 0f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        panels = panelParent.GetComponentsInChildren<Image>(true);
        if (button!= null)
            button.SetActive(false);
        showPanel(0);
    }

    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= panelDuration)
        {
            timer = 0f;
            panelIndex++;
            if (panelIndex < panels.Length)
            {
                showPanel(panelIndex);
            }
            else
            {
                EndCutscene();
            }
        }
    }

    private void showPanel(int index)
    {
        for (int i = 0; i < panels.Length; i++)
        {
            panels[i].gameObject.SetActive(i == index);
        }
    }
    private void EndCutscene()
    {
        gameObject.SetActive(false);
        if (button != null)
            button.SetActive(true);
    }

    
}
