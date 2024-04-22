using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace PL_Assembler
{
  public class Assembler
  {
    private List<Instruction> instructions;
    private List<string> instructionList;

    /*
*.split()
.indexOf()
.toString
[0]
Int.Parse
*/

    public Assembler()
    {
      instructions = new List<Instruction>();
      instructionList = new List<string>();
    }
    
    private string ParseInstructions(string line)
    {
      //parse lines -> mabe separate by space|| " " 
      return line.Replace("\n", "").Replace("\r", "").Trim();
    }


    public void LoadAssemblyFile(string assemblyPath)
    {
      string[] lines = File.ReadAllLines(assemblyPath);
      foreach (string line in lines)
      {
        if(!string.IsNullOrWhiteSpace(line))
        {
          string instruction = ParseInstructions(line);
          if (instruction != null)
          {
            Console.WriteLine(instruction);
            instructionList.Add(instruction);
          }
        }

      }
      ProcessThing();
    }

    public void ProcessThing()
    {
      foreach (string instruction in instructionList)
      { 
        string[] instructionPart = instruction.Replace(",", "").Replace(")", "").Replace("(", "").Split(' ');

        switch(instructionPart[0])
        {
          case "MOVT":
          case "MOVW":
            instructions.Add(new MOV(instructionPart)); break;
          case "B":
          case "BAL":
          case "BPL":
            instructions.Add(new Branch(instructionPart)); break;
          //case "STR":
          //case "LDR":
          //    instructions.Add(new SingleDataTransfer(instructionPart)); 
          //  break;

        }
      }
      //GetParsedCondCodes();
    }

    //protected string GetParsedCondCodes(string condition)
    //{
    //  switch (condition)
    //  {
    //    case "MOVT": return "0000";
    //    case "NE": return "0001";
    //    case "CS": return "0010";
    //    case "CC": return "0011";
    //    default: return "";
    //  }
    //}

    /* https://stackoverflow.com/questions/14657643/writing-to-txt-file-with-streamwriter-and-filestream */
    public void ExportToKernel(string outptPath)
    {
      List<byte[]> output = new List<byte[]>(); 
      foreach(Instruction instruction in instructions)
      {
        Console.WriteLine(instruction.originalInt + instruction.ToString());

        output.Add(instruction.ConvertBinaryInsToByteArray());
      }

      File.WriteAllBytes(outptPath, output.SelectMany(x => x).ToArray());


    }

  }
}