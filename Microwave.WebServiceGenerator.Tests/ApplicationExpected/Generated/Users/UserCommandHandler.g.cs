//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Application.Users
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Domain;
    using Domain.Users;
    using Microsoft.AspNetCore.Mvc;
    using Application.Posts;
    
    
    public partial class UserCommandHandler
    {
        
        public IEventStore EventStore { get; private set; }
        
        public IUserRepository UserRepository { get; private set; }
        
        public IPostRepository PostRepository { get; private set; }
        
        public UserCommandHandler(IEventStore EventStore, IUserRepository UserRepository, IPostRepository PostRepository)
        {
            this.EventStore = EventStore;
            this.UserRepository = UserRepository;
            this.PostRepository = PostRepository;
        }
        
        public async Task<IActionResult> GetUsers()
        {
            var listResult = await UserRepository.GetUsers();
            return new OkObjectResult(listResult);
        }
        
        public async Task<IActionResult> GetUser(Guid id)
        {
            var result = await UserRepository.GetUser(id);
            if (result != null) return new JsonResult(result);
            return new NotFoundObjectResult(new List<string> { $"Could not find Root User with ID: {id}" });
        }
        
        public async Task<IActionResult> CreateUser(UserCreateCommand command)
        {
            CreationResult<User> createResult = User.Create(command);
            if (createResult.Ok)
            {
                var hookResult = await EventStore.AppendAll(createResult.DomainEvents);
                if (hookResult.Ok)
                {
                    await UserRepository.CreateUser(createResult.CreatedEntity);
                    return new CreatedResult("uri", createResult.CreatedEntity);
                }
                return new BadRequestObjectResult(hookResult.Errors);
            }
            return new BadRequestObjectResult(createResult.DomainErrors);
        }
        
        public async Task<IActionResult> UpdateAgeUser(Guid id, UserUpdateAgeCommand command)
        {
            var entity = await UserRepository.GetUser(id);
            if (entity != null)
            {
                var validationResult = entity.UpdateAge(command);
                if (validationResult.Ok)
                {
                    var hookResult = await EventStore.AppendAll(validationResult.DomainEvents);
                    if (hookResult.Ok)
                    {
                        await UserRepository.UpdateUser(entity);
                        return new OkResult();
                    }
                    return new BadRequestObjectResult(hookResult.Errors);
                }
                return new BadRequestObjectResult(validationResult.DomainErrors);
            }
            return new NotFoundObjectResult(new List<string> { $"Could not find Root User with ID: {id}" });
        }
        
        public async Task<IActionResult> UpdateNameUser(Guid id, UserUpdateNameCommand command)
        {
            var entity = await UserRepository.GetUser(id);
            if (entity != null)
            {
                var validationResult = entity.UpdateName(command);
                if (validationResult.Ok)
                {
                    var hookResult = await EventStore.AppendAll(validationResult.DomainEvents);
                    if (hookResult.Ok)
                    {
                        await UserRepository.UpdateUser(entity);
                        return new OkResult();
                    }
                    return new BadRequestObjectResult(hookResult.Errors);
                }
                return new BadRequestObjectResult(validationResult.DomainErrors);
            }
            return new NotFoundObjectResult(new List<string> { $"Could not find Root User with ID: {id}" });
        }
        
        public async Task<IActionResult> AddPostUser(Guid id, UserAddPostApiCommand apiCommand)
        {
            var entity = await UserRepository.GetUser(id);
            if (entity != null)
            {
                var errorList = new List<string>();
                var NewPost = await PostRepository.GetPost(apiCommand.NewPostId);
                if (NewPost == null) errorList.Add($"Could not find Post for {nameof(apiCommand.NewPostId)} with ID: {id}");
                var PostToDelete = await PostRepository.GetPost(apiCommand.PostToDeleteId);
                if (PostToDelete == null) errorList.Add($"Could not find Post for {nameof(apiCommand.PostToDeleteId)} with ID: {id}");
                if (errorList.Count > 0) return new NotFoundObjectResult(errorList);
                var command = new UserAddPostCommand(NewPost, PostToDelete);
                var validationResult = entity.AddPost(command);
                if (validationResult.Ok)
                {
                    var hookResult = await EventStore.AppendAll(validationResult.DomainEvents);
                    if (hookResult.Ok)
                    {
                        await UserRepository.UpdateUser(entity);
                        return new OkResult();
                    }
                    return new BadRequestObjectResult(hookResult.Errors);
                }
                return new BadRequestObjectResult(validationResult.DomainErrors);
            }
            return new NotFoundObjectResult(new List<string> { $"Could not find Root User with ID: {id}" });
        }
    }
}