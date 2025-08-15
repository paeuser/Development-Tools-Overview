using System;
using System.Collections.Generic;

namespace CheeseMongers
{
    public class Program
    {
        private IList<CheeseMongersItem> Items;
        public Program(IList<CheeseMongersItem> items)
        {
            Items = items;
        }


        public static void Main()
        {
            var items = new List<CheeseMongersItem>
        {
            new CheeseMongersItem { Name = "Parmigiano Regiano", ValidByDays = 5, Quality = 40 },
            new CheeseMongersItem { Name = "Ricotta", ValidByDays = 3, Quality = 20 },
            new CheeseMongersItem { Name = "Tasting with Chef Massimo", ValidByDays = 10, Quality = 25 },
            new CheeseMongersItem { Name = "Caciocavallo Podolico", ValidByDays = 10, Quality = 80 },
            new CheeseMongersItem { Name = "Standard Cheese", ValidByDays = 15, Quality = 30 },
        };

            var program = new Program(items);

            Console.WriteLine("Before update:");
            foreach (var item in items)
                Console.WriteLine($"{item.Name} - ValidByDays: {item.ValidByDays}, Quality: {item.Quality}");

            program.UpdateQuality();

            Console.WriteLine("\nAfter update:");
            foreach (var item in items)
                Console.WriteLine($"{item.Name} - ValidByDays: {item.ValidByDays}, Quality: {item.Quality}");
        }

        
        
        public void UpdateQuality()
        {
            for (var i = 0; i < Items.Count; i++)
            {

                if (Items[i].Name == "Ricotta")
                {
                    // Ricotta quality degrades 3 times faster before expiry, 5 times after expiry
                    if (Items[i].Quality > 0)
                    {
                        int degradeAmount = Items[i].ValidByDays < 0 ? 5 : 3;
                        Items[i].Quality -= degradeAmount;
                        if (Items[i].Quality < 0)
                            Items[i].Quality = 0;
                    }
                    Items[i].ValidByDays--;
                    continue; // Prevent other rules from applying
                }

                if (Items[i].Name != "Parmigiano Regiano" && Items[i].Name != "Tasting with Chef Massimo")
                {
                    if (Items[i].Quality > 0)
                    {
                        if (Items[i].Name != "Caciocavallo Podolico")
                        {
                            Items[i].Quality = Items[i].Quality - 1;
                        }
                    }
                }
                else
                {
                    if (Items[i].Quality < 100)
                    {
                        Items[i].Quality = Items[i].Quality + 1;

                        if (Items[i].Name == "Tasting with Chef Massimo")
                        {
                            if (Items[i].ValidByDays < 15)
                            {
                                if (Items[i].Quality < 100)
                                {
                                    if (Items[i].Quality + 2 <= 100)
                                    {
                                        Items[i].Quality = Items[i].Quality + 2;
                                    }
                                    else
                                    {
                                        Items[i].Quality = 100;
                                    }
                                }
                            }

                            if (Items[i].ValidByDays < 8)
                            {
                                if (Items[i].Quality < 100)
                                {
                                    if (Items[i].Quality + 2 <= 100)
                                    {
                                        Items[i].Quality = Items[i].Quality + 2;
                                    }
                                    else
                                    {
                                        Items[i].Quality = 100;
                                    }
                                }
                            }
                        }
                    }
                }

                if (Items[i].Name != "Caciocavallo Podolico")
                {
                    Items[i].ValidByDays = Items[i].ValidByDays - 1;
                }

                if (Items[i].ValidByDays < 0)
                {
                    if (Items[i].Name != "Parmigiano Regiano")
                    {
                        if (Items[i].Name != "Tasting with Chef Massimo")
                        {
                            if (Items[i].Quality > 0)
                            {
                                if (Items[i].Name != "Caciocavallo Podolico")
                                {
                                    if (Items[i].Quality - 4 > 0)
                                    {
                                        Items[i].Quality = Items[i].Quality - 4;
                                    }
                                    else
                                    {
                                        Items[i].Quality = 0;
                                    }
                                }
                            }
                        }
                        else
                        {
                            Items[i].Quality = Items[i].Quality - Items[i].Quality;
                        }
                    }
                }
            }
        }

    }

    public class CheeseMongersItem
    {
        public string Name { get; set; }

        public int ValidByDays { get; set; }

        public int Quality { get; set; }
    }
}
