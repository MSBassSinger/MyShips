using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MyShips.Models;

namespace MyShips.Services
{
    public class MockDataStore : IDataStore<Item>
    {
        List<Item> items;

        public MockDataStore()
        {
            items = new List<Item>();
            var mockItems = new List<Item>
            {
                new Item { Id = Guid.NewGuid().ToString(),
                           Text = "NTC San Diego",
                           Description ="My first duty station - boot camp.", 
                           LongDescription = "Firefighting and damage control classes, plus marching drum and bugle corps (bass bugle).",
                           Picture = new Xamarin.Forms.Image {Source = "ussrecruit.jpg" },
                           PictureName = "ussrecruit.jpg"},
                new Item { Id = Guid.NewGuid().ToString(),
                           Text = "MM-A School Great Lakes",
                           Description ="Machinists's Mate A School",
                           LongDescription = "Great Lakes Training Center, Illinois.  My first winter up north, with lots of snow.",
                           Picture = new Xamarin.Forms.Image {Source = "mmratetrainingmanual.jpg" },
                           PictureName = "mmratetrainingmanual.jpg" },
                new Item { Id = Guid.NewGuid().ToString(),
                           Text = "USS Ticonderoga CV-14",
                           Description ="My first real ship.",
                           LongDescription = "A WWII carrier with a wooden flight deck. North Island Naval Air Station, San Diego, CA.  She was sold for scrap in 1973.",
                           Picture = new Xamarin.Forms.Image {Source = "ticonderoga.jpg" },
                           PictureName = "ticonderoga.jpg" },
                new Item { Id = Guid.NewGuid().ToString(),
                           Text = "NNPS MINSY",
                           Description ="Naval Nuclear Power School",
                           LongDescription = "Mare Island, CA Naval Shipyard.  8 hours of class per day, and even study notes were marked 'Classified'.  The school is closed down.",
                           Picture = new Xamarin.Forms.Image {Source = "nnpsminsy.jpg" },
                           PictureName = "nnpsminsy.jpg" },
                new Item { Id = Guid.NewGuid().ToString(),
                           Text = "NRTS, Idaho Falls",
                           Description ="Nuclear Reactor Training Site",
                           LongDescription = "I was assigned to the A1W prototype. NRTS is 60 miles outside Idaho Falls, Idaho, and closed down now.",
                           Picture = new Xamarin.Forms.Image {Source = "nrtsidaho.png" },
                           PictureName = "nrtsidaho.png" },
                new Item { Id = Guid.NewGuid().ToString(),
                           Text = "USS Fulton AS-11",
                           Description ="Temporary duty",
                           LongDescription = "A sub tender in Groton, CT.  I was working on nuclear sub repairs.",
                           Picture = new Xamarin.Forms.Image {Source = "fulton.jpg" },
                           PictureName = "fulton.jpg" },
                new Item { Id = Guid.NewGuid().ToString(),
                           Text = "USS Nimitz CVN-68",
                           Description ="First-of-her-kind supercarrier.",
                           LongDescription = "I am a plankowner.  The Nimitz was initially in Newport News shipyard to finish initial construction, then once commissioned, stationed in Norfolk, VA.",
                           Picture = new Xamarin.Forms.Image {Source = "ussnimitz.jpg" },
                           PictureName = "ussnimitz.jpg" }
            };

            foreach (var item in mockItems)
            {
                items.Add(item);
            }
        }

        public async Task<bool> AddItemAsync(Item item)
        {
            items.Add(item);

            return await Task.FromResult(true);
        }

        public async Task<bool> UpdateItemAsync(Item item)
        {
            var oldItem = items.Where((Item arg) => arg.Id == item.Id).FirstOrDefault();
            items.Remove(oldItem);
            items.Add(item);

            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteItemAsync(string id)
        {
            var oldItem = items.Where((Item arg) => arg.Id == id).FirstOrDefault();
            items.Remove(oldItem);

            return await Task.FromResult(true);
        }

        public async Task<Item> GetItemAsync(string id)
        {
            return await Task.FromResult(items.FirstOrDefault(s => s.Id == id));
        }

        public async Task<IEnumerable<Item>> GetItemsAsync(bool forceRefresh = false)
        {
            return await Task.FromResult(items);
        }
    }
}