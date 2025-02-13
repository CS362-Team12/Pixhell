using System.Collections.Generic;

// File used to dynamically store player items as they play

namespace InventoryClass {
    public class Inventory {
    List<Item> items = new List<Item>();

    public void addItem(Item item) {
        items.Add(item);
    }

    public void removeItem(Item item) {
        items.Remove(item);
    }

    public List<Item> getList() {
        return items;
    }
    

}
}
