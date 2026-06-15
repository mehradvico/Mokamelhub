using System;
using System.Collections.Generic;

namespace Application.Common.Dto.Result
{
    public class BaseResultDto<TData> : BaseResultDto
    {
        public BaseResultDto(bool isSuccess, List<Tuple<string, string>> messages, TData data, int code = 0) : base(isSuccess, messages, code)
        {
            Data = data;
        }
        public BaseResultDto(bool isSuccess, TData data, int code = 0) : base(isSuccess, code)
        {

            Data = data;
        }
        public BaseResultDto(bool isSuccess, string val, TData data, int code = 0) : base(isSuccess, val, code)
        {
            Data = data;

        }
        public BaseResultDto(bool isSuccess, string val1, string val2, TData data, int code = 0) : base(isSuccess, val1, val2, code)
        {
            Data = data;
        }
        public TData Data { get; private set; }
    }
    public class BaseResultDto
    {
        public BaseResultDto(bool isSuccess, string val, int code = 0)
        {
            IsSuccess = isSuccess;
            var message = new Tuple<string, string>(val, "");
            Messages = new List<Tuple<string, string>> { message };
            Code = code;
        }
        public BaseResultDto(bool isSuccess, string val1, string val2, int code = 0)
        {
            IsSuccess = isSuccess;
            var message = new Tuple<string, string>(val1, val2);
            Messages = new List<Tuple<string, string>> { message };
            Code = code;
        }

        public BaseResultDto(bool isSuccess, List<Tuple<string, string>> messages, int code = 0)
        {
            IsSuccess = isSuccess;
            Messages = messages;
            Code = code;
        }
        public BaseResultDto(bool isSuccess, int code = 0)
        {
            IsSuccess = isSuccess;
            Messages = new List<Tuple<string, string>>();
            Code = code;
        }
        public int Code { get; private set; }
        public bool IsSuccess { get; private set; }
        public List<Tuple<string, string>> Messages { get; private set; }
    }

}
