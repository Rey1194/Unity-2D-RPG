using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraManager : MonoBehaviour
{
    public static CameraManager instance;
    
    [SerializeField] private CinemachineVirtualCamera[] _allVirtualCameras;

    [Header("Controls for lerping the Y damping during the player jump/fall")]
    private Coroutine _lerpYPanCoroutine;
    [SerializeField] private float _fallPanAmount = 0.25f;
    [SerializeField] private float _fallPanYTime = 0.35f;
    public float _fallSpeedYDampingChangeThreshold = -15f;

    public bool IsLerpingYdamping { get; private set; } 
    public bool LerpedFromPlayerFalling { get; set; }

    private CinemachineVirtualCamera _currentCamera;
    private CinemachineFramingTransposer _framingTransposer;

    private float _normYPanAmount;

    private void Awake()
    {
        instance = this;

        for ( int i = 0; i < _allVirtualCameras.Length; i++ )
        {
            if (_allVirtualCameras[i].enabled  )
            {
                // set the current active camera
                _currentCamera = _allVirtualCameras[i];
                // set the framing transposer
                _framingTransposer = _currentCamera.GetCinemachineComponent<CinemachineFramingTransposer>();
            }
        }

        _normYPanAmount = _framingTransposer.m_YDamping;
    }

    public void LerpYDamping(bool isPlayerFalling)
    {
        _lerpYPanCoroutine = StartCoroutine(LerpYAction(isPlayerFalling));
    }

    private IEnumerator LerpYAction(bool isPlayerFalling)
    {
        IsLerpingYdamping = true;

        // grap the starting damping amount
        float startDampAmount = _framingTransposer.m_YDamping;
        float endDampAmount = 0;

        // determine the damping amount
        if (isPlayerFalling)
        {
            endDampAmount = _fallPanAmount;
            LerpedFromPlayerFalling = true;
        }
        else
        {
            endDampAmount = _fallPanAmount;
        }
        // lerp the pan amount
        float elapsedTime = 0f;
        while(elapsedTime < _fallPanAmount)
        {
            elapsedTime += Time.deltaTime;

            float lerpedPanAmount = Mathf.Lerp(startDampAmount, endDampAmount, (elapsedTime / _fallPanYTime));
            _framingTransposer.m_YDamping = lerpedPanAmount;

            yield return null;
        }

        IsLerpingYdamping = false;
    }
}
