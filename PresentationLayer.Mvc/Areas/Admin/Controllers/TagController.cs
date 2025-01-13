using AutoMapper;
using BusinessLayer.DTOs;
using BusinessLayer.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using PresentationLayer.Mvc.ActionFilters;
using PresentationLayer.Mvc.Models;

namespace PresentationLayer.Mvc.Areas.Admin.Controllers;

[Area(Constants.Areas.Admin)]
[RedirectIfNotAdminActionFilter]
public class TagController(ITagService tagService, IMapper mapper) : Controller
{
    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var tags = await tagService.GetAllTagsAsync();
        return View(mapper.Map<List<TagViewModel>>(tags));
    }

    [HttpGet]
    public IActionResult Create()
    {
        return View(new TagViewModel { Name = "" });
    }

    [HttpPost]
    public async Task<IActionResult> Create(TagViewModel viewModel)
    {
        if (!ModelState.IsValid)
        {
            return View(viewModel);
        }

        var createdTag = await tagService.CreateTagAsync(mapper.Map<TagDto>(viewModel));
        if (createdTag == null)
        {
            ModelState.AddModelError("Id", "Failed to create tag.");
            return View(viewModel);
        }

        return RedirectToAction(nameof(Index));
    }

    [HttpGet]
    public async Task<IActionResult> Edit(int id)
    {
        var tag = await tagService.GetTagByIdAsync(id);
        if (tag == null)
        {
            ModelState.AddModelError("Id", "Tag not found.");
            return View(tag);
        }

        return View(tag);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(TagDto viewModel)
    {
        if (!ModelState.IsValid) return View(viewModel);

        var updatedTag = await tagService.UpdateTagAsync(viewModel);
        if (updatedTag == null)
        {
            ModelState.AddModelError("Id", "Failed to update tag.");
            return View(viewModel);
        }

        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    public async Task<IActionResult> Delete(int id)
    {
        var isDeleted = await tagService.DeleteTagByIdAsync(id);
        if (!isDeleted)
        {
            ModelState.AddModelError("Id", "Failed to delete tag.");
            return View(nameof(Index));
        }

        return RedirectToAction(nameof(Index));
    }
}
