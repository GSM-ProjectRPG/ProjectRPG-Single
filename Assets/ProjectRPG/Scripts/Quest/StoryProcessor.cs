using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StoryProcessor : MonoBehaviour
{
    private void Update()
    {
        transform.LookAt(Camera.main.transform);
    }

    public IEnumerator Story(List<string> story)
    {
        foreach (var content in story)
        {
            GetComponentInChildren<Text>().text = content;
            yield return new WaitForSeconds(2f);
        }
        gameObject.SetActive(false);
    }
}
