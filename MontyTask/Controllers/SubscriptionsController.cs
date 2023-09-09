using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MontyTask.Data.Models;
using MontyTask.Data.Resources;
using MontyTask.Helpers;
using MontyTask.Services;

namespace MontyTask.Controllers;

[ApiController]
[Authorize]
[Route("/api/subscriptions")]
public class SubscriptionsController : ControllerBase
{
    private readonly IMapper _mapper;
    private readonly ILogger<SubscriptionsController> _logger;
    private readonly ISubscriptionService _subscriptionService;

    public SubscriptionsController(ISubscriptionService subscriptionService,
        IMapper mapper,
        ILogger<SubscriptionsController> logger)
    {
        _subscriptionService = subscriptionService;
        _mapper = mapper;
        _logger = logger;
    }

    [HttpGet]
    [Route("{userEmail}")]
    public async Task<IActionResult> GetSubscriptionsByUserEmail(string userEmail)
    {
        try
        {
            var response = await _subscriptionService.GetSubscriptionsByEmailAsync(userEmail);

            if (!response.Success)
            {
                return BadRequest(response.Message);
            }

            var subscriptionResource = _mapper.Map<List<Subscription>, List<SubscriptionResource>>(response.Subscriptions);

            return Ok(subscriptionResource);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.ExtractMessage());
            return StatusCode(500, "Internal server error");
        }
    }

    [HttpDelete]
    public async Task<IActionResult> DeleteSubscription([FromBody] UserSubscriptionResource userSubscriptionResource)
    {
        //ToDo: call _subscriptionService.DeleteSubscription(...)
        //if subscription is found,delete it,otherwise return that subscription was not found

        return Ok();
    }

    [HttpPost]
    public async Task<IActionResult> AddSubscription([FromBody] AddUserSubscriptionResource addUserSubscriptionResource)
    {
        try
        {
             await _subscriptionService.AddSubscription(addUserSubscriptionResource.UserEmail,
                                                                      addUserSubscriptionResource.SubscriptionType,
                                                                      addUserSubscriptionResource.StartDate,
                                                                      addUserSubscriptionResource.EndDate);

            return Ok("Subscription Added successfully");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.ExtractMessage());
            return StatusCode(500, "Internal server error");
        }
    }
}

