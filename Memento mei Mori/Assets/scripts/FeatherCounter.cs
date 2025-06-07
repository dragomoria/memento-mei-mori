using UnityEngine;
using TMPro;

public class FeatherCounter : MonoBehaviour
{

    public static FeatherCounter instance;

    public TMP_Text featherText;
    public int currentFeathers = 50;

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        featherText.text = "Feathers: " + currentFeathers.ToString();
    }

    public void IncreaseFeathers(int v)
    {
        currentFeathers += v;
        featherText.text = "Feathers: " + currentFeathers.ToString();
    }

    public void DecreaseFeathers(int v)
    {
        currentFeathers -= v;
        featherText.text = "Feathers: " + currentFeathers.ToString();
    }
}
