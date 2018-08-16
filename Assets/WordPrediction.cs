using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using Tst;

public class WordPrediction : MonoBehaviour {
    TstDictionary words;
    private KeyboardLayout targetkeyboard;
    private string _input;

    // Use this for initialization
    void Start () {
        words = new TstDictionary();
        string path = Application.dataPath + @"\SpellChecker\Resources\frequency_dictionary_en_82_765.txt";
        LoadDictionary(path);
        targetkeyboard = GetComponent<KeyboardLayout>();
        if (!targetkeyboard)
        {
            Debug.LogError("Target Keyboard Empty");
        }
        else
        {
            targetkeyboard.KeyboardLayout_OnKeyPressed += WordPrediction_KeyPressedHandler;
        }
    }

    private void WordPrediction_KeyPressedHandler(object sender, KeyEventArgs args)
    {
        if(args.KeyText.Equals(" "))
        {
            _input = "";
            ResetKeyboard();
        }
        else
        {
            _input += args.KeyText;
            List<char> highlightList = new List<char>(); 
            foreach (DictionaryEntry item in words.PartialMatch(_input + "*"))
            {
                highlightList.Add(item.Key.ToString()[item.Key.ToString().Length-1]);
            }
            targetkeyboard.HighlightKeys(highlightList);
        }
    }

    private void ResetKeyboard()
    {
        
    }

    private void LoadDictionary(string path)
    {
        StreamReader reader = new StreamReader(path);
        while (!reader.EndOfStream)
        {
            string line = reader.ReadLine();
            var splitline = line.Split(' ');
            if(splitline.Length > 0)
                words.Add(splitline[0].Trim(),null);
        }
        Debug.Log("# Words in Dictionary Loaded : " + words.Count);
    }
}
