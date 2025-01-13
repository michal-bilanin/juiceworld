using System.ComponentModel;

namespace PresentationLayer.Mvc.Models;

public class TagViewModel : BaseEntityViewModel
{
    public string Name { get; set; } = null!;

    [DisplayName("Color")]
    public string ColorHex { get; set; } = null!;
}
