using AutoMapper;
using BusinessLayer.DTOs;
using BusinessLayer.Services.Interfaces;
using Infrastructure.Repositories;
using JuiceWorld.Entities;

namespace BusinessLayer.Services;

public class TagService(IRepository<Tag> tagRepository, IMapper mapper) : ITagService
{
    public async Task<TagDto?> CreateTagAsync(TagDto tagDto)
    {
        var newTag = await tagRepository.CreateAsync(mapper.Map<Tag>(tagDto));
        return newTag is null ? null : mapper.Map<TagDto>(newTag);
    }

    public async Task<IEnumerable<TagDto>> GetAllTagsAsync()
    {
        var tags = await tagRepository.GetAllAsync();
        return mapper.Map<List<TagDto>>(tags);
    }

    public async Task<TagDto?> GetTagByIdAsync(int id)
    {
        var tag = await tagRepository.GetByIdAsync(id);
        return tag is null ? null : mapper.Map<TagDto>(tag);
    }

    public async Task<TagDto?> UpdateTagAsync(TagDto tagDto)
    {
        var updatedTag = await tagRepository.UpdateAsync(mapper.Map<Tag>(tagDto));
        return updatedTag is null ? null : mapper.Map<TagDto>(updatedTag);
    }

    public async Task<bool> DeleteTagByIdAsync(int id)
    {
        return await tagRepository.DeleteAsync(id);
    }
}
