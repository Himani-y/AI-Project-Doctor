using System.Diagnostics;
using UnityEditor;
using UnityEngine;
public static class DoctorRule
{
    public static DoctorIssue CheckRigidbody(GameObject target)
    {
        if (
            target.GetComponent<Rigidbody>() != null &&
            target.GetComponent<Collider>() == null
        )
        {
            return null;
        }

        if (
            target.GetComponent<Collider>() != null &&
            target.GetComponent<Rigidbody>() == null
        )
        {
            return new DoctorIssue(
                "Rigidbody",
                "Rigidbody is missing.",
                DoctorSeverity.Warning
            );
        }

        return null;
    }

    public static void FixRigidbody(GameObject target)
    {
        if (
            target.GetComponent<Rigidbody>() == null &&
            target.GetComponent<Collider>() != null
        )
        {
            Undo.AddComponent<Rigidbody>(target);
        }
    }
    public static DoctorIssue CheckCollider(GameObject target)
    {
        if (
            target.GetComponent<Rigidbody>() != null &&
            target.GetComponent<Collider>() == null
        )
        {
            return new DoctorIssue(
                "Collider",
                "Collider is missing.",
                DoctorSeverity.Critical
            );
        }

        return null;
    }

    public static void FixCollider(GameObject target)
    {
        if (
            target.GetComponent<Rigidbody>() != null &&
            target.GetComponent<Collider>() == null
        )
        {
            Undo.AddComponent<BoxCollider>(target);
        }
    }

    public static void FixTag(GameObject target, string expectedTag)
    {
        if (!target.CompareTag(expectedTag))
        {
            Undo.RecordObject(target, "Fix GameObject Tag");
            target.tag = expectedTag;
        }
    }
    public static DoctorIssue CheckTag(GameObject target, string expectedTag)
    {
        if (!target.CompareTag(expectedTag))
        {
            return new DoctorIssue(
                "Tag",
                $"Incorrect tag. Expected: {expectedTag}.",
                DoctorSeverity.Warning
            );
        }

        return null;
    }
    public static DoctorIssue CheckAudioSource(GameObject target)
    {
        AudioSource audioSource =
            target.GetComponent<AudioSource>();

        if (audioSource != null && audioSource.clip == null)
        {
            return new DoctorIssue(
                "AudioSource",
                "AudioSource is missing an AudioClip.",
                DoctorSeverity.Warning
            );
        }

        return null;
    }

    public static void FixAudioSource(GameObject target)
    {
        AudioSource audioSource = target.GetComponent<AudioSource>();

        if (audioSource != null && audioSource.clip == null)
        {
            UnityEngine.Debug.LogWarning(
                "Please assign an AudioClip in the GameManager AudioSource component.",
                target
            );
        }
    }
}