using UnityEditor;
using UnityEngine;
using System.Collections.Generic;

public class ProjectDoctorWindow : EditorWindow
{
    private List<DoctorIssue> issues = new List<DoctorIssue>();
    private bool refreshAfterFix = false;

    private int healthScore = 100;
    private bool hasAnalyzed = false;
    private string aiDiagnosis = "";
    private string aiRecommendation = "";
    private Vector2 scrollPosition;
    [MenuItem("Tools/AI Project Doctor")]
    public static void ShowWindow()
    {
        GetWindow<ProjectDoctorWindow>("Project Doctor");
    }

    private void OnGUI()
    {
        
        if (refreshAfterFix)
        {
            refreshAfterFix = false;

            if (Selection.activeGameObject != null)
            {
                RunRules(Selection.activeGameObject);
                CalculateHealthScore();
                hasAnalyzed = true;

                aiDiagnosis = ProjectDoctorAI.GenerateDiagnosis(
                    Selection.activeGameObject,
                    issues
                );

                aiRecommendation = ProjectDoctorAI.GetRecommendation(
                    issues
                );
            }
        }
        scrollPosition = EditorGUILayout.BeginScrollView(scrollPosition);
        GUILayout.Label("AI Project Doctor", EditorStyles.boldLabel);

        EditorGUILayout.Space(10);

        GameObject selectedObject = Selection.activeGameObject;

        if (selectedObject != null)
        {
            EditorGUILayout.LabelField(
                "Selected Object:",
                selectedObject.name
            );

            EditorGUILayout.Space(10);


            if (GUILayout.Button("Analyze Selected"))
            {
                RunRules(selectedObject);
                CalculateHealthScore();
                hasAnalyzed = true;

                aiDiagnosis = ProjectDoctorAI.GenerateDiagnosis(
                    selectedObject,
                    issues
                );

                aiRecommendation = ProjectDoctorAI.GetRecommendation(
    issues
);
            }
            if (hasAnalyzed)
            {
                EditorGUILayout.Space(10);

                GUIStyle healthTitleStyle = new GUIStyle(EditorStyles.boldLabel);
                healthTitleStyle.fontSize = 18;
                healthTitleStyle.alignment = TextAnchor.MiddleCenter;

                GUIStyle scoreStyle = new GUIStyle(EditorStyles.boldLabel);
                scoreStyle.fontSize = 32;
                scoreStyle.alignment = TextAnchor.MiddleCenter;

                EditorGUILayout.BeginVertical("box");

                GUILayout.Label("PROJECT HEALTH", healthTitleStyle);

                EditorGUILayout.Space(5);

                GUILayout.Label(
                    $"{healthScore}%",
                    scoreStyle
                );

                EditorGUILayout.Space(5);

                if (healthScore == 100)
                {
                    EditorGUILayout.HelpBox(
                        "✓ HEALTHY — No issues detected",
                        MessageType.Info
                    );
                }
                else if (healthScore >= 50)
                {
                    EditorGUILayout.HelpBox(
                        "⚠ ATTENTION — Issues detected",
                        MessageType.Warning
                    );
                }
                else
                {
                    EditorGUILayout.HelpBox(
                        "✖ CRITICAL — Immediate attention required",
                        MessageType.Error
                    );
                }

                EditorGUILayout.EndVertical();
            }

            if (hasAnalyzed && !string.IsNullOrEmpty(aiDiagnosis))
            {
                EditorGUILayout.Space(10);

                GUIStyle aiTitleStyle = new GUIStyle(EditorStyles.boldLabel);
                aiTitleStyle.fontSize = 17;

                GUIStyle sectionStyle = new GUIStyle(EditorStyles.boldLabel);
                sectionStyle.fontSize = 13;

                GUIStyle textStyle = new GUIStyle(EditorStyles.label);
                textStyle.wordWrap = true;
                textStyle.fontSize = 12;

                EditorGUILayout.BeginVertical("box");

                GUILayout.Label(
                    "🤖  AI DIAGNOSTIC ANALYSIS",
                    aiTitleStyle
                );

                EditorGUILayout.Space(8);

                GUILayout.Label(
                    "DIAGNOSIS",
                    sectionStyle
                );

                EditorGUILayout.LabelField(
                    aiDiagnosis,
                    textStyle
                );

                EditorGUILayout.Space(8);

                GUILayout.Label(
                    "RECOMMENDATION",
                    sectionStyle
                );

                EditorGUILayout.LabelField(
                    aiRecommendation,
                    textStyle
                );

                EditorGUILayout.EndVertical();
            }

            EditorGUILayout.Space(10);

            if (issues.Count > 0)
            {
                GUIStyle issueHeaderStyle = new GUIStyle(EditorStyles.boldLabel);
                issueHeaderStyle.fontSize = 16;

                GUILayout.Label(
                    $"⚠  ISSUES FOUND  ({issues.Count})",
                    issueHeaderStyle
                );

                int warningCount = 0;
                int criticalCount = 0;

                

                foreach (DoctorIssue issue in issues)
                {
                    

                    if (issue.severity == DoctorSeverity.Warning)
                    {
                        warningCount++;
                    }
                    else if (issue.severity == DoctorSeverity.Critical)
                    {
                        criticalCount++;
                    }
                }

                EditorGUILayout.Space(3);

                EditorGUILayout.LabelField(
                    $"Warnings: {warningCount}     Critical: {criticalCount}",
                    EditorStyles.boldLabel
                );

                EditorGUILayout.Space(5);

                GUIStyle issueBoxStyle = new GUIStyle("box");
                issueBoxStyle.padding = new RectOffset(10, 10, 8, 8);
                issueBoxStyle.margin = new RectOffset(0, 0, 0, 8);

                GUIStyle fixButtonStyle = new GUIStyle(GUI.skin.button);
                fixButtonStyle.fontStyle = FontStyle.Bold;
                fixButtonStyle.fixedHeight = 28;

                foreach (DoctorIssue issue in issues)
                {
                    EditorGUILayout.BeginVertical(issueBoxStyle);

                    MessageType messageType =
                        issue.severity == DoctorSeverity.Critical
                            ? MessageType.Error
                            : MessageType.Warning;

                    EditorGUILayout.HelpBox(
                        issue.message,
                        messageType
                    );

                    if (issue.ruleName == "Rigidbody")
                    {
                        if (GUILayout.Button("Fix Rigidbody", fixButtonStyle))
                        {
                            DoctorRule.FixRigidbody(selectedObject);

                            refreshAfterFix = true;

                        }
                    }
                    if (issue.ruleName == "Collider")
                    {
                        if (GUILayout.Button("Fix Collider", fixButtonStyle))
                        {
                            DoctorRule.FixCollider(selectedObject);

                            refreshAfterFix = true;

                        }
                    }
                    if (issue.ruleName == "Tag")
                    {
                        if (GUILayout.Button("Fix Tag", fixButtonStyle))
                        {
                            DoctorRule.FixTag(selectedObject, "Player");

                            refreshAfterFix = true;

                        }
                    }
                    if (issue.ruleName == "AudioSource")
                    {
                        if (GUILayout.Button("Fix AudioSource", fixButtonStyle))
                        {
                            DoctorRule.FixAudioSource(selectedObject);

                            refreshAfterFix = true;

                        }
                    }
                    EditorGUILayout.Space(5);
                    EditorGUILayout.EndVertical();
                }
            }
            else
            {
                EditorGUILayout.HelpBox(
    "✓ Project Doctor: Healthy",
    MessageType.Info
);
            }
        }
        else
        {
            EditorGUILayout.LabelField("Selected Object:", "None");
        }
        EditorGUILayout.EndScrollView();
    }
    private void CalculateHealthScore()
    {
        healthScore = 100;

        foreach (DoctorIssue issue in issues)
        {
            if (issue.severity == DoctorSeverity.Critical)
            {
                healthScore -= 30;
            }
            else if (issue.severity == DoctorSeverity.Warning)
            {
                healthScore -= 15;
            }
        }

        healthScore = Mathf.Clamp(healthScore, 0, 100);
    }
    private void RunRules(GameObject selectedObject)
    {
        issues.Clear();

        DoctorIssue rigidbodyIssue =
            DoctorRule.CheckRigidbody(selectedObject);

        if (rigidbodyIssue != null)
        {
            issues.Add(rigidbodyIssue);
        }

        DoctorIssue colliderIssue =
            DoctorRule.CheckCollider(selectedObject);

        if (colliderIssue != null)
        {
            issues.Add(colliderIssue);
        }

        if (selectedObject.name == "Player")
        {
            DoctorIssue tagIssue =
                DoctorRule.CheckTag(selectedObject, "Player");

            if (tagIssue != null)
            {
                issues.Add(tagIssue);
            }
        }
        DoctorIssue audioIssue =
            DoctorRule.CheckAudioSource(selectedObject);

        if (audioIssue != null)
        {
            issues.Add(audioIssue);
        }
        
    }
}