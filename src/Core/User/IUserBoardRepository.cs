namespace Core.User
{
    public interface IUserBoardRepository
    {
        public Task<List<UserBoard>> GetAll(string userId);       
    }
}