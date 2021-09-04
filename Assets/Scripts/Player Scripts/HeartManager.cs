using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeartManager : MonoBehaviour
{
    public Image[] hearts;
    public Sprite fullHeart;
    public Sprite threeQuarterHeart;
    public Sprite halfFullHeart;
    public Sprite quarterHeart;
    public Sprite emptyHeart;
    public Text currentHealthText;
    public Text maxHealthText;
    public FloatValue maxHealthPlayer;
    public FloatValue currentHealthPlayer;

    // Number of heart icons in the bar (2)
    public FloatValue HeartContainer;

    // Start is called before the first frame update
    void Start()
    {
        InitHeart();
    }

    public void InitHeart()
    {
        // Show Health on UI (First Time)
        currentHealthText.gameObject.SetActive(true);
        currentHealthText.text = currentHealthPlayer.initialValue.ToString();
        maxHealthText.gameObject.SetActive(true);
        maxHealthText.text = "/ " + maxHealthPlayer.initialValue.ToString();

        // Show image of heart
        for (int i = 0; i < HeartContainer.initialValue; i++)
        {
            // Set image active and change sprite to "FullHeart"
            hearts[i].gameObject.SetActive(true);
            hearts[i].sprite = fullHeart;
        }
    }

    public void UpdateHearts()
    {
        // Script: Goes through player health and update UI
        currentHealthText.text = currentHealthPlayer.RuntimeValue.ToString();

        // To update graphics of heart
        for (int i = 0; i < HeartContainer.initialValue; i++)
        {
            if (currentHealthPlayer.RuntimeValue == maxHealthPlayer.initialValue)
            {
                hearts[i].sprite = fullHeart;
                hearts[i].GetComponent<Outline>().enabled = false;
            }
            else if (currentHealthPlayer.RuntimeValue >= (maxHealthPlayer.initialValue * 0.75))
            {
                hearts[i].sprite = threeQuarterHeart;
                hearts[i].GetComponent<Outline>().enabled = false;
            }
            else if (currentHealthPlayer.RuntimeValue >= (maxHealthPlayer.initialValue * 0.5))
            {
                hearts[i].sprite = halfFullHeart;
                hearts[i].GetComponent<Outline>().enabled = false;
            }
            else if (currentHealthPlayer.RuntimeValue >= (maxHealthPlayer.initialValue * 0.25))
            {
                hearts[i].sprite = quarterHeart;
                hearts[i].GetComponent<Outline>().enabled = true;
            }
            else
            {
                hearts[i].sprite = emptyHeart;
                hearts[i].GetComponent<Outline>().enabled = true;
            }
        }

    }
}
