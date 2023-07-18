using System;
using System.Collections;
using UnityEngine;


public enum InstructionsState
{
    Start,
    Controls,
    Visible,
    Hidden
}
public class InstructionsController : MonoBehaviour
{
    public static event Action InstructionsFinished;

    [SerializeField] private float startWaitTime = 1f;
    
    private Animator animator;
    private InstructionsState state = InstructionsState.Start;
    
    private float startTime;
    
    private static bool didShowInstructions;
    
    void Start()
    {
        animator = GetComponent<Animator>();
        startTime = Time.time + startWaitTime;
    }

    void Update()
    {
        if (!didShowInstructions)
        {
            if (Time.time < startTime) return;

            HandleInstructionsState(state);    
        }
        else
        {
            StartCoroutine(SkipInstructionsCoroutine());
        }
    }

    private void HandleInstructionsState(InstructionsState currentState)
    {
        switch (currentState)
        {
            case InstructionsState.Start:
                animator.SetTrigger("Start");
                state = InstructionsState.Visible;
                break;
            
            case InstructionsState.Visible:
                if (Input.anyKeyDown) StartCoroutine(HideCoroutine());
                break;
            
            case InstructionsState.Controls:
                if (Input.anyKeyDown) StartCoroutine(HideCoroutine());
                break;
            
            case InstructionsState.Hidden:
                break;
        }
    }

    private IEnumerator HideCoroutine()
    {
        didShowInstructions = true;
        
        animator.SetTrigger("End");

        yield return new WaitForSeconds(1f);

        state = InstructionsState.Hidden;
        
        InstructionsFinished?.Invoke();
    }

    private IEnumerator SkipInstructionsCoroutine()
    {
        yield return new WaitForSeconds(1f);
        
        InstructionsFinished?.Invoke();
    }
}
