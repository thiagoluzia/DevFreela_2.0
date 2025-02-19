namespace DevFreela.Application.Models
{
    public class ResultViewModel
    {
        public ResultViewModel(bool iSuccess = true, string message = "")
        {
            ISuccess = iSuccess;
            Message = message;
        }

        public bool ISuccess { get; private set; }
        public string Message { get; private set; }

        public static ResultViewModel Error(string message)
        {
            return new ResultViewModel(false, message);
        }

        public static ResultViewModel Success()
        {
            return new ResultViewModel();
        }
    }

    public class ResultViewModel<T> : ResultViewModel
    {
        public ResultViewModel(T? data, bool isSucces = true, string message = "")
            : base(isSucces, message)
        {
            Data = data;
        }


        public T? Data { get; private set; }


        public static ResultViewModel<T> Success(T data)
        {
            return new ResultViewModel<T>(data);
        }

        //public static ResultViewModel<T> SuccessSimplificado(T data)
        //    => new(data);
        
        public static ResultViewModel<T> Error(string message)
        {
            return new ResultViewModel<T>(default, false, message);
        }
    }
}
