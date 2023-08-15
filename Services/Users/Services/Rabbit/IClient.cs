namespace Users.Services.Rabbit
{
    public interface IClient
    {
        public void CreatePortfolio(int id);
        public void DeletePortfolio(int id);
    }
}