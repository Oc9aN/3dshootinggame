
public class Weapon
{
    // TODO: 총 종류별로 팩토리로 생성할 수 있게 변경
    // 총의 데이터가 달라짐
    private SO_Weapon _data;
    public SO_Weapon Data => _data;

    public Weapon(SO_Weapon data)
    {
        _data = data;
    }
}