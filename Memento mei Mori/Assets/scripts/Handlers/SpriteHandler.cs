using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.Unity.VisualStudio.Editor;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using Image = UnityEngine.UI.Image;
public class SpriteHandler : MonoBehaviour
{
    GameObject magicAttack;
    GameObject slashAttack;
    GameObject skullAttack;
    public static SpriteHandler instance { get; private set; }


    private int featherIndex = 0;


    //pls set this up
   [SerializeField] private List<Image> featherSprites = new List<Image>();
   [SerializeField] private Sprite emptyFeather;
   [SerializeField] private Sprite fullFeather;
   [SerializeField] private TMP_Text featherText;

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
        skullAttack = GameObject.Find("Skull Attack");
        featherText.text = "Feathers: " + featherIndex.ToString();
        hideAll();
    }

    private void hideAll()
    {
        hideSlash();
        hideMagicAttack();
        hideSkullAttack();
    }


    public void hideSkullAttack()
    {
        if (skullAttack != null)
        {
            skullAttack.SetActive(false);
        }
        else
        {
            Debug.LogWarning("Skull Attack GameObject not found!");
        }
    }
    public void showSkullAttack()
    {
        if (skullAttack != null)
        {
            skullAttack.SetActive(true);
        }
        else
        {
            Debug.LogWarning("Skull Attack GameObject not found!");
        }
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

    public IEnumerator moveSprite(GameObject sprite, Vector3 start, Vector3 end, float duration) // lerp the movement so it is smoooth
    {
        Debug.Log($"moved sprite {sprite} from: {start} to {end} in {duration} s");
        float t = 0;
        while (t < 1f)
        {
            t += Time.deltaTime / duration;
            sprite.transform.position = Vector3.Lerp(start, end, t);
            yield return null;
        }

    }

    public void updateFeatherCounter()
    {
        if (featherIndex++ > 20)
        {
            Debug.LogWarning($"feather index out of bounds: {featherIndex}");
            return;
        }
        featherSprites[featherIndex].sprite = fullFeather;
        featherIndex++;
        featherText.text = "Feathers: " + (featherIndex-1).ToString();
    }

    public void removeLastFeather()
    {
        if (featherIndex < 0)
        {
            Debug.LogWarning($"feather index out of bounds: {featherIndex}");
            return;
        }
        featherSprites[featherIndex - 1].sprite = emptyFeather;
        featherIndex--; 
        featherText.text = "Feathers: " + (featherIndex-1).ToString();

    }
}
