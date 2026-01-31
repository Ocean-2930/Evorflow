using System.Collections;
using TMPro;
using UnityEngine;

public class Opening : MonoBehaviour
{
    private CSVParser parser;
    private TextMeshProUGUI tm;
    public float delay = 1.0f;

    void Start()
    {
        parser = new CSVParser("Text/1_Opening/opening");
        tm = gameObject.GetComponent<TextMeshProUGUI>();
        StartCoroutine(PrintText());
        
    }

    private IEnumerator PrintText()
    {
        for (int i = 0; i < parser.data.Length; i++)
        {
            tm.text = parser.data[i];
            yield return new WaitForSeconds(delay);
        }
    }
}
