//0000000000000000000000000000000000000000000000
//            NGUI: Next-Gen UI kit
// Copyright ?2011-2016 Tasharen Entertainment
//0000000000000000000000000000000000000000000000

using UnityEngine;

/// <summary>
/// Very basic script that will activate or deactivate an object (and all of its children) when clicked.
/// </summary>

[AddComponentMenu("NGUI/Interaction/Button Activate")]
public class UIButtonActivate : MonoBehaviour
{
    public GameObject target;
    public bool state = true;

    void OnClick () { if (target != null) NGUITools.SetActive(target, state); }
}