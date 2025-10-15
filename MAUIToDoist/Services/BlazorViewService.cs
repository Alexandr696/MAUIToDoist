using MAUIToDoist.Interfaces;

namespace MAUIToDoist.Services
{
    public class BlazorViewService : ICView
    {
        public event Func<string, ToastLevel, Task>? OnMessage;

        public async Task ShowMessage(string message, ToastLevel level = ToastLevel.Info)
        {
            if (OnMessage != null)
                await OnMessage.Invoke(message, level);
        }
    }
}
