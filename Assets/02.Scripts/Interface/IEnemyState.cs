public interface IEnemyState
{
    public void Enter();
    // State에 따른 동작
    public void Acting();
    public void Exit();
}