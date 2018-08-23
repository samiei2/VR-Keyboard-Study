using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using Tst;

public class WordPrediction : MonoBehaviour
{
    TstDictionary words;
    private KeyboardLayout targetkeyboard;
    private string _input;

    // Use this for initialization
    void Start()
    {
        words = new TstDictionary();
#if UNITY_STANDALONE_WIN
        string path = Application.dataPath + @"\SpellChecker\Resources\frequency_dictionary_en_82_765.txt";
#else
        string path = Application.dataPath + @"/SpellChecker/Resources/frequency_dictionary_en_82_765.txt";
#endif


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
        ResetKeyboard();
        if (args.KeyText.Equals(" "))
        {
            _input = "";
        }
        else
        {
            _input += args.KeyText;
            HashSet<char> highlightList = new HashSet<char>();
            String temp = "";
            foreach (DictionaryEntry item in words)
            {
                if (item.Key.ToString().StartsWith(_input, StringComparison.InvariantCultureIgnoreCase))
                {
                    string filteredinput = item.Key.ToString().Remove(0, _input.Length);
                    if (filteredinput.Length != 0)
                    {
                        highlightList.Add(filteredinput[0]);
                        temp += filteredinput[0] + ", ";
                    }
                }
                //highlightList.Add(item.Key.ToString()[item.Key.ToString().Length-1]);
                
            }
            Debug.Log(temp);
            targetkeyboard.HighlightKeys(new List<char>(highlightList));
        }
    }

    private void ResetKeyboard()
    {
        targetkeyboard.ResetKeyBoard();
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
