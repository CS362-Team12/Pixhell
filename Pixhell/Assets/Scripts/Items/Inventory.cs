using UnityEngine;
using System.Collections.Generic;
using System;
using System.IO;

// File used to dynamically store player items as they play

namespace InventoryClass {
    public class Inventory {
        public List<Item> items = new List<Item>();
        public int totalDamageMod = 0;
        public int totalAttackSpeedMod = 0;
        public int totalHealthMod = 0;
        public int totalMovementSpeedMod = 0;

        public void addItem(int id) {
            items.Add(readItem(id));
            calculateModifiers();
        }

        public void addItem(Item item) {
            items.Add(item);
            calculateModifiers();
        }

        public void removeItem(Item item) {
            items.Remove(item);
            calculateModifiers();
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

        public Item readItem(int id) {
            string itemInfoPath = Path.Combine(Application.streamingAssetsPath, "Items/GlobalItems.csv");
            string[] lines = File.ReadAllLines(itemInfoPath);
            foreach (var line in lines)
            {
                string[] columns = line.Split(',');
                if (columns[0] == id.ToString())
                {
                    string name = columns[1];
                    string description = columns[2];
                    string imagePath = columns[3];
                    int damage = int.Parse(columns[4]);
                    int attackSpeed = int.Parse(columns[5]);
                    int health = int.Parse(columns[6]);
                    int movementSpeed = int.Parse(columns[7]);
                    return new Item(id, name, description, imagePath, damage, attackSpeed, health, movementSpeed);
                }
            }
            Debug.Log("Item with ID {id} not found.");
            return new Item();
        }
    }
}
