using System.ComponentModel.DataAnnotations;

namespace EF_SplitQuery_LoadError_Tests.Models;

public class ManyItemOneWithBackreference
{
    [Key]
    public int Id { get; set; }

    public List<ManyItemTwoWithBackreference> Items { get; set; } = new();
}