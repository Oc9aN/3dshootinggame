
public class Weapon
{
    // 총의 데이터가 달라짐
    private SO_Weapon _data;
    public SO_Weapon Data => _data;

    public Weapon(SO_Weapon data)
    {
        _data = data;
    }
}