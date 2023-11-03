using System.ComponentModel.DataAnnotations;

namespace EF_SplitQuery_LoadError_Tests.Models;

public class ManyItemTwoWithoutBackreference
{
    [Key]
    public int Id { get; set; }
}