using Arithmetica;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Testing.Models;

namespace Testing.Services
{
    public class MockDataStore : IDataStore<Item>
    {
        readonly List<Item> items;
        SQLiteAsyncConnection Database;
        public MockDataStore()
        {
            items = new List<Item>()
            {
                
            };
        }

        async Task Init()
        {
            if (Database != null)
                return;

            Database = new SQLiteAsyncConnection(Constants.DatabasePath, Constants.Flags);
            var result = await Database.CreateTableAsync<Item>();
        }

        public async Task<bool> AddItemAsync(Item item)
        {
            //item.Id = (items.Count + 1).ToString();
            //items.Add(item);

            //return await Task.FromResult(true);
            try
            {
                await Init();
                if (item.Id != null)
                    await Database.UpdateAsync(item);
                else
                    await Database.InsertAsync(item);

                return true;
            }
            catch
            {
                return false;
            }
            
        }

        public async Task<bool> UpdateItemAsync(Item item)
        {
            //var oldItem = items.Where((Item arg) => arg.Id == item.Id).FirstOrDefault();
            //items.Remove(oldItem);
            //items.Add(item);

            //return await Task.FromResult(true);

            try
            {
                await Init();
                if (item.Id != null)
                    await Database.UpdateAsync(item);
                else 
                {
                    List<Item> items =  await Database.Table<Item>().ToListAsync();
                    Item lastItem = items.LastOrDefault();
                    if(lastItem != null)
                    {
                        item.Id = (int.Parse(lastItem.Id) + 1).ToString();
                    }
                    else
                    {
                        item.Id = "0";
                    }
                    await Database.InsertAsync(item);
                }
                    

                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> DeleteItemAsync(string id)
        {
            //var oldItem = items.Where((Item arg) => arg.Id == id).FirstOrDefault();
            //items.Remove(oldItem);

            //return await Task.FromResult(true);
            try
            {
                await Init();
                Item item = await Database.Table<Item>().Where(i => i.Id == id).FirstOrDefaultAsync();
                await Database.DeleteAsync(item);

                return true;
            }
            catch
            {
                return false;
            }
            
        }

        public async Task<Item> GetItemAsync(string id)
        {
            //return await Task.FromResult(items.FirstOrDefault(s => s.Id == id));

            await Init();
            return await Database.Table<Item>().Where(i => i.Id == id).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Item>> GetItemsAsync(bool forceRefresh = false)
        {
            //return await Task.FromResult(items);
            await Init();
            return await Database.Table<Item>().ToListAsync();

        }

        public async Task<bool> ClearItemsAsync()
        {
            await Init();
            List<Item> Items =  await Database.Table<Item>().ToListAsync();
            foreach (Item item in Items) 
            {
                await Database.DeleteAsync(item);
            }
            return true;

        }
    }
}