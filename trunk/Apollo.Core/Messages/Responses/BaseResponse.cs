using System;
using System.Collections.Generic;
using System.Linq;
using Apollo.Core.Base;
using FluentValidation.Results;

namespace Apollo.Core.Messages.Responses
{
    public abstract class BaseResponse : IBaseResponse
    {
        protected BaseResponse()
        {
            Errors = new List<Failure>();
        }

        public IList<Failure> Errors { get; set; }
        public bool IsSuccessful => Errors.Count == 0;
        public string Message { get; set; }

        public void AddError(Exception ex)
        {
            Errors.Add(new Failure(ex));
            Message = ex.Message;
        }

        public virtual object GetContent()
        {
            throw new NotImplementedException();
        }

        public void AddError(string message)
        {
            Errors.Add(new Failure(message));
            Message = message;
        }
        public void AddErrors(IEnumerable<ValidationFailure> failures)
        {
            ((List<Failure>)Errors)
                .AddRange(failures.Select(e => new Failure(e)));
        }

        public IBaseResponse FromValidationResult(ValidationResult result)
        {
            ((List<Failure>) Errors)
                .AddRange(result.Errors.Select(e => new Failure(e)));
            Message = string.Join<ValidationFailure>(Environment.NewLine, result.Errors.ToArray());

            return this;
        }

        public IBaseResponse FromValidationResult(IEnumerable<ValidationResult> result)
        {
            foreach (var validationResult in result)
            {
                ((List<Failure>) Errors)
                    .AddRange(validationResult.Errors.Select(e => new Failure(e)));
                Message += string.Join<ValidationFailure>(Environment.NewLine, validationResult.Errors.ToArray());
            }

            return this;
        }

    }

    public static class BaseResponseExtentions
    {
        public static TOut Join<TOut>(this IBaseResponse response, IBaseResponse source) where TOut : IBaseResponse
        {
            response.Message = $@"{response.Message}{Environment.NewLine}{source.Message}";
            ((List<Failure>) response.Errors)
                .AddRange(source.Errors);

            return (TOut) response;
        }
    }

    public interface IBaseResponse
    {
        IList<Failure> Errors { get; set; }
        bool IsSuccessful { get; }
        string Message { get; set; }
        void AddError(Exception ex);
        object GetContent();
    }

    public interface IBaseResponse<out T> : IBaseResponse
    {
        T Content { get; }
    }
}