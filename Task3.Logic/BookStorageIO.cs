using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task3.Logic
{
    public static class BookStorageIO
    {
        public static void StorageReader(IBookStorage storage, string filePath)
        {
            using (BinaryReader reader = new BinaryReader(File.Open(filePath, FileMode.Open)))
            {
                while (reader.BaseStream.Position != reader.BaseStream.Length)
                {
                    Book b = new Book(reader.ReadInt64());
                    b.Author = reader.ReadString();
                    b.Edition = reader.ReadInt32();
                    //b.Genre = reader.ReadString();
                    b.Name = reader.ReadString();
                    b.PageCount = reader.ReadInt32();
                    b.Publisher = reader.ReadString();
                    b.Year = reader.ReadInt32();
                    storage.Add(b);
                }
            }
        }

        public static void StorageWriter(IBookStorage storage, string filePath)
        {
            using (BinaryWriter writer = new BinaryWriter(File.Open(filePath, FileMode.OpenOrCreate)))
            {
                foreach (Book item in storage)
                {
                    writer.Write(item.ISBN);
                    writer.Write(item.Author);
                    writer.Write(item.Edition);
                    //writer.Write(item.Genre);
                    writer.Write(item.Name);
                    writer.Write(item.PageCount);
                    writer.Write(item.Publisher);
                    writer.Write(item.Year);
                }
            }
        }
    }
}
