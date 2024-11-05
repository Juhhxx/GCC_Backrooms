using System;
using TMPro;
using UnityEngine;

public class PageManager : MonoBehaviour
{
    private int _totalPages;
    private int _collectedPages = 0;
    private TextMeshProUGUI _tmp;
    void Start()
    {
        _totalPages = FindObjectsByType<GrabPaper>(0).Length;
        _tmp = GetComponent<TextMeshProUGUI>();
        UpdateText();
    }
    public void CollectedPage() 
    {
        _collectedPages ++;
        UpdateText();
    }
    private void UpdateText()
    {
        _tmp.text = String.Format("Collected Pages : {0}/{1}",_collectedPages,_totalPages);
    }
}
