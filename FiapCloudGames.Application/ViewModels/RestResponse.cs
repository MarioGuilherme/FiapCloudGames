namespace FiapCloudGames.Application.ViewModels;

public class RestResponse {
    public IEnumerable<dynamic>? Errors { get; set; }

    public static RestResponse Success() => new();

    public static RestResponse Error(IEnumerable<dynamic> errors) => new() { Errors = errors };
}

public class RestResponse<T> : RestResponse {
    public T? Data { get; private set; }

    public static RestResponse<T> Success(T? data) => new() { Data = data };

    public static new RestResponse<T> Error(IEnumerable<dynamic> errors) => new() { Errors = errors };
}
