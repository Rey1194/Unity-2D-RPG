using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowObject : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Transform _playerTransform;

    [Header("Flip Rotation Stats")]
    [SerializeField] private float _flipYRotationTime = 0.5f;

    private Coroutine _turnCoroutine;

    private PlayerController _playerController;
    private bool _isfacingRight;

    private void Awake()
    {
        _playerController = _playerTransform.GetComponent<PlayerController>();
        _isfacingRight = _playerController.facingRight;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = _playerTransform.position;
    }

    public void CallTurn()
    {
        _turnCoroutine = StartCoroutine(FlipYLerp());
    }

    private IEnumerator FlipYLerp()
    {
        float startRotation = this.transform.localEulerAngles.y;
        float endRotation = DetermineEndRotation();
        float yRotation = 0f;
        float elapsedTime = 0;
        while (elapsedTime < _flipYRotationTime)
        {
            elapsedTime += Time.deltaTime;
            // Lerp the rotation
            yRotation = Mathf.Lerp(startRotation, endRotation, (elapsedTime / _flipYRotationTime));
            transform.rotation = Quaternion.Euler(0f, yRotation, 0f);

            yield return null;
        }

    }

    private float DetermineEndRotation ()
    {
        _isfacingRight = !_isfacingRight;
        if (_isfacingRight)
        {
            return 0;
        }
        else
        {
            return 180f;
        }
    }
}
