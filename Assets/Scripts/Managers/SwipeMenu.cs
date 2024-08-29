using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SwipeMenu : MonoBehaviour
{
    public GameObject scrollbar;
    private GameObject rightButton;
    private GameObject leftButton;

    private ChangeNameText changeNameText;
    float scroll_pos = 0;
    float[] pos;

    void Start()
    {
        rightButton.GetComponent<Button>().onClick.AddListener(ScrollRight);
        leftButton.GetComponent<Button>().onClick.AddListener(ScrollLeft);
        changeNameText = GetComponent<ChangeNameText>();
    }

    public void ScrollRight()
    {
        float distance = 1f / (pos.Length - 1f);
        for (int i = 0; i < pos.Length; i++)
        {
            if (scroll_pos < pos[i] + (distance / 2) && scroll_pos > pos[i] - (distance / 2))
            {
                if (i < pos.Length - 1)
                {
                    scroll_pos = pos[i + 1];
                    break;
                }
            }
        }
    }

    public void ScrollLeft()
    {
        float distance = 1f / (pos.Length - 1f);
        for (int i = 0; i < pos.Length; i++)
        {
            if (scroll_pos < pos[i] + (distance / 2) && scroll_pos > pos[i] - (distance / 2))
            {
                if (i > 0)
                {
                    scroll_pos = pos[i - 1];
                    break;
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        pos = new float[transform.childCount];
        float distance = 1f / (pos.Length - 1f);
        for (int i = 0; i < pos.Length; i++)
        {
            pos[i] = distance * i;
        }
        if (Input.GetMouseButton(0))
        {
            scroll_pos = scrollbar.GetComponent<Scrollbar>().value;
        }
        else
        {
            for (int i = 0; i < pos.Length; i++)
            {
                if (scroll_pos < pos[i] + (distance / 2) && scroll_pos > pos[i] - (distance / 2))
                {
                    scrollbar.GetComponent<Scrollbar>().value = Mathf.Lerp(scrollbar.GetComponent<Scrollbar>().value, pos[i], 0.1f);
                }
            }
        }

        for (int i = 0; i < pos.Length; i++)
        {
            if (scroll_pos < pos[i] + (distance / 2) && scroll_pos > pos[i] - (distance / 2))
            {
                transform.GetChild(i).localScale = Vector2.Lerp(transform.GetChild(i).localScale, new Vector2(1.2f, 1.2f), 0.1f);
                if (changeNameText != null)
                {
                    changeNameText.ChangeName(transform.GetChild(i).name);
                }
                print(transform.GetChild(i).name);

                for (int a = 0; a < pos.Length; a++)
                {
                    if (a != i)
                    {
                        transform.GetChild(a).localScale = Vector2.Lerp(transform.GetChild(a).localScale, new Vector2(0.8f, 0.8f), 0.1f);
                        // text.text = transform.GetChild(a).name;
                        if (changeNameText != null)
                        {
                            changeNameText.ChangeName(transform.GetChild(a).name);
                        }
                    }
                }
            }
        }
    }

    public string ReturnNameObject(GameObject transform)
    {
        return transform.name;
    }
}
