
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Code {



   class Directory{
      public Dictionary<String,Directory> directories = new Dictionary<String,Directory>();
      public String key;
      public int size;
      public Directory(String key){
         this.key=key;
      }
      public int getSize(){
         int count =0;
         foreach(Directory dir in directories.Values){
            count+=dir.getSize();
         }
         return size+count;
      }

   }
   

   class AdventOfCode
   {
      public static List<String> lines = new List<String>(File.ReadAllLines("input.txt"));
      public static int part1ans;
      public static int neededSpace;
      public static List<Directory> allDirs = new List<Directory>();

      public static void createTree(Directory dir){
         while(lines.Any()){
            String[] line = lines[0].Split(' ');
            if(int.TryParse(line[0], out int value)){
               dir.size += int.Parse(line[0]);
            }
            else if(line[0].Equals("dir")){
               dir.directories.Add(line[1],new Directory(line[1]));
            }
            else if(line[0].Equals("$") &&  line.Length > 2 && line[1].Equals("cd") ){
               if(line[2].Equals("..")){
                  return;
               }else if(dir.directories.ContainsKey(line[2])){
                  createTree(dir.directories[line[2]]);
               }  
            }
            if(lines.Count==0){
               return;
            }else{
               lines.Remove(lines[0]);
            }
         }
        return;
      }

      public static int bfs(Directory node){
         Queue<Directory> q = new Queue<Directory>();
         q.Enqueue(node);
         while(q.Count>0){
            node = q.Dequeue();

            allDirs.Add(node);
            
            if(node.getSize() <=100000){
               part1ans+=node.getSize();
            }
            foreach(Directory dir in node.directories.Values){
               q.Enqueue(dir);
            }
         }
         return part1ans;
      }

      public static int part2(int neededSpace){
         return allDirs.Where(s=>s.getSize()>=neededSpace).MinBy(dir => Math.Abs((long) dir.getSize() - neededSpace)).getSize();
      }

      static void Main(string[] args) 
      {
         Directory root = new Directory("/");
         createTree(root);
         neededSpace = root.getSize() - 40000000;
         Console.WriteLine("Total Size: "+ root.getSize());
         Console.WriteLine("Task 1: " +bfs(root));
         Console.WriteLine("Task 2: " + part2(neededSpace));
   }
}
}