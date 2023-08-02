using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectScript : MonoBehaviour
{
    [SerializeField] float objectSpeed = 10f;
    Vector2 minBoundary;
    Vector2 maxBoundary;

    private void Awake()
    {

    }
    // Start is called before the first frame update
    void Start()
    {
        minBoundary = (Vector2)RocketMaster.Instance.playerArea.rect.min + new Vector2(RocketMaster.Instance.playerArea.position.x, RocketMaster.Instance.playerArea.position.y);
        maxBoundary = (Vector2)RocketMaster.Instance.playerArea.rect.max + new Vector2(RocketMaster.Instance.playerArea.position.x, RocketMaster.Instance.playerArea.position.y);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector2.left);
        float screenOffset = 105f;
        if(transform.position.x < minBoundary.x - screenOffset)
        {
            Destroy(transform.gameObject);
        }
    }
}
