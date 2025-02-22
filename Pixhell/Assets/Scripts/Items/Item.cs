
// File used for the item registry, stores all buffs/nerfs from items

public class Item {
    public int id;
    public string name;
    public string description;
    public string imagePath;
    public int damage;
    public int attackSpeed;
    public int health;
    public int movementSpeed;

    public Item() {
        this.id = -1;
    }

    public Item(int id, string name, string desc, string imagePath, int d, int aS, int h, int mS) {
        this.id = id;
        this.name = name;
        this.description = desc;
        this.imagePath = imagePath;
        this.damage = d;
        this.attackSpeed = aS;
        this.health = h;
        this.movementSpeed = mS;
    }

}