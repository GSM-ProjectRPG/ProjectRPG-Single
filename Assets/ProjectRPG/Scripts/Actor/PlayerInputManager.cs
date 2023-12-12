using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInputManager : MonoBehaviour
{
    public static PlayerInputManager Instance { get; private set; }

    public KeyCode[] skillKeys = new KeyCode[] { KeyCode.Alpha1, KeyCode.Alpha2, KeyCode.Alpha3 };

    public bool inputEnable = true;

    private void Start()
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
        if (!inputEnable) return 0;
        return Input.GetAxisRaw("Horizontal");
    }

    public float GetVertical()
    {
        if (!inputEnable) return 0;
        return Input.GetAxisRaw("Vertical");
    }

    public Vector2 GetMoveVector()
    {
        if (!inputEnable) return Vector2.zero;
        return new Vector2(GetHorizontal(), GetVertical());
    }

    public bool GetRunning()
    {
        if (!inputEnable) return false;
        return Input.GetKey(KeyCode.LeftShift);
    }

    public bool GetJump()
    {
        if (!inputEnable) return false;
        return Input.GetKeyDown(KeyCode.Space);
    }

    public Vector2 GetLookDelta()
    {
        if (!inputEnable || Cursor.lockState != CursorLockMode.Locked) return Vector2.zero;
        return new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
    }

    public bool GetLookScrolling()
    {
        return inputEnable;
    }

    public bool GetSkill(int index)
    {
        if (!inputEnable) return false;
        return Input.GetKeyDown(skillKeys[index]);
    }

    public bool GetAttack()
    {
        if (!inputEnable) return false;
        return Input.GetMouseButton(0);
    }

    public float GetCameraDistanceDelta()
    {
        if (!inputEnable) return 0f;
        return -Input.GetAxis("Mouse ScrollWheel");
    }
}
