using UnityEngine;

// GameChapter 枚举定义
public enum GameChapter
{
    Intro,
    Pre,
    Ending
}

public class PresentationManager : MonoBehaviour
{
    public static PresentationManager Instance { get; private set; }

    public GameChapter CurrentChapter { get; private set; }

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
    }

    void Start()
    {
        SetChapter(GameChapter.Intro);
    }

    public void SetChapter(GameChapter newChapter)
    {
        CurrentChapter = newChapter;
        Debug.Log("当前章节: " + CurrentChapter.ToString());
    }

    public bool IsInChapter(GameChapter chapter)
    {
        return CurrentChapter == chapter;
    }
}
