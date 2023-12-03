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
    private Coroutine _panCameraCoroutine;

    [SerializeField] private float _fallPanAmount = 0.25f;
    [SerializeField] private float _fallPanYTime = 0.35f;

    public float _fallSpeedYDampingChangeThreshold = -15f;

    public bool IsLerpingYdamping { get; private set; } 
    public bool LerpedFromPlayerFalling { get; set; }

    private CinemachineVirtualCamera _currentCamera;
    private CinemachineFramingTransposer _framingTransposer;

    private float _normYPanAmount;
    private Vector2 _startingTrackedObjectOffset;

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

        _startingTrackedObjectOffset = _framingTransposer.m_TrackedObjectOffset;
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

    public void PanCameraOnContact( float panDistance, float panTime, PanDirection panDirection, bool panToStartingPoint )
    {
        _panCameraCoroutine = StartCoroutine(PanCamera(panDistance, panTime, panDirection, panToStartingPoint));
    }

    private IEnumerator PanCamera(float panDistance, float panTime, PanDirection panDirection, bool panToStartingPoint)
    {
        Vector2 endPos = Vector2.zero;
        Vector2 startingPos = Vector2.zero;

        if (!panToStartingPoint)
        {
            switch (panDirection)
            {
                case PanDirection.up: endPos = Vector2.up; break;
                case PanDirection.down: endPos = Vector2.down; break;
                case PanDirection.left: endPos = Vector2.left; break;
                case PanDirection.right : endPos = Vector2.right; break;
            }
            endPos *= panDistance;
        }
        else
        {
            startingPos = _framingTransposer.m_TrackedObjectOffset;
            endPos = _startingTrackedObjectOffset;
        }

        float elapsedTime = 0f;
        while (elapsedTime < panTime)
        {
            elapsedTime += Time.deltaTime;

            Vector3 pantLerp = Vector3.Lerp(startingPos, endPos, (elapsedTime / panTime) );
            _framingTransposer.m_TrackedObjectOffset = pantLerp;

            yield return null;
        }
    }

    public void SwapCameras( CinemachineVirtualCamera cameraFromLeft, CinemachineVirtualCamera cameraFromRight, Vector2 triggerExitDirection )
    {
        if (_currentCamera == cameraFromLeft && triggerExitDirection.x >0f)
        {
            cameraFromRight.enabled = true;

            cameraFromLeft.enabled = false;

            _currentCamera = cameraFromRight;

            _framingTransposer = _currentCamera.GetCinemachineComponent<CinemachineFramingTransposer>();
        }
        else if (_currentCamera == cameraFromRight && triggerExitDirection.x < 0f)
        {
            cameraFromLeft.enabled = true;

            cameraFromRight.enabled = false;

            _currentCamera = cameraFromLeft;

            _framingTransposer = _currentCamera.GetCinemachineComponent<CinemachineFramingTransposer>();
        }

    }

}
