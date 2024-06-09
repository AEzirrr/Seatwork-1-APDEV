using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class CardManager : MonoBehaviour
{
    private string cardsFolder = "Assets/Card Prefabs";
    public List<GameObject> loadedCards = new List<GameObject>();
    public List<GameObject> randomizedDeck = new List<GameObject>();
    [SerializeField] private TapReceiver tapReceiver;

    void Start()
    {
        LoadPrefabsFromFolder(cardsFolder);
        DeckRandomizer(loadedCards);
        tapReceiver.SetRandomizedDeck(randomizedDeck);
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
}
