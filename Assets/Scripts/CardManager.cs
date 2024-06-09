using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class CardManager : MonoBehaviour
{
    private string cardsFolder = "Assets/Card Prefabs";
    public List<GameObject> loadedCards = new List<GameObject>();
    public List<GameObject> randomizedDeck = new List<GameObject>();
    [SerializeField] private TapReceiver tapReceiver;
    [SerializeField] private Transform[] tableauPiles; // Array to hold the positions for the piles

    void Start()
    {
        LoadPrefabsFromFolder(cardsFolder);
        DeckRandomizer(loadedCards);
        tapReceiver.SetRandomizedDeck(randomizedDeck);
        DealTableau(); // Calling method to deal the cards
        Debug.Log("Total Cards Loaded: " + randomizedDeck.Count);
        for (int i = 0; i < randomizedDeck.Count; i++)
        {
            Debug.Log(randomizedDeck[i].name);
        }
    }

    void LoadPrefabsFromFolder(string path)
    {
        // Load all prefabs in the specified folder
        string[] prefabGuids = AssetDatabase.FindAssets("t:Prefab", new[] { path });
        foreach (string guid in prefabGuids)
        {
            string assetPath = AssetDatabase.GUIDToAssetPath(guid);
            GameObject prefab = AssetDatabase.LoadAssetAtPath<GameObject>(assetPath);
            if (prefab != null)
            {
                loadedCards.Add(prefab);
            }
        }

        Debug.Log("All Cards loaded");
    }

    void DeckRandomizer(List<GameObject> cardsFolder)
    {
        // Initialize the randomizedDeck list from the provided cardsFolder list
        randomizedDeck = new List<GameObject>(cardsFolder);

        // Fisher-Yates shuffle algorithm
        for (int i = randomizedDeck.Count - 1; i > 0; i--)
        {
            int randomIndex = Random.Range(0, i + 1);
            // Swap the elements
            GameObject temp = randomizedDeck[i];
            randomizedDeck[i] = randomizedDeck[randomIndex];
            randomizedDeck[randomIndex] = temp;
        }
    }

    void DealTableau()
    {
        int cardIndex = 0;

        for (int i = 0; i < 7; i++)
        {
            for (int j = 0; j <= i; j++)
            {
                // Instantiate card from deck
                GameObject card = Instantiate(randomizedDeck[cardIndex], 
                    tableauPiles[i].position + new Vector3(0, -j * 0.8f, -j * 0.02f), 
                    Quaternion.identity);

                // Remove card from deck
                randomizedDeck.RemoveAt(cardIndex);

                // Set card face-up for top card, face-down for others
                CardProperty cardProperty = card.GetComponent<CardProperty>();
                if (j == i)
                {
                    cardProperty.SetFaceUp(true);
                }
                else
                {
                    cardProperty.SetFaceUp(false);
                }

                // Assign card to the appropriate pile
                card.transform.SetParent(tableauPiles[i]);
            }
        }
    }

}
