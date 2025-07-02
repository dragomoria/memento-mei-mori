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

    public void IncreaseFeathers()
    {
        currentFeathers++;

        featherText.text = "Feathers: " + currentFeathers.ToString();
    }

    public void DecreaseFeathers()
    {
        currentFeathers--;

        featherText.text = "Feathers: " + currentFeathers.ToString();
    }
}
