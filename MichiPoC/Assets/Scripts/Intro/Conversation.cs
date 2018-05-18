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
        public string DialogEventString;

        public DialogString(SpeachbubbleManager talker, string text, string dialogEventString = "none")
        {
            Talker = talker;
            Text = text;
            DialogEventString = dialogEventString;
        }
    }

    private struct DialogEvent
    {
        public string CallString;
        public Action EventToOccure;

        public DialogEvent(string callString, Action eventToOccure)
        {
            CallString = callString;
            EventToOccure = eventToOccure;
        }
    }

    public SpeachbubbleManager MeanBoss;
    public SpeachbubbleManager FriendlyBoss;

    public GameObject IntroSprites;
    public GameObject GuySprite;
    public GameObject GuitarSprite;
    public GameObject GroupyStunSprite;
    public GameObject GroupyPunchSprite;
    public GameObject GroupyDestroySprite;

    public MoveToSpot PlayerMover;

    private List<DialogString> _dialogStrings = new List<DialogString>();
    private List<DialogEvent> _dialogEvents = new List<DialogEvent>();
    private int _currentDialog = 0;

    private void Start()
    {
        _dialogStrings.Add(new DialogString(MeanBoss, "... [Press \nany key to \ncontinue]"));
        _dialogStrings.Add(new DialogString(MeanBoss, "Hey Roady you \nlazy fuck. Get \nover here.", "Start"));
        _dialogStrings.Add(new DialogString(MeanBoss, "Honestly you \nare the worst \nRoady we ever \nhad on our stage."));
        _dialogStrings.Add(new DialogString(MeanBoss, "You always \nbring our stuff \nfrom backstage a \nsplit second before \nwe need it."));
        _dialogStrings.Add(new DialogString(MeanBoss, "And sometimes \nyou even hit \nour groupies that \nsneaked up on \nthe stage."));
        _dialogStrings.Add(new DialogString(MeanBoss, "You really are \ntruly horrible."));
        _dialogStrings.Add(new DialogString(FriendlyBoss, "But just the man \nwe need right now."));
        _dialogStrings.Add(new DialogString(FriendlyBoss, "We want you to \napply as a roady \nat these other bands \nand wreck havoc \nthere."));
        _dialogStrings.Add(new DialogString(FriendlyBoss, "But don`t get \nfired, so be \nfast with supplies, \nlike guitars."));
        _dialogStrings.Add(new DialogString(FriendlyBoss, "Bring the guitars \nto their guys.", "ShowGuy")); //Show guy
        _dialogStrings.Add(new DialogString(FriendlyBoss, "To scare of their \nfans just hit \nsome of their \ngroupies on stage.", "HideGuy")); //Hide guy
        _dialogStrings.Add(new DialogString(FriendlyBoss, "You know how \nto hit groupies, \ndon´t you?"));
        _dialogStrings.Add(new DialogString(MeanBoss, "Just walk to `em \nand smash `em in \nthe head!!!"));
        _dialogStrings.Add(new DialogString(FriendlyBoss, "You have to pick \nup a guitar first.", "ShowGuitar")); //Show guitar
        _dialogStrings.Add(new DialogString(FriendlyBoss, "Then walk to \nthem by pressing \n[A] or [D] and jump \nby pressing [W].", "HideGuitar")); //Hide guitar
        _dialogStrings.Add(new DialogString(FriendlyBoss, "At the moment \nyou are close \nenough just press \n[Space] to hit them."));
        _dialogStrings.Add(new DialogString(FriendlyBoss, "Hitting them will \nalso bring you \ncloser to being \nfired, so be \ncautious."));
        _dialogStrings.Add(new DialogString(FriendlyBoss, "Also try not \nto touch the \ngroupies."));
        _dialogStrings.Add(new DialogString(FriendlyBoss, "They might be \npissed and hit \nyou back or even \ndestroy the \nguitar."));
        _dialogStrings.Add(new DialogString(FriendlyBoss, "These groupies \nwill push you \nback and prevent \nyou from moving \nfor a short \nduration. (brown)", "ShowGroupyStun")); //Show groupy stun
        _dialogStrings.Add(new DialogString(FriendlyBoss, "These groupies \nwill kick your \nguitar out of \nyour hands. (yellow)", "ShowGroupyPunch")); //Show groupy punch
        _dialogStrings.Add(new DialogString(FriendlyBoss, "These groupies \nwill destroy your \nguitar. (pink)", "ShowGroupyDestroy")); //Show groupy destroy
        _dialogStrings.Add(new DialogString(MeanBoss, "You were the worst \nhere you little fucker, \nbut try to be the \nworst roady the \nworld has ever \nseen out there.", "HideGroupyDestroy")); //Hide groupy destroy
        _dialogStrings.Add(new DialogString(MeanBoss, "Then go and \ndon`t fuck it up. \nOr no fuck it up \nbig time, but \nfor them.", "End"));

        _dialogEvents.Add(new DialogEvent("Start", () => { PlayerMover.StartMoving(); }));
        _dialogEvents.Add(new DialogEvent("ShowGuy", () => { ShowGuy(); }));
        _dialogEvents.Add(new DialogEvent("HideGuy", () => { HideGuy(); }));
        _dialogEvents.Add(new DialogEvent("ShowGuitar", () => { ShowGuitar(); }));
        _dialogEvents.Add(new DialogEvent("HideGuitar", () => { HideGuitar(); }));
        _dialogEvents.Add(new DialogEvent("ShowGroupyStun", () => { ShowGroupyStun(); }));
        _dialogEvents.Add(new DialogEvent("ShowGroupyPunch", () => { ShowGroupyPunch(); }));
        _dialogEvents.Add(new DialogEvent("ShowGroupyDestroy", () => { ShowGroupyDestroy(); }));
        _dialogEvents.Add(new DialogEvent("HideGroupyDestroy", () => { HideGroupyDestroy(); }));
        _dialogEvents.Add(new DialogEvent("End", () => { PlayerMover.StartMovingBack(); }));

        MeanBoss.FinishSpeaking();
        FriendlyBoss.FinishSpeaking();
    }


    public void Update()
    {
        if (Input.anyKeyDown)
        {
            _currentDialog++;

            foreach (var dialogEvent in _dialogEvents)
            {
                if (_dialogStrings[_currentDialog].DialogEventString == dialogEvent.CallString)
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
            SceneManager.LoadScene("Lvl1");
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

    private void ShowGuy()
    {
        IntroSprites.SetActive(true);
        GuySprite.SetActive(true);
    }

    private void HideGuy()
    {
        IntroSprites.SetActive(false);
        GuySprite.SetActive(false);
    }

    private void ShowGuitar()
    {
        IntroSprites.SetActive(true);
        GuitarSprite.SetActive(true);
    }

    private void HideGuitar()
    {
        IntroSprites.SetActive(false);
        GuitarSprite.SetActive(false);
    }

    private void ShowGroupyStun()
    {
        IntroSprites.SetActive(true);
        DisableAndEnableGameObject(GroupyStunSprite, GuitarSprite);
    }

    private void ShowGroupyPunch()
    {
        DisableAndEnableGameObject(GroupyPunchSprite, GroupyStunSprite);
    }

    private void ShowGroupyDestroy()
    {
        DisableAndEnableGameObject(GroupyDestroySprite, GroupyPunchSprite);
    }

    private void HideGroupyDestroy()
    {
        IntroSprites.SetActive(false);
        GroupyDestroySprite.SetActive(true);
    }

    private void DisableAndEnableGameObject(GameObject toEnable, GameObject toDisable)
    {
        if(toEnable != null)
        {
            toEnable.SetActive(true);
        }

        if(toDisable != null)
        {
            toDisable.SetActive(false);
        }
    }




}
