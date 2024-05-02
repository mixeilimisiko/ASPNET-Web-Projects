using Mapster;
using System.Drawing;
using ToDo.App.Subtasks.Requests;
using ToDo.App.Subtasks.Responses;
using ToDo.App.Todos.Requests;
using ToDo.App.Todos.Responses;
using ToDo.App.Users.Requests;
using ToDo.App.Users.Responses;
using ToDo.Domain;
using ToDo.Domain.Enums;


namespace ToDo.App.Mappings
{
    public static class CoreMapsterConfig
    {
        
        public static void ConfigureCoreMappings()
        {

            TypeAdapterConfig<UserRegisterRequestModel, User>
               .NewConfig()
               .Map(dest => dest.Username, src => src.Username)
               .Map(dest => dest.PasswordHash, src => PasswordHasher.HashPassword(src.Password))
               .Map(dest => dest.CreatedAt, _ => DateTime.Now)
               .Map(dest => dest.ModifiedAt, _ => DateTime.Now)
               .Map(dest => dest.Status, _ => Statuses.Active);

            TypeAdapterConfig<UserLoginRequestModel, User>
               .NewConfig()
               .Map(dest => dest.Username, src => src.Username)
               .Map(dest => dest.PasswordHash, src => PasswordHasher.HashPassword(src.Password));


            TypeAdapterConfig<User, UserResponseModel>
               .NewConfig()
               .Map(dest => dest.Username, src => src.Username)
               .Map(dest => dest.Id, src => src.Id);

            TypeAdapterConfig<TodoCreateRequestModel, Todo>
                .NewConfig()
                .Map(dest => dest.Title, src => src.Title)
                .Map(dest => dest.TargetDate, src => src.TargetDate)
                .Map(dest => dest.CreatedAt, _ => DateTime.Now)
                .Map(dest => dest.ModifiedAt, _ => DateTime.Now)
                .Map(dest => dest.Status, _ => Statuses.Active);

            TypeAdapterConfig<Subtask, SubtaskResponseModel>
                .NewConfig()
                .Map(dest => dest.Id, src => src.Id)
                .Map(dest => dest.Title, src => src.Title)
                .Map(dest => dest.Status, src => src.Status); 

 
            TypeAdapterConfig<Todo, TodoResponseModel>
                .NewConfig()
                .Map(dest => dest.Id, src => src.Id)
                .Map(dest => dest.Title, src => src.Title)
                .Map(dest => dest.TargetDate, src => src.TargetDate)
                .Map(dest => dest.UserId, src => src.UserId)
                .Map(dest => dest.Subtasks, src => src.Subtasks != null ? src.Subtasks.Adapt<List<SubtaskResponseModel>>() : new List<SubtaskResponseModel>())
                .Map(dest => dest.Status, src => src.Status);

            TypeAdapterConfig<SubtaskRequestModel, Subtask>
                .NewConfig()
                .Map(dest => dest.Title, src => src.Title)
                .Map(dest => dest.ToDoId, src => src.TodoId)
                .Map(dest => dest.CreatedAt, _ => DateTime.Now)
                .Map(dest => dest.ModifiedAt, _ => DateTime.Now)
                .Map(dest => dest.Status, _ => Statuses.Active);
        }


    }

}