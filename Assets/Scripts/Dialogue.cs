using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Dialogue : MonoBehaviour
{
    public List<Chat> lines;
    public List<ChatOption> options;

    public DialogueUI dialogueUI;
    private void Start()
    {
        dialogueUI = DialogueUI.instance;
    }
    public void StartDialogue() 
    {
        dialogueUI.OpenDialogueIU(this);
    }



}

[Serializable]
public class Chat
{
    [TextArea]
    public string text;
    public string speaker;
    public AudioClip voiceClip;
}
[Serializable]
public class ChatOption 
{
    public string optionText;
    public Dialogue dialogues;
    public UnityEvent action;
}