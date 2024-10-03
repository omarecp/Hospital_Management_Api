using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hma_api.Application.DTOs
{
  public class ApiResponse<T>
  {
    public bool Success { get; set; }
    public string Message { get; set; }
    public T Data { get; set; }
    public int? ErrorCode { get; set; }

    public static ApiResponse<T> CreateResponse(T data, string message = "")
    {
      return new ApiResponse<T>
      {
        Success = true,
        Message = message,
        Data = data
      };
    }

    public static ApiResponse<T> CreateError(string message, int? errorCode = null)
    {
      return new ApiResponse<T>
      {
        Success = false,
        Message = message,
        Data = default,
        ErrorCode = errorCode
      };
    }
  }
}
