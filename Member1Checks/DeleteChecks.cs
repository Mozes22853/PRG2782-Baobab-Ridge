using System.Text;
using BaobabRidge.BusinessLogicLayer;
using BaobabRidge.DataLayer;
using BaobabRidge.PresentationLayer;

internal static partial class Program
{
    // Tests safe deletion, byte preservation, ambiguous IDs and file errors.
    private static void CheckDeleteFiles()
    {
        string fixture=Path.Combine(AppContext.BaseDirectory,"delete-review-"+Guid.NewGuid()+".txt");
        var records=new[]{CreateDeleteAnimal("WR-0201",19,"Nalédi"),CreateDeleteAnimal("WR-0202",79,"Thabo"),CreateDeleteAnimal("WR-0203",80,"Tau")};
        var lines=records.Select(FileHandler.ToFileLine).ToArray();
        FileHandler handler=new FileHandler(fixture);
        try
        {
            foreach(string ending in new[]{"\r\n","\n","\r"})
            foreach(bool lastEnding in new[]{true,false})
            foreach(int target in new[]{0,1,2})
            {
                string text=string.Join(ending,lines)+(lastEnding?ending:"");
                File.WriteAllText(fixture,text,new UTF8Encoding(false));
                string removed=lines[target]+((target<2||lastEnding)?ending:"");
                string expected=text.Remove(text.IndexOf(removed,StringComparison.Ordinal),removed.Length);
                Check(handler.TryDeleteAnimal(records[target],out _)&&File.ReadAllBytes(fixture).SequenceEqual(Encoding.UTF8.GetBytes(expected)),"Delete target "+target+" preserves all other bytes; ending="+ending.Replace("\r","CR").Replace("\n","LF")+" final="+lastEnding);
            }
            File.WriteAllText(fixture,string.Join("\r\n",lines),new UTF8Encoding(true));
            byte[] original=File.ReadAllBytes(fixture);
            byte[] expectedBom=Encoding.UTF8.GetPreamble().Concat(Encoding.UTF8.GetBytes(lines[0]+"\r\n"+lines[2])).ToArray();
            Check(handler.TryDeleteAnimal(records[1],out _)&&File.ReadAllBytes(fixture).SequenceEqual(expectedBom),"Middle delete preserves UTF-8 BOM and Unicode bytes");
            File.WriteAllText(fixture,lines[0]);
            Check(handler.TryDeleteAnimal(records[0],out _)&&new FileInfo(fixture).Length==0,"Delete last remaining record creates empty file");
            Check(handler.TryLoadAnimals(out var empty,out _,out _)&&empty.Count==0,"Empty file loads after last deletion");
            string malformed="WR-0201|Bad|Serval|oops|19|Critical|Intensive Care Unit";
            File.WriteAllLines(fixture,new[]{malformed,lines[0],lines[1]});
            Check(handler.TryDeleteAnimal(records[0],out _)&&File.ReadAllLines(fixture).SequenceEqual(new[]{malformed,lines[1]}),"Delete ignores malformed same-ID line and removes only valid record");
            File.WriteAllLines(fixture,new[]{lines[0],lines[0],lines[1]}); original=File.ReadAllBytes(fixture);
            Check(!handler.TryDeleteAnimal(records[0],out string message)&&message.Contains("More than one")&&File.ReadAllBytes(fixture).SequenceEqual(original),"Ambiguous valid duplicate IDs rejected without write");
            File.WriteAllLines(fixture,lines); original=File.ReadAllBytes(fixture);
            Check(!handler.TryDeleteAnimal(CreateDeleteAnimal("WR-0201",20,"Nalédi"),out message)&&message.Contains("changed")&&File.ReadAllBytes(fixture).SequenceEqual(original),"Stale selection rejected without write");
            Check(!handler.TryDeleteAnimal(CreateDeleteAnimal("WR-9999",19,"None"),out message)&&message.Contains("not found")&&File.ReadAllBytes(fixture).SequenceEqual(original),"Missing target rejected without write");
            File.SetAttributes(fixture,FileAttributes.ReadOnly);
            try { Check(!handler.TryDeleteAnimal(records[0],out message)&&message.Contains("read-only")&&File.ReadAllBytes(fixture).SequenceEqual(original),"Read-only Delete fails safely"); }
            finally { File.SetAttributes(fixture,FileAttributes.Normal); }
            using(FileStream locked=new FileStream(fixture,FileMode.Open,FileAccess.ReadWrite,FileShare.None))
                Check(!handler.TryDeleteAnimal(records[0],out message)&&message.Contains("locking"),"Locked Delete gives friendly error");
            Check(File.ReadAllBytes(fixture).SequenceEqual(original),"Locked Delete preserves original bytes");
            Check(!new FileHandler(fixture+"missing").TryDeleteAnimal(records[0],out message)&&message.Contains("not found"),"Missing file gives friendly Delete error");
            Check(!new FileHandler(fixture+"/missing/animals.txt").TryDeleteAnimal(records[0],out message)&&message.Contains("folder"),"Missing folder gives friendly Delete error");
            Check(Directory.GetFiles(AppContext.BaseDirectory,Path.GetFileName(fixture)+".*.tmp").Length==0,"Delete cleans temporary files after failures");
        }
        finally { if(File.Exists(fixture)){File.SetAttributes(fixture,FileAttributes.Normal); File.Delete(fixture);} }
    }

    // Builds a valid classified animal for deletion fixtures.
    private static Animal CreateDeleteAnimal(string id, int score, string name)
    {
        Animal animal = new Animal(id, name, "Serval", 3, score);
        AnimalClassifier.Classify(animal);
        return animal;
    }
}
