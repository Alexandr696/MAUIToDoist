namespace MAUIToDoist.Interfaces
{
    public interface ICView
    {
        /// <summary>
        /// Показать сообщение с возможной блокировкой экрана
        /// </summary>
        Task ShowMessage(string message, ToastLevel level = ToastLevel.Info, int duration = 3000, bool blockScreen = false);

        /// <summary>
        /// Принудительно скрыть сообщение и разблокировать экран
        /// </summary>
        Task HideMessage();
    }

    public enum ToastLevel
    {
        Info,
        Success,
        Warning,
        Error
    }
}
