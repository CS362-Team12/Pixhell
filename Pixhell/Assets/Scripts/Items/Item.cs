
// File used for the item registry, stores all buffs/nerfs from items

public class Item {
    public int id;
    public string name;
    public string description;
    public string imagePath;
    public float damage;
    public float attackSpeed;
    public float health;
    public float movementSpeed;

    public Item() {
        this.id = -1;
    }

    public Item(int id, string name, string desc, string imagePath, float d, float aS, float h, float mS) {
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