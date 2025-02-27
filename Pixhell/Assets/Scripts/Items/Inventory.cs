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

        public string itemInfoPath = Path.Combine(Application.streamingAssetsPath, "Items/GlobalItems.csv");

        public void addItem(int id) {
            items.Add(readItem(id));
            calculateModifiers();
        }

        public void addItem(Item item) {
            items.Add(item);
            calculateModifiers();
        }

        public void removeItem(Item item) {
            items.RemoveAll(i => i.id == item.id);
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

        public bool hasItem(Item item) {
            for (int i = 0; i < items.Count; i++) {
                if (items[i].id == item.id) {
                    return true;
                }
            }
            return false;
        }

        public Item readItem(int id) {
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
                    int cost = int.Parse(columns[8]);
                    return new Item(id, name, description, imagePath, damage, attackSpeed, health, movementSpeed, cost);
                }
            }
            Debug.Log($"Item with ID {id} not found.");
            return new Item();
        }

        public Item readItem(string[] columns) 
        {
            // Removes the need for looking at all lines in the file
            int id = int.Parse(columns[0]);
            string name = columns[1];
            string description = columns[2];
            string imagePath = columns[3];
            float damage = float.Parse(columns[4]);
            float attackSpeed = float.Parse(columns[5]);
            float health = float.Parse(columns[6]);
            float movementSpeed = float.Parse(columns[7]);
            int cost = int.Parse(columns[8]);
            return new Item(id, name, description, imagePath, damage, attackSpeed, health, movementSpeed, cost);
        }

        public void resetInventory() {
            items = new List<Item>();
            calculateModifiers();
        }
    }
}
