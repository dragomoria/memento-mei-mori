using UnityEngine;
using TMPro;

public class FeatherCounter : MonoBehaviour
{

    public static FeatherCounter instance { get; private set; }

    public TMP_Text featherText;
    public int currentFeathers { get; private set; } = 10;

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
