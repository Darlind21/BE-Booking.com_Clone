using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingClone.Application.Common.Behaviours
{
    //ValidationBehaviour class intercepts a request and if validation passes it allows the request to continue being processed, if they fail it throws error.
    //ValidationBehaviour is a generic class that works with any request type and returns any response type
    //It takes an list of validators(classes that implement AbstractValidator) that validate the request type
    //It implements IPipelineBehaviour(from MediatR), which means it acts like a middleware or a step in a chain that processed requests
        //IPipelineBehaviour represents a middleware step in the request processing pipeline.
        //It allows to intercept a request before it reaches the handler or after it returns.
    public class ValidationBehavior<TRequest, TResponse>(IEnumerable<IValidator<TRequest>> _validators) : IPipelineBehavior<TRequest, TResponse>
        where TRequest : notnull
    {
        //MediatR calls Handle() automatically when you send a request using .Send(). It takes:
        //-the request itself 
        //-A function(next) that represents the next step in the processing pipeline
        //-a cancellation token to cancel the operation if needed
        public async Task<TResponse> Handle
            (TRequest request,
            RequestHandlerDelegate<TResponse> next, //represents next step in a MediatR pipeline -- comes from MediatR
            CancellationToken cancellationToken)
        {
            if (_validators.Any()) //checks if there are any validators(Validation classes that implement AbstractValidator(can be multiple even though its not common)
                                   //avb for the current request
                                   //, if none provided it skips validation 
            {
                var context = new ValidationContext<TRequest>(request);
                //ValidationContext<T> is a helper class from FluentValidation that wraps the request obj,
                //and provides the validators all the info they need to run their rules.
                //creates a validation context that holds the request data. this context is passed to the validators so they know what to check 

                var validationTasks = _validators
                    .Select(v => v.ValidateAsync(context, cancellationToken)) //foreach validator v in _validators we call its async validation,
                                                                              //method ValidateAsync(), which returns a Task<ValidationResult> -
                                                                              //a task that eventually produces a validation result(Task is like a container for the result but not the result itself yet)
                                                                              //When we call the method we get the Task immediately but to get the T inside Task<T> we have to await the Task
                    .ToList();

                //once we have the list of validation tasks we run them in parallel
                var results = await Task.WhenAll(validationTasks);
                //Task.WhenAll is a method from System.Threading.Tasks namespace. It takes a list of tasks and return a single Task,
                //when ALL the tasks are finished


                var failures = results // collects all validation errors from all validators
                    .SelectMany(r => r.Errors)
                    .Where(f => f != null)
                    .ToList();

                if (failures.Count != 0)
                    throw new ValidationException(failures);
            }

            return await next(cancellationToken);
        }
    }
}
