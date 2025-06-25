using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadSceneManager : MonoBehaviour
{
    [SerializeField] private Animator _animator;

    [Header("Events")]
    [SerializeField] private VoidGameEvent _onPlayerGame;
    [SerializeField] private VoidGameEvent _onPlayerLoadScreen;

    public AsyncOperation _operation;
    public Coroutine _coroutine;

    private void Start()
    {
        OpenLoadScreen();
    }

    public void LoadScene(int index)
    {
        if (index < 0 || index >= SceneManager.sceneCountInBuildSettings) return;

        CloseLoadScreen(index);
    }

    public IEnumerator Animation(string name)
    {
        _animator.SetTrigger(name);
        // Ждём, пока анимация не начнётся
        while (true)
        {
            AnimatorStateInfo stateInfo = _animator.GetCurrentAnimatorStateInfo(0);
            if (stateInfo.IsName(name) && !_animator.IsInTransition(0))
                break;
            yield return null;
        }
        //Debug.Log("Анимация началась");
        //AnimationStart();

        // Отслеживаем прогресс анимации
        while (_animator.GetCurrentAnimatorStateInfo(0).IsName(name))
        {
            yield return null;
        }

        //Debug.Log("Анимация закончилась");
        AnimationEnd(name);
    }

    public IEnumerator Animation(string name, int index)
    {
        _animator.SetTrigger(name);
        // Ждём, пока анимация не начнётся
        while (true)
        {
            AnimatorStateInfo stateInfo = _animator.GetCurrentAnimatorStateInfo(0);
            if (stateInfo.IsName(name) && !_animator.IsInTransition(0))
                break;
            yield return null;
        }
        //Debug.Log("Анимация началась");
        //AnimationStart();

        // Отслеживаем прогресс анимации
        while (_animator.GetCurrentAnimatorStateInfo(0).IsName(name))
        {
            yield return null;
        }

        //Debug.Log("Анимация закончилась");
        AnimationEnd(name, index);
    }

    public void AnimationEnd(string name)
    {
        StopCoroutine(_coroutine);
        switch (name)
        {
            case "Open":
                _onPlayerGame.Raise();
                break;
        }
    }

    public void AnimationEnd(string name, int index)
    {
        StopCoroutine(_coroutine);
        switch (name)
        {
            case "Close":
                SceneManager.LoadScene(index);
                break;
        }
    }

    public void OpenLoadScreen()
    {
        _coroutine = StartCoroutine(Animation("Open"));
    }

    public void CloseLoadScreen(int index)
    {
        _onPlayerLoadScreen.Raise();
        _coroutine = StartCoroutine(Animation("Close", index));
    }

    public void Quit()
    {
        Application.Quit();
    }
}
