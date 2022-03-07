// See https://aka.ms/new-console-template for more information

using PrisninjaDatabaseAPI;

PrisninjaDB database =new PrisninjaDB();
await database.DoEverything();



Item item2 = new Item
{
    EAN = "323",
    Name = "Kaffe",
    Weight = 2,
    Price = 3


};

//await database.AddToItemContainer(item1);