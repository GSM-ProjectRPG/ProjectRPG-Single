using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestClearEffect : MonoBehaviour
{
    private void Start()
    {
        gameObject.GetComponent<Image>().color = new Color(0, 0, 0, 0);
        gameObject.GetComponentInChildren<Text>().color = new Color(0, 0, 0, 0);
    }

    public IEnumerator OnClear()
    {
        for (float i = 0; i <= 0.96f; i += 0.01f)
        {
            gameObject.GetComponent<Image>().color = new Color(0, 0, 0, i);
            gameObject.GetComponentInChildren<Text>().color = new Color(255, 255, 255, i);
            yield return new WaitForSeconds(0.01f);
        }
        yield return new WaitForSeconds(1);
        Destroy(transform.parent.gameObject);
    }
}
