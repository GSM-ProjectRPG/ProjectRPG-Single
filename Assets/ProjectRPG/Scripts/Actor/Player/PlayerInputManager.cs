using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInputManager : MonoBehaviour
{
    private static PlayerInputManager _instance;
    public static PlayerInputManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindAnyObjectByType<PlayerInputManager>();
                if (_instance == null)
                {
                    GameObject g = new GameObject("PlayerInputManager");
                    _instance = g.AddComponent<PlayerInputManager>();
                }
            }
            return _instance;
        }
        private set { _instance = value; }
    }

    public bool MouseLock
    {
        get { return !Cursor.visible; }
        set
        {
            if (value)
            {
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
            }
            else
            {
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
            }
        }
    }

    private void Awake()
    {
        if (Instance != this)
        {
            Debug.LogError("PlayerInputManager가 2개 이상 존재합니다.\nGameObject : " + gameObject.name);
            Destroy(Instance);
        }
    }

    public float GetHorizontal()
    {
        if (!MouseLock) return 0;
        return Input.GetAxisRaw("Horizontal");
    }

    public float GetVertical()
    {
        if (!MouseLock) return 0;
        return Input.GetAxisRaw("Vertical");
    }

    public Vector2 GetMoveVector()
    {
        if (!MouseLock) return Vector2.zero;
        return new Vector2(GetHorizontal(), GetVertical());
    }

    public bool GetRunning()
    {
        if (!MouseLock) return false;
        return Input.GetKey(KeyCode.LeftShift);
    }

    public bool GetJump()
    {
        if (!MouseLock) return false;
        return Input.GetKeyDown(KeyCode.Space);
    }

    public Vector2 GetLookDelta()
    {
        if (!MouseLock || Cursor.lockState != CursorLockMode.Locked) return Vector2.zero;
        return new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
    }

    public bool GetLookScrolling()
    {
        return MouseLock;
    }

    public bool GetAttack()
    {
        if (!MouseLock) return false;
        return Input.GetMouseButton(0);
    }

    public float GetCameraDistanceDelta()
    {
        if (!MouseLock) return 0f;
        return -Input.GetAxis("Mouse ScrollWheel");
    }

    public bool GetInteraction()
    {
        if (!MouseLock) return false;
        return Input.GetKeyDown(KeyCode.F);
    }

    public bool GetInventory()
    {
        return Input.GetKeyDown(KeyCode.I);
    }

    public bool GetMouseMove()
    {
        return Input.GetKey(KeyCode.LeftAlt);
    }

    public bool GetSkill()
    {
        if (!MouseLock) return false;
        return Input.GetKeyDown(KeyCode.E);
    }
}
