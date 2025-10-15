using MAUIToDoist.Interfaces;
using MAUIToDoist.Models;
using MAUIToDoist.Services;
using Microsoft.AspNetCore.Components;
using System.Threading.Tasks;

namespace MAUIToDoist.Components.Pages
{
    public partial class Home
    {
        [Inject] private TodoApiService? Http { get; set; }
        [Inject] private BlazorViewService? Toast { get; set; }
        private List<TodoItem> List { get; set; } = new List<TodoItem>();

        string? Message;
        string MessageClass = "";
        bool IsLoading = false;

        protected override void OnInitialized()
        {
            if (Toast == null) return;
            Toast.OnMessage += ShowMessageAsync;
        }

        private async Task ShowMessageAsync(string msg, ToastLevel level)
        {
            Message = msg;
            MessageClass = level switch
            {
                ToastLevel.Info => "toast-info",
                ToastLevel.Success => "toast-success",
                ToastLevel.Warning => "toast-warning",
                ToastLevel.Error => "toast-error",
                _ => "toast-info"
            };
            StateHasChanged();
            await Task.Delay(3000);
            Message = null;
            StateHasChanged();
        }

        public void Dispose()
        {
            if (Toast != null)
                Toast.OnMessage -= ShowMessageAsync;
        }

        

        public async Task Click()
        {
            if (Http == null) return;

            try
            {
                IsLoading = true;
                StateHasChanged();
                List = await Http.GetTodosAsync();
            }
            finally
            {
                IsLoading = false;
                StateHasChanged();
            }
        }
    }
}
