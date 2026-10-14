using System;
using UnityEngine;
using UnityEngine.UIElements;


public class Home : MonoBehaviour
{
    [SerializeField] private GameManager gameManager;
    public GameObject frog;
    private bool isOccupied;

    public bool IsOccupied => isOccupied;

    private void Awake()
    {
        UpdateSprite();
    }

    private void OnTriggerEnter2D (Collider2D other)
    {
        if (!other.CompareTag("Player"))
        {
            return;
        }

        Frogger player= other.GetComponentInParent<Frogger>();
        if (player == null)
        {
            return;
        }

        if (isOccupied)
        {
            player.Death();
            return;
        }

        isOccupied = true;
        UpdateSprite();

        gameManager.HomeOccupied();
    }

    public void ResetHome()
    {
        isOccupied=false;
        UpdateSprite();
    }

    private void UpdateSprite()
    {
        if (frog != null)
        {
            frog.SetActive(isOccupied);
        }
    }
}
