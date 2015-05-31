using UnityEngine;
using System.Collections;

public class HideTime: MonoBehaviour
{
  public float hideTime = 1.5f;
  public bool hideRoot;
  void OnEnable()
  {
    StartCoroutine(Hide());
  }

  private IEnumerator Hide() {
    yield return new WaitForSeconds(hideTime);
    if(hideRoot)
      this.transform.root.gameObject.SetActive(false);
    else
      this.gameObject.SetActive(false);
  }
} 
