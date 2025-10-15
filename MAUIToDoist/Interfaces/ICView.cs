using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MAUIToDoist.Interfaces
{
    public enum ToastLevel
    {
        Info,
        Success,
        Warning,
        Error
    }
    public interface ICView
    {
        /// <summary>
        /// Показать сообщение пользователю (например, alert, toast или div на странице)
        /// </summary>
        /// <param name="message">Текст сообщения</param>
        Task ShowMessage(string message, ToastLevel level = ToastLevel.Success);
    }
}
