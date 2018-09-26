using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellCorrection : MonoBehaviour {
    //private SymSpell symSpell;
    //public bool enableCompoundCorrection = false;
    //public bool enableSegmentation = false;
    //public KeyboardLayout targetkeyboard;

    //private string _input = "";

    //// Use this for initialization
    //void Start()
    //{
    //    if (!targetkeyboard)
    //        targetkeyboard = GetComponent<KeyboardLayout>();
    //    if (!targetkeyboard)
    //    {
    //        Debug.LogError("Target Keyboard Empty");
    //    }
    //    else
    //    {
    //        targetkeyboard.KeyboardLayout_OnKeyPressed += WordPrediction_KeyPressedHandler;
    //    }
    //    Debug.Log("Creating dictionary ...");

    //    //set parameters
    //    const int initialCapacity = 82765;
    //    const int maxEditDistance = 2;
    //    const int prefixLength = 7;
    //    symSpell = new SymSpell(initialCapacity, maxEditDistance, prefixLength);

    //    //Load a frequency dictionary
    //    //wordfrequency_en.txt  ensures high correction quality by combining two data sources: 
    //    //Google Books Ngram data  provides representative word frequencies (but contains many entries with spelling errors)  
    //    //SCOWL — Spell Checker Oriented Word Lists which ensures genuine English vocabulary (but contained no word frequencies)   
    //    string path = Application.dataPath + @"\SpellChecker\Resources\frequency_dictionary_en_82_765.txt"; //path referencing the SymSpell core project
    //                                                                                                        //string path = "../../frequency_dictionary_en_82_765.txt";  //path when using symspell nuget package (frequency_dictionary_en_82_765.txt is included in nuget package)
    //    if (!symSpell.LoadDictionary(path, 0, 1)) { Debug.LogError("\rFile not found: " + System.IO.Path.GetFullPath(path)); }

    //    //warm up
    //    var result = symSpell.Lookup("warmup", SymSpell.Verbosity.All);
    //}


    //private void WordPrediction_KeyPressedHandler(object sender, KeyEventArgs args)
    //{
    //    Debug.Log("Predicting");
    //    if (enableCompoundCorrection)
    //    {
    //        _input += args.KeyText;
    //    }
    //    else
    //    {
    //        if (args.KeyText == " " || args.KeyText == "spc")
    //            _input = "";
    //        else
    //        {
    //            _input += args.KeyText;

    //            if (!string.IsNullOrEmpty(_input))
    //            {
    //                List<SymSpell.SuggestItem> suggestions = Correct(_input, symSpell);
    //                List<char> suggestedAlphabet = new List<char>();
    //                foreach (SymSpell.SuggestItem item in suggestions)
    //                {
    //                    var replaced = item.term.Replace(_input, "");
    //                    if (replaced != "")
    //                    {
    //                        suggestedAlphabet.Add(replaced[0]);
    //                    }
    //                }

    //                targetkeyboard.HighlightKeys(suggestedAlphabet);
    //            }
    //        }
    //    }

    //}

    //public static List<SymSpell.SuggestItem> Correct(string input, SymSpell symSpell)
    //{
    //    List<SymSpell.SuggestItem> suggestions = null;

    //    //check if input term or similar terms within edit-distance are in dictionary, return results sorted by ascending edit distance, then by descending word frequency     
    //    const SymSpell.Verbosity verbosity = SymSpell.Verbosity.All;
    //    suggestions = symSpell.Lookup(input, verbosity);

    //    //return suggestions;
    //    //display term and frequency
    //    foreach (var suggestion in suggestions)
    //    {
    //        //Debug.Log(suggestion.term + " " + suggestion.distance.ToString() + " " + suggestion.count.ToString("N0"));
    //    }
    //    if (verbosity != SymSpell.Verbosity.Top) Debug.Log(suggestions.Count.ToString() + " suggestions");
    //    return suggestions;
    //}

}
