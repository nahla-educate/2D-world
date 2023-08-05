using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ChatBubble : MonoBehaviour
{
    public static void Create(Transform parent, Vector3 localPosition, string text)
    {
        Transform transformtBubbleTransform = Instantiate(parent);
        transformtBubbleTransform.localPosition = localPosition;

        transformtBubbleTransform.GetComponent<ChatBubble>().Setup(text);

        Destroy(transformtBubbleTransform.gameObject, 4f);
    }
       
    private SpriteRenderer backgroundSpriteRenderer;
    private SpriteRenderer iconSpriteRenderer;
    private TextMeshPro textMeshPro;

    // Start is called before the first frame update
    void Awake()
    {
        backgroundSpriteRenderer = transform.Find("Background").GetComponent<SpriteRenderer>();
      //  iconSpriteRenderer = transform.Find("Icon").GetComponent<SpriteRenderer>();
        textMeshPro = transform.Find("Text").GetComponent<TextMeshPro>();


    }

    // Update is called once per frame
    private void Start()
    {
        Setup("Hello World");
    }

    private void Setup(string text)
    {
        textMeshPro.SetText(text);
        textMeshPro.ForceMeshUpdate();

        /*Vector2 textSize = textMeshPro.GetRenderedValues(false);
       
        backgroundSpriteRenderer.size = textSize;*/

        Vector3 offset = new Vector3(0f, 1.2f);

        backgroundSpriteRenderer.transform.localPosition =
            new Vector3(backgroundSpriteRenderer.size.x / 1f, 0f) + offset;
    }

}
