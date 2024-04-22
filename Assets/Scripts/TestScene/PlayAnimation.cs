using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayAnimation : MonoBehaviour
{
  public Animator NoteAnimator;
  private bool IsOpen = false;

  // Start is called before the first frame update
  void Start()
  {

  }

  // Update is called once per frame
  void Update()
  {

  }

  public void OnEnter(BaseEventData data)
  {
    Debug.Log("OnPointerDownDelegate called.");
  }

  private void OnMouseDown()
  {
    if (NoteAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1)
    {
      if (IsOpen)
      {
        NoteAnimator.Play("Close");
        IsOpen = false;

      }
      else
      {
        NoteAnimator.Play("Open");
        IsOpen = true;
      }
    }


  }


}
