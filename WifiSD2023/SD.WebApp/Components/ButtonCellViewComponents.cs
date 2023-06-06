using GridShared;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewComponents;
using Wifi.SD.Core.Application.Movies.Results;

namespace SD.WebApp.Components
{
    public class ButtonCellViewComponents : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync(object Item, IGrid Grid, object Object)
        {
            Guid Id = ((MovieDto)Item).Id;
            ViewData["gridState"] = Grid.GetState();
            ViewData["returnUrl"] = (string)Object;

            var factory = Task<IViewComponentResult>.Factory;
            return await factory.StartNew(() => this.View(Id));

        }
    }
}
