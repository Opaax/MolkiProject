using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInfo : MonoBehaviour
{
    [Header("RectTransform")]
    [SerializeField] private RectTransform rect = null;

    public RectTransform Rect { get => rect; set => rect = value; }
}
