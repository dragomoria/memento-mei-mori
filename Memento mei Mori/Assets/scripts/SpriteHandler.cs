using UnityEngine;

public class SpriteHandler : MonoBehaviour
{
    GameObject magicAttack;
    GameObject slashAttack;
    public static SpriteHandler instance { get; private set; }

    // Start is called once before the first execution of Update after the MonoBehaviour is created

    void Awake()
    {
        instance = this;
        // Ensure that the GameObject is not destroyed when loading a new scene
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        magicAttack = GameObject.Find("Magic Attack");
        slashAttack = GameObject.Find("Slash");
        hideBoth();
    }

    private void hideBoth()
    {
        hideSlash();
        hideMagicAttack();
    }

    public void hideSlash()
    {
        if (slashAttack != null)
        {
            slashAttack.SetActive(false);
        }
        else 
        {
            Debug.LogWarning("Slash Attack GameObject not found!");
        }
    }
    public void showSlash()
    {
        if (slashAttack != null)
        {
            slashAttack.SetActive(true);
        }
        else 
        {
            Debug.LogWarning("Slash Attack GameObject not found!");
        }
    }
    public void hideMagicAttack()
    {
        if (magicAttack != null)
        {
            magicAttack.SetActive(false);
        }
        else 
        {
            Debug.LogWarning("Magic Attack GameObject not found!");
        }
    }
    public void showMagicAttack()
    {
        if (magicAttack != null)
        {
            magicAttack.SetActive(true);
        }
        else 
        {
            Debug.LogWarning("Magic Attack GameObject not found!");
        }
    }



}
