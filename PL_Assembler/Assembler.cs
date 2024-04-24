using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
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
          #region MOV
          case "MOVT":
          case "MOVW":
            #endregion
            instructions.Add(new MOV(instructionPart)); 
            break;
          #region Branch 
          case "B":
          case "BAL":
          case "BPL":
            #endregion
            instructions.Add(new Branch(instructionPart)); 
            break;
          #region Single Data Transfer
          case "STR":
          case "LDR":
            #endregion
            instructions.Add(new SingleDataTransfer(instructionPart)); 
            break;
          #region Data processing
          case "AND":
          case "EOR":
          case "SUB":
          case "SUBS":
          case "RSB":
          case "ADD":
          case "ADC":
          case "SBC":
          case "RSC":
          case "TST":
          case "TEQ":
          case "CMP":
          case "CMN":
          case "ORR":
          case "MOV":
          case "BIC":
          case "MVN":
            #endregion 
            instructions.Add(new DataProcessing(instructionPart));
            break;
        }
      }
    }

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