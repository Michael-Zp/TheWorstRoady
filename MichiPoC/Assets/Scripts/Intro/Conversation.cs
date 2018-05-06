using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;

public class Conversation : MonoBehaviour
{
    private struct DialogString
    {
        public SpeachbubbleManager Talker;
        public string Text;

        public DialogString(SpeachbubbleManager talker, string text)
        {
            Talker = talker;
            Text = text;
        }
    }

    private struct DialogEvent
    {
        public int IndexToCallEvent;
        public Action EventToOccure;

        public DialogEvent(int indexToCallEvent, Action eventToOccure)
        {
            IndexToCallEvent = indexToCallEvent;
            EventToOccure = eventToOccure;
        }
    }

    public SpeachbubbleManager MeanBoss;
    public SpeachbubbleManager FriendlyBoss;

    public MoveToSpot PlayerMover;

    private List<DialogString> _dialogStrings = new List<DialogString>();
    private List<DialogEvent> _dialogEvents = new List<DialogEvent>();
    private int _currentDialog = 0;

    private void Start()
    {
        _dialogStrings.Add(new DialogString(MeanBoss, "..."));
        _dialogStrings.Add(new DialogString(MeanBoss, "Hey Roady you \nlazy fuck. Get \nover here."));
        _dialogStrings.Add(new DialogString(MeanBoss, "Honestly you \nare the worst \nRoady we ever \nhad on our stage."));
        _dialogStrings.Add(new DialogString(MeanBoss, "You are always \nbring our stuff \nfrom backstage a \nsplit second before \nwe need it."));
        _dialogStrings.Add(new DialogString(MeanBoss, "And sometimes \nyou even hit \nour groupies that \nsneaked up on \nthe stage."));
        _dialogStrings.Add(new DialogString(MeanBoss, "You really are \ntruly horrible."));
        _dialogStrings.Add(new DialogString(FriendlyBoss, "But just the man \nwe need right now."));
        _dialogStrings.Add(new DialogString(FriendlyBoss, "We want you to \napply as a roady \nat these other bands \nand wreck havoc \nthere."));
        _dialogStrings.Add(new DialogString(FriendlyBoss, "But don`t get \nfired, so be on \nfast with supplies, \nlike guitars."));
        _dialogStrings.Add(new DialogString(FriendlyBoss, "To scare of their \nfans just hit \nsome of their \ngroupies on stage."));
        _dialogStrings.Add(new DialogString(FriendlyBoss, "You know how \nto hit groupies, \ndon´t you?"));
        _dialogStrings.Add(new DialogString(MeanBoss, "Just walk to `em \nand smash `em in \nthe head!!!"));
        _dialogStrings.Add(new DialogString(FriendlyBoss, "You have to pick \nup a guitar first."));
        _dialogStrings.Add(new DialogString(FriendlyBoss, "Then walk to \nthem by pressing \n[A] or [D] and jump \nby pressing [W]."));
        _dialogStrings.Add(new DialogString(FriendlyBoss, "At the moment \nyou are close \nenough just press \n[Space] to hit them."));
        _dialogStrings.Add(new DialogString(FriendlyBoss, "Hitting them will \nalso bring you \ncloser to being \nfired, so be \ncautious."));
        _dialogStrings.Add(new DialogString(MeanBoss, "You were the worst \nhere you little fucker, \nbut try to be the \nworst roady the \nworld has ever \nseen out there."));
        _dialogStrings.Add(new DialogString(MeanBoss, "Then go and \ndon`t fuck it up. \nOr no fuck it up \nbig time, but \nfor them."));

        _dialogEvents.Add(new DialogEvent(1, () => { PlayerMover.StartMoving(); }));
        _dialogEvents.Add(new DialogEvent(_dialogStrings.Count - 1, () => { PlayerMover.StartMovingBack(); }));

        MeanBoss.FinishSpeaking();
        FriendlyBoss.FinishSpeaking();
    }

    public void Update()
    {
        Debug.Log(_currentDialog);

        if (Input.anyKeyDown)
        {
            _currentDialog++;

            foreach (var dialogEvent in _dialogEvents)
            {
                if (_currentDialog == dialogEvent.IndexToCallEvent)
                {
                    dialogEvent.EventToOccure.Invoke();
                }
            }
        }


        if (_currentDialog < _dialogStrings.Count)
        {
            ShowCurrentDialog();
        }
        else
        {
            EventSystem.Instance.UnlockLevel(1);
            SceneManager.LoadScene("Main");
        }
    }

    private void ShowCurrentDialog()
    {
        if (_currentDialog > 0)
        {
            _dialogStrings[_currentDialog - 1].Talker.FinishSpeaking();
        }

        _dialogStrings[_currentDialog].Talker.Speak(_dialogStrings[_currentDialog].Text);
    }
}
