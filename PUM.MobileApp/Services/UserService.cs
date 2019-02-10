namespace PUM.MobileApp.Services
{
    using PUM.SharedModels;
    public interface IUserService
    {
        User CurrentUser { get; set; }
    }

    public class UserService : IUserService
    {
        public User CurrentUser { get; set; }
    }
}
