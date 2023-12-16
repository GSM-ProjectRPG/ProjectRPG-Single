using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInputManager : MonoBehaviour
{
    public static PlayerInputManager Instance { get; private set; }

    public KeyCode[] SkillKeys = new KeyCode[] { KeyCode.Alpha1, KeyCode.Alpha2, KeyCode.Alpha3 };

    public bool InputEnable = true;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Debug.LogError("PlayerInputManager가 2개 이상 존재합니다.\nGameObject : " + gameObject.name);
            Destroy(Instance);
        }
    }

    public float GetHorizontal()
    {
        if (!InputEnable) return 0;
        return Input.GetAxisRaw("Horizontal");
    }

    public float GetVertical()
    {
        if (!InputEnable) return 0;
        return Input.GetAxisRaw("Vertical");
    }

    public Vector2 GetMoveVector()
    {
        if (!InputEnable) return Vector2.zero;
        return new Vector2(GetHorizontal(), GetVertical());
    }

    public bool GetRunning()
    {
        if (!InputEnable) return false;
        return Input.GetKey(KeyCode.LeftShift);
    }

    public bool GetJump()
    {
        if (!InputEnable) return false;
        return Input.GetKeyDown(KeyCode.Space);
    }

    public Vector2 GetLookDelta()
    {
        if (!InputEnable || Cursor.lockState != CursorLockMode.Locked) return Vector2.zero;
        return new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
    }

    public bool GetLookScrolling()
    {
        return InputEnable;
    }

    public bool GetSkill(int index)
    {
        if (!InputEnable) return false;
        return Input.GetKeyDown(SkillKeys[index]);
    }

    public bool GetAttack()
    {
        if (!InputEnable) return false;
        return Input.GetMouseButton(0);
    }

    public float GetCameraDistanceDelta()
    {
        if (!InputEnable) return 0f;
        return -Input.GetAxis("Mouse ScrollWheel");
    }
}
