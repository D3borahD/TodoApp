namespace Domain.DTO;

public class TimeEntryCreateDto
{
    private float _workload;
    
    public int Id { get; set; }
    public int UserId { get; set; }
    public DateTime WorkDate { get; set; }
    public float Workload { 
        get => _workload; 
        set => _workload = value;
    }

    public int ActivityId { get; set; } 
    public int TeamId { get; set; } 
    public int ProductId { get; set; } 

    public int ModuleId { get; set; } 
    
    public int SpecificProjectId { get; set; }
    public string? Comment { get; set; }
    
    public bool IsValid(out string? errorMessage)
    { 
        // Vérification du workload
        if (Workload is not (0 or 0.25f or 0.5f or 0.75f or 1f))
        {
            errorMessage = $"Invalid workload value: {Workload}. Allowed values are 0, 0.25, 0.5, 0.75 or 1.";
            return false;
        }

        errorMessage = null;
        return true;
    }
}