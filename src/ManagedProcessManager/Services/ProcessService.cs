namespace ManagedProcessManager.Services;

public enum TaskItemStatus
{
    Created,
    Ready,
    InProgress,
    Complete,
    Failed,
    NotApplicable,
    Rejected
}

public class TaskItem
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Title { get; set; } = "";
    public bool IsCompleted { get; set; }
    public string Details { get; set; } = "";
    public string Owner { get; set; } = "";
    public TaskItemStatus Status { get; set; } = TaskItemStatus.Created;
    public DateTime? DateCompleted { get; set; }
    public string Condition { get; set; } = "";
    public string AutomationKey { get; set; } = "";
    public string Group { get; set; } = "";
    public string Type { get; set; } = "";
}

public class ManagedProcess
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Name { get; set; } = "";
    public List<TaskItem> Tasks { get; set; } = new();
}

public class ProcessService
{
    public List<ManagedProcess> Processes { get; } = new();

    public ProcessService()
    {
        var seed = new ManagedProcess { Name = "My First Process" };
        foreach (var s in Enum.GetValues<TaskItemStatus>())
            seed.Tasks.Add(new TaskItem { Title = $"{StatusLabel(s)} Task", Status = s });
        Processes.Add(seed);
    }

    public ManagedProcess? GetProcess(Guid id) =>
        Processes.FirstOrDefault(p => p.Id == id);

    public ManagedProcess AddProcess(string name)
    {
        var proc = new ManagedProcess { Name = name.Trim() };
        Processes.Add(proc);
        return proc;
    }

    public void DeleteProcess(Guid id) => Processes.RemoveAll(p => p.Id == id);

    public static string StatusLabel(TaskItemStatus s) => s switch
    {
        TaskItemStatus.InProgress    => "In Progress",
        TaskItemStatus.NotApplicable => "Not Applicable",
        _                            => s.ToString()
    };
}
