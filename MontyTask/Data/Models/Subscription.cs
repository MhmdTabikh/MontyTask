
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MontyTask.Data.Models;
public class Subscription
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public SubscriptionType SubscriptionType { get; set; }
    public User User { get; set; } = new();

}

public enum SubscriptionType
{
    Guest = 0,
    Standard,
    Premium
}
