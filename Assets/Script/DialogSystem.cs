using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine.UIElements;
using UnityEngine.Events;

public class DialogSystem : MonoBehaviour
{
    public List<Dialogs> StoryLines;

    [Serializable]
    public struct Dialogs
    {
        public string StoryLine;
        public List<Dialog> DialogLines;
    }

    [Serializable]
    public struct Dialog
    {
        [HideInInspector]
        public string name;
        public string CharacterName;
        public string Text;
        public List<Answer> Answers;
    }

    [Serializable]
    public struct Answer
    {
        public string AnswerText;
        public int goesTo;
        public UnityEvent Trigger;
        public bool end;
        public List<Condition> Conditions;
    }
    
    [Serializable]
    public class Condition
    {
        public UnityEvent CheckCondition;
    }

}



