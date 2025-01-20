using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Tile : MonoBehaviour
{
    public TileState state { get; private set; }
    public TileCell cell { get; private set; } 
    public int number { get; set; }

    private Image background;
    private TextMeshProUGUI text;

    private void Awake()
    {
        background = GetComponent<Image>();
        text=GetComponentInChildren<TextMeshProUGUI>();
    }

    public void SetState(TileState state,int number)
    {
        this.state=state;
        this.number = number;

        background.color = state.backgroundColor;
        text.color = state.textColor;
        text.text = number.ToString();
    }
    private IEnumerator Amimate(Vector3 to)
    {
        float elapsed = 0f;
        float duration = 0.1f;
        Vector3 from=transform.position;
        while (elapsed<duration)
        {
            transform.position = Vector3.Lerp(from,to,elapsed/duration);
            elapsed += Time.deltaTime;
            yield return null;
        }
        transform.position = to;
    }
}
