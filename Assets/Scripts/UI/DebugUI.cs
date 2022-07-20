using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DebugUI : MonoBehaviour
{
    private TextMeshProUGUI _debugUI;
    private static DebugUI _instance;
    private const int LineCapacity = 5;
    private Queue<string> _lines = new ();

    private void Awake()
    {
        _instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        _debugUI = GetComponentInChildren<TextMeshProUGUI>();
    }

    public static void Show(string msg)
    {
        string[] msgLines = msg.Split("\n");
        foreach (string line in msgLines)
        {
            _instance.AddLine(line);
        }
    }

    private void AddLine(string line)
    {
        _lines.Enqueue(line);
        if (_lines.Count > LineCapacity)
        {
            _lines.Dequeue();
        }

        string toPrint = "";
        foreach (string s in _lines)
        {
            toPrint += s + "\n";
        }

        _debugUI.text = toPrint;
    }

  
}
