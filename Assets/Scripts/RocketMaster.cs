using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketMaster : MonoBehaviour
{
    public static RocketMaster Instance;  
    
    public List<RectTransform> prefab;
    
    public GameObject framePrefab;
    public RectTransform answerContainer;
    public RectTransform playerArea;
    public List<string> toSpell;
    public List<char> charList;

    public float spawnDelay = 1.0f; // Delay between spawns in seconds  

    private Coroutine spawnCoroutine;
    private int index = 0;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        // Start the coroutine to spawn objects with a delay
        spawnCoroutine = StartCoroutine(SpawnRandomObjectsWithDelay());
        SeperateSpelling(index);
    }

    // Update is called once per frame
    void Update()
    {        
        
    }

    private IEnumerator SpawnRandomObjectsWithDelay()
    {
        while (prefab.Count > 0)
        {
            // Get a random object from the list
            int randomIndex = Random.Range(0, prefab.Count);
            RectTransform obj = prefab[randomIndex];

            // Calculate random y position within the player area bounds
            Vector2 minBoundary = (Vector2)playerArea.rect.min + new Vector2(playerArea.position.x, playerArea.position.y);
            Vector2 maxBoundary = (Vector2)playerArea.rect.max + new Vector2(playerArea.position.x, playerArea.position.y);
            float randomY = Random.Range(minBoundary.y, maxBoundary.y);
            float offset = 500f;
            Vector3 spawnPosition = new Vector3(maxBoundary.x + offset, randomY, 0f);

            // Instantiate the object prefab at the calculated position
            RectTransform spawnedObject = Instantiate(obj, spawnPosition, Quaternion.identity);
            spawnedObject.SetParent(playerArea); // Make sure the object is a child of the player area

            // Wait for the specified delay before spawning the next object
            yield return new WaitForSeconds(spawnDelay);
        }
    }

    void SeperateSpelling(int index)
    {
        foreach(char character in toSpell[index])
        {
            charList.Add(character);            
            GameObject frame = Instantiate(framePrefab);
            frame.transform.SetParent(answerContainer);
            frame.transform.GetChild(0).localScale = Vector2.zero;
        }
    }
}
