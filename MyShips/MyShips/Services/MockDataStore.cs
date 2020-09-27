using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Essentials;
using MyShips.Models;
using System.Text.Json;
using System.IO;
using Xamarin.Forms;

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
                           LongDescription = "I was 17 years old, and loved the low humidity. Firefighting and damage control classes are what stood out for me, plus playing the bass bugle in the marching drum and bugle corps.  An easy adaptation since I played tuba in the high school band.",
                           Picture = new Xamarin.Forms.Image {Source = "ussrecruit.jpg" },
                           PictureName = "ussrecruit.jpg"},
                new Item { Id = Guid.NewGuid().ToString(),
                           Text = "MM-A School Great Lakes",
                           Description ="Machinists's Mate A School",
                           LongDescription = "Great Lakes Training Center, Illinois. My first winter up north, with lots of snow. I was a musician in a school to work on some of the largest steam engines and related equipment in the world.",
                           Picture = new Xamarin.Forms.Image {Source = "mmratetrainingmanual.jpg" },
                           PictureName = "mmratetrainingmanual.jpg" },
                new Item { Id = Guid.NewGuid().ToString(),
                           Text = "USS Ticonderoga CV-14",
                           Description ="My first real ship, an aircraft carrier.",
                           LongDescription = "She was an Essex class carrier, 41,700 ton displacement fully loaded, 3,400 crew, and 80-100 planes.  In 1972, she recovered Apollo 17.  When I first saw her at the dock at North Island Naval Air Station in San Diego, I though she was huge (until I saw the USS Nimitz in 1974).  A WWII carrier still with the original wooden flight deck (angle deck was metal). She was decommissioned and sold for scrap in 1973 not long after I left for Nuclear Power School.  I married my wife while stationed on the Ticonderoga.",
                           Picture = new Xamarin.Forms.Image {Source = "ticonderoga.jpg" },
                           PictureName = "ticonderoga.jpg" },
                new Item { Id = Guid.NewGuid().ToString(),
                           Text = "NNPS MINSY",
                           Description ="Naval Nuclear Power School",
                           LongDescription = "The Navy's most academically difficult engineering school.  This one was located at the Mare Island, CA Naval Shipyard. Two others existed in New England and Orlando, FL.  The average school day consited of 8 hours/day, 5 days/week of classes that included advanced engineering, nuclear physics, metallurgy, math, calculus, etc.  Even study notes were marked 'Classified'. The school is closed down, and the building scrapped.",
                           Picture = new Xamarin.Forms.Image {Source = "nnpsminsy.jpg" },
                           PictureName = "nnpsminsy.jpg" },
                new Item { Id = Guid.NewGuid().ToString(),
                           Text = "NRTS, Idaho Falls",
                           Description ="Nuclear Reactor Training Site",
                           LongDescription = "I was assigned to the A1W prototype. NRTS is 60 miles outside Idaho Falls, Idaho.  The site is closed down now.  For me, it was an absolutely amazing place.  It was in Idaho Falls that my first child was born.",
                           Picture = new Xamarin.Forms.Image {Source = "nrtsidaho.png" },
                           PictureName = "nrtsidaho.png" },
                new Item { Id = Guid.NewGuid().ToString(),
                           Text = "USS Fulton AS-11",
                           Description ="Temporary duty",
                           LongDescription = "A sub tender in Groton, CT. I was working on nuclear sub repairs.  This was temporary duty until my billet opened on the USS Nimitz in Newport News, VA",
                           Picture = new Xamarin.Forms.Image {Source = "fulton.jpg" },
                           PictureName = "fulton.jpg" },
                new Item { Id = Guid.NewGuid().ToString(),
                           Text = "USS Nimitz CVN-68",
                           Description ="First-of-her-kind supercarrier.",
                           LongDescription = "She has 110,000 ton displacement fully loaded, 6,000 crew, and 90 aircraft.  I am a plankowner. The Nimitz was initially built in the Newport News shipyard, then once commissioned, was stationed in Norfolk, VA.  She has other homeports since then.",
                           Picture = new Xamarin.Forms.Image {Source = "ussnimitz.jpg" },
                           PictureName = "ussnimitz.jpg" }
            };

            foreach (var item in mockItems)
            {
                items.Add(item);
            }

            // Check to see if any items were saved.
            String fileFolder = FileSystem.AppDataDirectory;

            String[] filesFound = Directory.GetFiles(fileFolder, "*.item");

            if (filesFound != null)
            {
                if (filesFound.Length > 0)
                {
                    foreach (String fileName in filesFound)
                    {
                        String fileContents = File.ReadAllText(fileName);

                        Item fileItem = JsonSerializer.Deserialize<Item>(fileContents);

                        if (String.IsNullOrWhiteSpace(fileItem.Id))
                        {
                            fileItem.Id = Guid.NewGuid().ToString();
                        }

                        items.Add(fileItem);
                    }
				}
			}

            //ContextMgr.Instance.ContextValues.Add("MockDataStoreItems", items);

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