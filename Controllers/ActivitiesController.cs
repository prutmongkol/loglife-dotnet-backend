using System.Security.Cryptography.X509Certificates;
using ActivitiesApi.Models;
using ActivitiesApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace ActivitiesApi.Controllers;

[ApiController]
[Route("[controller]")]
public class ActivitiesController : ControllerBase
{
    private readonly ActivitiesService _activitiesService;

    public ActivitiesController(ActivitiesService activitiesService) =>
        _activitiesService = activitiesService;

    [HttpGet]
    public async Task<List<Activity>> Get() =>
        await _activitiesService.GetActivitiesAsync();

    [HttpGet("{id}")]
    public async Task<ActionResult<Activity>> Get(string id)
    {
        var activity = await _activitiesService.GetActivityAsync(id);

        if (activity is null)
        {
            return NotFound();
        }

        return activity;;
    }

    [HttpPost]
    public async Task<IActionResult> Post(Activity newActivity)
    {
        await _activitiesService.CreateActivityAsync(newActivity);

        return CreatedAtAction(nameof(Get), new { id = newActivity.Id }, newActivity);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Put(string id, Activity updatedActivity)
    {
        var activity = await _activitiesService.GetActivityAsync(id);

        if (activity is null)
        {
            return NotFound();
        }

        updatedActivity.Id = activity.Id;

        await _activitiesService.UpdateActivityAsync(id, updatedActivity);

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(string id)
    {
        var activity = await _activitiesService.GetActivityAsync(id);

        if (activity is null)
        {
            return NotFound();
        }

        await _activitiesService.DeleteActivityAsync(id);

        return NoContent();
    }
}