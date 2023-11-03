using System.ComponentModel.DataAnnotations;

namespace EF_SplitQuery_LoadError_Tests.Models;

public class ManyItemOneWithoutBackreference
{
    [Key]
    public int Id { get; set; }

    public List<ManyItemTwoWithoutBackreference> Items { get; set; } = new();
}