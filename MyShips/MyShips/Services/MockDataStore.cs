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
    /// <summary>
    /// Used to store items for the demo.
    /// </summary>
    public class MockDataStore : IDataStore<Item>
    {
        /// <summary>
        /// Holds the items to be viewed, edited, saved, or deleted.
        /// </summary>
        List<Item> m_Items;

        /// <summary>
        /// Constructor.
        /// Creates the default data.
        /// </summary>
        public MockDataStore()
        {
            m_Items = new List<Item>();
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
                m_Items.Add(item);
            }

            // Check to see if any items were saved.

            String fileFolder = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);

            String[] filesFound = null;

            try
            {
                filesFound = Directory.GetFiles(fileFolder, "*.item", SearchOption.TopDirectoryOnly);
            }
            catch
            {
                filesFound = null;
            }

            if (filesFound != null)
            {
                if (filesFound.Length > 0)
                {
                    // User-added items are encrypted.
                    String encryptionKey = ContextMgr.Instance.ContextValues[MyShips.Properties.Resources.EncryptionKey].ToString();

                    String encryptionSeed = ContextMgr.Instance.ContextValues[MyShips.Properties.Resources.EncryptionSeed].ToString();

                    if ((encryptionKey.Length >= 32) && (encryptionSeed.Length >= 8))
                    {
                        foreach (String fileName in filesFound)
                        {
                            String fileContents = File.ReadAllText(fileName);

                            CryptoMgr encryptMgr = new CryptoMgr(encryptionKey, encryptionSeed);

                            String decryptedString = encryptMgr.DecryptStringAES(fileContents);

                            Item fileItem = JsonSerializer.Deserialize<Item>(decryptedString);

                            if (String.IsNullOrWhiteSpace(fileItem.Id))
                            {
                                fileItem.Id = Guid.NewGuid().ToString();
                            }

                            m_Items.Add(fileItem);
                        }
                    }
				}
			}

        }

        /// <summary>
        /// Used to add an item.
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public async Task<bool> AddItemAsync(Item item)
        {
            m_Items.Add(item);

            return await Task.FromResult(true);
        }


        /// <summary>
        /// Updates an existing item by remiving the old one, and adding the new one.
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public async Task<bool> UpdateItemAsync(Item item)
        {
            var oldItem = m_Items.Where((Item arg) => arg.Id == item.Id).FirstOrDefault();
            m_Items.Remove(oldItem);
            m_Items.Add(item);

            return await Task.FromResult(true);
        }

        /// <summary>
        /// Removes a specified item if it exists.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<bool> DeleteItemAsync(String id)
        {
            var oldItem = m_Items.Where((Item arg) => arg.Id == id).FirstOrDefault();
            m_Items.Remove(oldItem);

            return await Task.FromResult(true);
        }

        /// <summary>
        /// Gets an item by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<Item> GetItemAsync(String id)
        {
            return await Task.FromResult(m_Items.FirstOrDefault(s => s.Id == id));
        }

        /// <summary>
        /// Gets all the items.
        /// </summary>
        /// <param name="forceRefresh"></param>
        /// <returns></returns>
        public async Task<IEnumerable<Item>> GetItemsAsync(Boolean forceRefresh = false)
        {
            return await Task.FromResult(m_Items);
        }
    }
}