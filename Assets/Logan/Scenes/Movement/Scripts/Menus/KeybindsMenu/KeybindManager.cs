using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class KeybindManager : MonoBehaviour
{

    //private static KeybindManager instance;

    //public static KeybindManager MyInstance
    //{
    //    get
    //    {
    //        if (instance == null)
    //        {
    //            instance = FindObjectOfType<KeybindManager>();
    //        }

    //        return instance;
    //    }

    //}

    //public Dictionary<string, KeyCode> Keybinds { get; private set; }

    //public Dictionary<string, KeyCode> ActionBinds { get; private set; }

    //private string bindName;

    //// Start is called before the first frame update
    //void Start()
    //{
    //    Keybinds = new Dictionary<string, KeyCode>();
    //    ActionBinds = new Dictionary<string, KeyCode>();

    //    BindKey("UP", KeyCode.W);
    //    BindKey("DOWN", KeyCode.S);
    //    BindKey("RIGHT", KeyCode.D);
    //    BindKey("LEFT", KeyCode.A);

    //    BindKey("DASH", KeyCode.LeftControl); //Dash
    //    BindKey("ACT2", KeyCode.F); //Grab
    //}

    //public void BindKey(string key, KeyCode keyBind)
    //{
    //    Dictionary<string, KeyCode> currentDictionary = Keybinds;

    //    if(key.Contains("ACT"))
    //    {
    //        currentDictionary = ActionBinds;
    //    }

    //    if(!currentDictionary.ContainsKey(key))
    //    {
    //        currentDictionary.Add(key, keyBind);
    //        KeybindsMenuManager.MyInstance.UpdateKeyText(key, keyBind);
    //    }
    //    else if (currentDictionary.ContainsValue(keyBind))
    //    {
    //        string myKey = currentDictionary.FirstOrDefault(x => x.Value == keyBind).Key;

    //        currentDictionary[myKey] = KeyCode.None;

    //        KeybindsMenuManager.MyInstance.UpdateKeyText(key, KeyCode.None);
    //    }

    //    currentDictionary[key] = keyBind;
    //    KeybindsMenuManager.MyInstance.UpdateKeyText(key, keyBind);
    //    bindName = string.Empty;

    //}

    //public void KeyBindOnClick(string bindName)
    //{
    //    this.bindName = bindName;
    //}

    //private void OnGUI()
    //{
    //    if(bindName != string.Empty)
    //    {
    //        Event e = Event.current;
    //        if(e.isKey)
    //        {
    //            BindKey(bindName, e.keyCode);
    //        }
    //    }
    //}

}
