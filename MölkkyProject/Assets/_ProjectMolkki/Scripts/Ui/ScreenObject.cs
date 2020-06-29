using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class ScreenObject : MonoBehaviour
{
    [Header("animation")]
    [SerializeField] Animator animator = null;
}
