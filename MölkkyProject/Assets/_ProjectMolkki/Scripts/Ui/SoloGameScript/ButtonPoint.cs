using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class ButtonPoint : MonoBehaviour
{
    [Header("Point")]
    [SerializeField] private int point = 1;

    private Button button = null;

    public event Action<int> onPointClicked = default;

    private void Awake()
    {
        button = GetComponent<Button>();
    }

    private void Start()
    {
        button.onClick.AddListener(OnClicked);
    }

    private void OnClicked()
    {
        onPointClicked?.Invoke(point);
    }
}
