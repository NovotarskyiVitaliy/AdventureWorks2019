namespace AdventureWorks2019.Application;

public interface IJwtProvider
{
    string GenerateToken(UserInfo userInfo);
}