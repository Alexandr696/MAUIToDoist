using MAUIToDoist.Interfaces;

namespace MAUIToDoist.Services
{
    public class BlazorViewService : ICView
    {
        public event Func<string, ToastLevel, int, bool, Task>? OnMessage;
        public event Func<Task>? OnHide;

        public async Task ShowMessage(string message, ToastLevel level = ToastLevel.Info, int duration = 3000, bool blockScreen = false)
        {
            if (OnMessage != null)
                await OnMessage.Invoke(message, level, duration, blockScreen);
        }

        public async Task HideMessage()
        {
            if (OnHide != null)
                await OnHide.Invoke();
        }
    }
}
