using MAUIToDoist.Interfaces;
using MAUIToDoist.Models;
using MAUIToDoist.Services;
using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MAUIToDoist.Components.Pages
{
    public partial class Home
    {
        [Inject] private TodoApiService? Http { get; set; }
        [Inject] private ICView? Toast { get; set; }

        private List<TodoItem> List { get; set; } = new();

        public async Task Click()
        {
            if (Http == null || Toast == null)
                return;

            // 👇 1. Просто блокируем экран (без текста)
            await Toast.ShowMessage("", ToastLevel.Info, duration: 0, blockScreen: true);

            ApiResult<List<TodoItem>> result;

            try
            {
                result = await Http.GetTodosAsync();
                if (result.Data != null)
                {
                    List = result.Data;
                    StateHasChanged();
                }
            }
            finally
            {
                // 👇 2. Разблокируем экран после завершения запроса
                await Toast.HideMessage();
            }

            // 👇 3. Показываем сообщение (уже после разблокировки)
            var toastLevel = result.Status switch
            {
                ApiResultStatus.Success => ToastLevel.Success,
                ApiResultStatus.Warning => ToastLevel.Warning,
                ApiResultStatus.Error => ToastLevel.Error,
                _ => ToastLevel.Info
            };

            await Toast.ShowMessage(result.Message ?? "Операция завершена", toastLevel);
            if (result.Data != null)
                List = result.Data;
        }
    }
}