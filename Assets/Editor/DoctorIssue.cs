public class DoctorIssue
{
    public string ruleName;
    public string message;
    public DoctorSeverity severity;

    public DoctorIssue(
        string ruleName,
        string message,
        DoctorSeverity severity
    )
    {
        this.ruleName = ruleName;
        this.message = message;
        this.severity = severity;
    }
}

public enum DoctorSeverity
{
    Warning,
    Critical
}