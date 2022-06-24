using CocaCola.Web.Models;
using IronXL;
using System.Text;

namespace CocaCola.Web.Services
{
    public class PromotionService : IPromotionService
    {
        public async Task<List<string>> GetItemFromFile(IFormFile file)
        {
            List<string> items = new List<string>();

            using (var reader = new StreamReader(file.OpenReadStream()))
            {
                while (reader.Peek() >= 0)
                {
                    items.Add(reader.ReadLine());
                }
            }

            return items.Where(x => x != "ITEM").ToList();
        }

        public async Task<List<Store>> GetStore(string path)
        {
            var stores = new List<Store>();

            try
            {
                //get data from excel

                WorkBook book = WorkBook.Load(path);
                WorkSheet sheet = book.GetWorkSheet("Sheet1");
                var rangeA = sheet.GetColumn("A");
                var rangeB = sheet.GetColumn("B");
                for (int i = 1; i < rangeA.Rows.Count(); i++)
                {
                    Store store = new Store
                    {
                        StoreId = rangeA.Rows[i].StringValue,
                        Store_Name = rangeB.Rows[i].StringValue,
                    };
                    stores.Add(store);
                }
            }
            catch (Exception ex)
            {

            }

            return stores;
        }

        public async Task<string> Submit(PromotionDto data)
        {
            string fileName = Environment.CurrentDirectory + "\\FilePromotion\\" + "P" + DateTime.Now.Date.ToString("yyyyMMdd") + "001" + ".txt";

            int count = 1;
            while (System.IO.File.Exists(fileName))
            {
                if (count < 10)
                {
                    count++;
                    fileName = Environment.CurrentDirectory + "\\FilePromotion\\" + "P" + DateTime.Now.Date.ToString("yyyyMMdd") + "00" + count + ".txt";
                }
                else if (count >= 10 && count < 100)
                {
                    count++;
                    fileName = Environment.CurrentDirectory + "\\FilePromotion\\" + "P" + DateTime.Now.Date.ToString("yyyyMMdd") + "0" + count + ".txt";
                }
                else if (count >= 100)
                {
                    count++;
                    fileName = Environment.CurrentDirectory + "\\FilePromotion\\" + "P" + DateTime.Now.Date.ToString("yyyyMMdd") + count + ".txt";
                }

            }

            using (FileStream fs = System.IO.File.Create(fileName))
            {
                // Add some text to file    
                Byte[] title = new UTF8Encoding(true).GetBytes($"FHEAD|{data.Description}|||;\n");
                fs.Write(title, 0, title.Length);
                foreach (var item in data.Items)
                {
                    byte[] codeItem = new UTF8Encoding(true).GetBytes($"FITEM|{item}|{data.Type}|{data.ValueType}|;\n");
                    fs.Write(codeItem, 0, codeItem.Length);
                }
                foreach (var store in data.Store)
                {
                    byte[] codeItem = new UTF8Encoding(true).GetBytes($"FSTORE|{store}|{data.StartDate.Date.ToString("yyyyMMdd")}|{data.EndDate.Date.ToString("yyyyMMdd")}|;\n");
                    fs.Write(codeItem, 0, codeItem.Length);
                }
                byte[] author = new UTF8Encoding(true).GetBytes("FTAIL||||");
                fs.Write(author, 0, author.Length);
            }

            return fileName;
        }
    }
}
