using MAUIToDoist.Models;
using MAUIToDoist.Interfaces;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace MAUIToDoist.Services
{
    public class TodoApiService
    {
        private HttpClient _http;
        private ILogger<TodoApiService> _logger;
        private ICView _toast;
        public TodoApiService(HttpClient http, ILogger<TodoApiService> logger, ICView toast)
        {
            _http = http;
            _logger = logger;
            _toast = toast;
        }

        public async Task<List<TodoItem>> GetTodosAsync()
        {
            List<TodoItem> result = [];
            try
            {
                result = await _http.GetFromJsonAsync<List<TodoItem>>("api/todo") ?? [];
                await _toast.ShowMessage("Список успешно загружен!");
            }
            catch
            {
                _logger.LogError("Ошибка данных нет!");
                 await _toast.ShowMessage("Ошибка данных нет!", ToastLevel.Warning);
            }
            return result;
        }

        /*public async Task AddTodoAsync(TodoItem item)
            => await _http.PostAsJsonAsync("api/todo", item);*/

    }
}
