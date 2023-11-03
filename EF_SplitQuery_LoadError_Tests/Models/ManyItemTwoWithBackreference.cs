using System.ComponentModel.DataAnnotations;

namespace EF_SplitQuery_LoadError_Tests.Models;

public class ManyItemTwoWithBackreference
{
    [Key]
    public int Id { get; set; }
    
    public List<ManyItemOneWithBackreference> Items { get; set; } = new();
}