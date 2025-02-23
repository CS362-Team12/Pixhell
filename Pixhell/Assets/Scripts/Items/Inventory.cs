using UnityEngine;
using System.Collections.Generic;
using System;
using System.IO;

// File used to dynamically store player items as they play

namespace InventoryClass {
    public class Inventory {
        public List<Item> items = new List<Item>();
        public float totalDamageMod = 0;
        public float totalAttackSpeedMod = 0;
        public float totalHealthMod = 0;
        public float totalMovementSpeedMod = 0;

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
                totalDamageMod += item.damage / 100;
                totalAttackSpeedMod += item.attackSpeed / 100;
                totalHealthMod += item.health / 100;
                totalMovementSpeedMod += item.movementSpeed / 100;
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
                    float damage = float.Parse(columns[4]);
                    float attackSpeed = float.Parse(columns[5]);
                    float health = float.Parse(columns[6]);
                    float movementSpeed = float.Parse(columns[7]);
                    return new Item(id, name, description, imagePath, damage, attackSpeed, health, movementSpeed);
                }
            }
            Debug.Log($"Item with ID {id} not found.");
            return new Item();
        }
    }
}
