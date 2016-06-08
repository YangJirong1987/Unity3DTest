using UnityEngine;

public class CCUpdater : MonoBehaviour
{
    private void Start()
    {
        if (!UnityEngine.Camera.main.orthographic)
        {
            Debug.LogError("CCUpdater:: Camera must be orthographic");
        }
    }

    private void OnEnable()
    {
    }

    private void OnDisable()
    {
        CCTouchMgr.UnsharedTouchManager();
        CCActionMgr.UnsharedActionMgr();
        CCGUIActionMgr.UnsharedActionMgr();
    }

    private void Update()
    {
        CCActionMgr.SharedActionManager.Update();
        CCTouchMgr.SharedTouchManager.Update();
    }

    private void OnGUI()
    {
        CCGUIActionMgr.SharedActionMgr.Update();
    }
}