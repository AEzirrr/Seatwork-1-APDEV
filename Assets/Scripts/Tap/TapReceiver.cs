using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using UnityEngine;

public class TapReceiver : MonoBehaviour
{
    // Template GameObject to be instantiated on tap
    [SerializeField] private Transform spawnPos;

    private List<GameObject> randomizedDeck;
    private int currentIndex = 0; 
    private GameObject previousCard; 

    public void SetRandomizedDeck(List<GameObject> deck)
    {
        this.randomizedDeck = deck;
    }

    // Start is called before the first frame update
    void Start()
    {
        GestureManager.Instance.OnTap += this.OnTap;
    }

    private void OnDisable()
    {
        GestureManager.Instance.OnTap -= this.OnTap;
    }

    public void OnTap(object sender, TapEventArgs args)
    {

        if (args.HitObject == null)
        {
            Debug.Log("TapReceiver: OnTap called");

            if (randomizedDeck != null && randomizedDeck.Count > 0)
            {
                if (previousCard != null)
                {
                    SpawnReceiver spawnReceiver = previousCard.GetComponent<SpawnReceiver>();

                    if (!spawnReceiver.IsDocked())
                    {
                        // !! Not sure if this is needed but idk
                        // Set the previous card back to face-down before destroying it
                        //CardProperty prevCardProperty = previousCard.GetComponent<CardProperty>();
                        //if (prevCardProperty != null)
                        //{
                        //    prevCardProperty.SetFaceUp(false);
                        //}
                        //else
                        //{
                        //    Debug.LogWarning("CardProperty component not found on previous card!");
                        //}
                        Destroy(previousCard);
                    }
                    else
                    {
                        Debug.Log("Card is docked");
                    }
                }


                GameObject template = randomizedDeck[currentIndex];
                GameObject instance = Instantiate(template, spawnPos.position, Quaternion.identity);
                
                // Set card to be face-up
                CardProperty cardProperty = instance.GetComponent<CardProperty>();
                if (cardProperty != null)
                {
                    cardProperty.SetFaceUp(true);
                }
                else
                {
                    Debug.LogWarning("CardProperty component not found on instantiated card!");
                }

                instance.SetActive(true);
                previousCard = instance;
            }
        }
        currentIndex = (currentIndex + 1) % randomizedDeck.Count;
    }

}
