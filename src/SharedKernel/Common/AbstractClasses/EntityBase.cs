using System.ComponentModel.DataAnnotations;
using SharedKernel.Common.Interfaces;

namespace SharedKernel.Common.AbstractClasses;

public abstract class EntityBase : IEntity
{
    [Key] public int Id { get; set; }
}