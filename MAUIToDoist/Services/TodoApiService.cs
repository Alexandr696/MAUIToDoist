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

        public async Task<ApiResult<List<TodoItem>>> GetTodosAsync()
        {
            try
            {
                var result = await _http.GetFromJsonAsync<List<TodoItem>>("api/todo");

                return new ApiResult<List<TodoItem>>
                {
                    Status = ApiResultStatus.Success,
                    Message = "Список успешно загружен!",
                    Data = result
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ошибка при загрузке списка задач");
                return new ApiResult<List<TodoItem>>
                {
                    Status = ApiResultStatus.Error,
                    Message = "Не удалось загрузить список задач!",
                    Data = new List<TodoItem>()
                };
            }
        }

        /*public async Task AddTodoAsync(TodoItem item)
            => await _http.PostAsJsonAsync("api/todo", item);*/

    }
}
