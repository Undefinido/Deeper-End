using UnityEngine;
[CreateAssetMenu(fileName = "New Scene Manager", menuName = "Only one can exist/Scene Manager")]
public class SceneManager : ScriptableObject
{
    [SerializeReference] int Distance;//distance between safe zones
    public int unlocked = 0;
    public bool AliveBoss { get { return floorNumber / Distance > unlocked; } }
    int floorNumber = 0;
    int FloorNumber {
        get {
            if (floorNumber % Distance == 0 && floorNumber / Distance > unlocked) unlocked++;
            return ++floorNumber % Distance == 0 ? 0 : floorNumber / Distance + 1;
        }
    }
    [SerializeReference] string CharacterSelect;
    [SerializeField] FloorLevels[] AllFloors;
    public void NextFloor() {
        UnityEngine.SceneManagement.SceneManager.LoadScene(SceneName(ref AllFloors[FloorNumber].Floors));
    }
    string SceneName(ref string[] array) { return array[Random.Range(0, array.Length)]; }
    public void Return() {
        UnityEngine.SceneManagement.SceneManager.LoadScene(CharacterSelect);
    }
    public void StartingFloor(int choice) { //assume it's a variable divided by Distance
        floorNumber = choice * Distance;
        NextFloor();
    }
}
[System.Serializable]
class FloorLevels
{
    public string[] Floors;
}
