using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AudioSettings", menuName = "Settings/AudioSettings", order = 1)]
public class AudioSettings : ScriptableObject
{
    public float musicVolume = 0.5f;
    public float sfxVolume = 0.5f;
    public float masterVolume = 0.5f;
}
