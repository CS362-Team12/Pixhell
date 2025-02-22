using System.Collections.Generic;

// File used to dynamically store player items as they play

namespace InventoryClass {
    public class Inventory {
        public List<Item> items;
        public int totalDamageMod = 0;
        public int totalAttackSpeedMod = 0;
        public int totalHealthMod = 0;
        public int totalMovementSpeedMod = 0;

        public void addItem(Item item) {
            items.Add(item);
        }

        public void removeItem(Item item) {
            items.Remove(item);
        }

        public List<Item> getList() {
            return items;
        }

        public void calculateModifiers() {
            totalDamageMod = 0; 
            totalAttackSpeedMod = 0; 
            totalHealthMod = 0;
            totalMovementSpeedMod = 0;
            foreach (Item item in items) {
                totalDamageMod += item.damage;
                totalAttackSpeedMod += item.attackSpeed;
                totalHealthMod += item.health;
                totalMovementSpeedMod += item.movementSpeed;
            }
        }
    
    }
}
