// Copyright (c) Meta Platforms, Inc. and affiliates.

using UnityEngine;
using UnityEngine.AI;

public class VirtualPet : MonoBehaviour
{
    Animator _animator;
    public ThoughtBubble _thoughtBubble;
    public GameObject _listeningIndicator;
    NavMeshAgent _agent;
    Vector3 _moveTargetDir = Vector3.forward;
    float _runSpeed = 2.0f;
    Transform _mainCam;
    public PetState _oppyState { private set; get; } = PetState.Idle;
    public bool _welcomeVoicePlayed = false; // 用于判断是否播放了欢迎语音

    public enum PetState
    {
        Idle,
        Listening,
    };

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _listeningIndicator.SetActive(false);
    }

    public void Initialize()
    {
        _agent = GetComponent<NavMeshAgent>();
        _agent.SetDestination(Vector3.zero);
        ResetAnimFlags();
        _thoughtBubble.gameObject.SetActive(false);
    }

    public bool CanListen()
    {
        // 判断是否在 Intro 章节
        bool inIntroChapter = PresentationManager.Instance.IsInChapter(GameChapter.Intro);

        // 判断是否播放了欢迎语音
        bool welcomeVoicePlayed = _welcomeVoicePlayed;

        // 条件：处于 Intro 章节且欢迎语音播放完毕
        return inIntroChapter && welcomeVoicePlayed;
    }

    // 在欢迎语音播放完毕时调用该方法
    public void OnWelcomeVoicePlayed()
    {
        _welcomeVoicePlayed = true;
    }

    public void DisplayThought(string thought = "")
    {
        _thoughtBubble.gameObject.SetActive(true);
        _thoughtBubble.ForceSizeUpdate();
        if (thought == "")
        {
            _thoughtBubble.ShowHint();
        }
        else
        {
            _thoughtBubble.UpdateText(thought);
        }
    }

    public void HideThought()
    {
        _thoughtBubble.gameObject.SetActive(false);
    }

    public void Listening(bool value)
    {
        if (value) _animator.SetBool("ListenFail", false);
        _listeningIndicator.SetActive(value);
        if (!value && _animator.GetBool("Listening"))
        {
            _animator.SetTrigger("ForceChase");
        }
        _animator.SetBool("Listening", value);
        _oppyState = value ? PetState.Listening : PetState.Idle;
        if (value)
        {
            SetLookDirection((_mainCam.position - transform.position).normalized);
        }
    }

    public void ListenFail()
    {
        HideThought();
        _animator.SetBool("ListenFail", true);
        _listeningIndicator.SetActive(false);
    }

    public void VoiceCommandHandler(string actionString)
    {
        _animator.SetBool("Listening", false);
        switch (actionString)
        {
            case "hi":
                _animator.SetTrigger("Wave");
                break;
        }
        Listening(false);
        DisplayThought(actionString + "?");
    }

    public void ResetAnimFlags()
    {
        _animator.SetBool("Running", false);
        _animator.SetBool("Petting", false);
        _animator.SetBool("Listening", false);
        _animator.SetBool("ListenFail", false);
    }

    public void SetLookDirection(Vector3 lookDirection)
    {
        _moveTargetDir = lookDirection;
    }
}

