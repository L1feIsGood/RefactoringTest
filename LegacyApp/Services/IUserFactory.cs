namespace LegacyApp
{
    public interface IUserFactory
    {
        User Create(CreateUserParams createUserParams);
    }
}