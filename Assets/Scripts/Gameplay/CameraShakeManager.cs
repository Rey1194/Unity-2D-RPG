using UnityEngine;
using Cinemachine;

public class CameraShakeManager : MonoBehaviour
{
    public static CameraShakeManager instance;
    
    [SerializeField] private float globalShakeForce = 1f;
    [SerializeField] CinemachineImpulseListener impulseListener;
    private CinemachineImpulseDefinition impulseDefinition;
    

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    public void CameraShake(CinemachineImpulseSource impulseSource )
    {
        impulseSource.GenerateImpulseWithForce(globalShakeForce);
    }
    
    public void ScreenShakeFromProfile(CinemachineImpulseSource impulseSource, ScreenShakeProfile profile ) {
        // apply Settings
        SetupScreenShakeSettings(impulseSource, profile);
        // screenshake
        impulseSource.GenerateImpulseWithForce(profile.impactForce);
    }
    
    private void SetupScreenShakeSettings(CinemachineImpulseSource impulseSource, ScreenShakeProfile profile ) {
        impulseDefinition = impulseSource.m_ImpulseDefinition;
        
        // change the impulse source settings
        impulseDefinition.m_ImpulseDuration = profile.impulseTime;
        impulseSource.m_DefaultVelocity = profile.defaultVelocity;
        impulseDefinition.m_CustomImpulseShape = profile.impulseCurve;
        
        // change the impulse listener settings 
        impulseListener.m_ReactionSettings.m_AmplitudeGain = profile.listenerAmplitude;
        impulseListener.m_ReactionSettings.m_FrequencyGain = profile.listenerFrecuency;
        impulseListener.m_ReactionSettings.m_Duration = profile.listenerDuration;
    }
}
