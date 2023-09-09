
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MontyTask.Data.Models;
public class Subscription
{
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
