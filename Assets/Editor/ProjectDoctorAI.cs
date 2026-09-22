using System.Collections.Generic;
using UnityEngine;

public static class ProjectDoctorAI
{
    public static string GetRecommendation(List<DoctorIssue> issues)
    {
        if (issues == null || issues.Count == 0)
        {
            return "No action required. The selected object is healthy.";
        }

        foreach (DoctorIssue issue in issues)
        {
            if (issue.ruleName == "Rigidbody")
            {
                return "Add a Rigidbody component to enable physics-based interaction.";
            }

            if (issue.ruleName == "Collider")
            {
                return "Add an appropriate Collider component for physics interaction.";
            }

            if (issue.ruleName == "Tag")
            {
                return "Assign the expected Tag to ensure tag-based gameplay logic works correctly.";
            }

            if (issue.ruleName == "AudioSource")
            {
                return "Assign an AudioClip to the existing AudioSource.";
            }
        }

        return "Review the detected issues and apply the recommended fixes.";
    }
    public static string GenerateDiagnosis(
        GameObject target,
        List<DoctorIssue> issues
    )
    {
        if (target == null)
        {
            return "No GameObject selected for diagnosis.";
        }

        if (issues == null || issues.Count == 0)
        {
            return
                "The selected object appears healthy. " +
                "No configuration issues were detected.";
        }

        string diagnosis = "";

        foreach (DoctorIssue issue in issues)
        {
            if (issue.ruleName == "Rigidbody")
            {
                diagnosis +=
                    "The object has a Collider but no Rigidbody. " +
                    "This may prevent physics-based interaction.\n\n" +
                    "Recommendation: Add a Rigidbody component " +
                    "to enable physics simulation.";
            }
            else if (issue.ruleName == "Collider")
            {
                diagnosis +=
                    "The object has a Rigidbody but no Collider. " +
                    "Physics interaction may not work correctly.\n\n" +
                    "Recommendation: Add an appropriate Collider " +
                    "component.";
            }
            else if (issue.ruleName == "Tag")
            {
                diagnosis +=
                    "The object is using an unexpected Tag. " +
                    "This can affect tag-based gameplay logic.\n\n" +
                    "Recommendation: Assign the expected Tag.";
            }
            else if (issue.ruleName == "AudioSource")
            {
                diagnosis +=
                    "An AudioSource exists but has no AudioClip assigned. " +
                    "Audio playback will not produce sound.\n\n" +
                    "Recommendation: Assign an AudioClip to the AudioSource.";
            }

            diagnosis += "\n\n";
        }

        return diagnosis.Trim();
    }
}