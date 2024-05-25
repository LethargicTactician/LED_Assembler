using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.ExceptionServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace PL_Assembler
{

  public class Assembler
  {
    private List<Instruction> instructions;
    private List<string> instructionList;
    //private Dictionary<string, int> labels;
    //private int[] labelValues;
    //private string[] labelInputs;
    //private string[] labelOutputs;
    //public int currentAddress = 0;



    public Assembler()
    {
      instructions = new List<Instruction>();
      instructionList = new List<string>();

    }
    
    private string ParseInstructions(string line)
    {
      
      return line.Replace("\n", "").Replace("\r", "").Trim();
    }

    public void LoadAssemblyFile(string assemblyPath)
    {
      string[] lines = File.ReadAllLines(assemblyPath);
      foreach (string line in lines)
      {
        if (!string.IsNullOrWhiteSpace(line) && !line.StartsWith("#"))
        {
          string instruction = ParseInstructions(line);
          if (instruction != null)
          {
            Console.WriteLine(instruction);
            instructionList.Add(instruction);
          }
        }
      }
      ProcessInstruction();
      UpdateOffset();
    }

    

    public void ProcessInstruction()
    {
      //int currentAddress = 0;

      foreach (string instruction in instructionList)
      { 
        string[] instructionPart = instruction.Replace(",", "").Replace(")", "").Replace("(", "").Split(' ');

        switch (instructionPart[0])
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
          case "BL":
            #endregion
            instructions.Add(new Branch(instructionPart));
            break;

          case "BX":
            instructions.Add(new BranchExchange(instructionPart));
            break;
          #region Single Data Transfer
          case "STR":
          case "LDR":
            #endregion
            instructions.Add(new SingleDataTransfer(instructionPart)); 
            break;
          case "STMEA":
          case "LDMEA":
            instructions.Add(new StackOperations(instructionPart));
            break;
          #region Unga Bunga - Data processing
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
        //currentAddress +=2;
      }
      //currentAddress = 0;
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

    public void UpdateOffset()
    {
      for(int i = 0; i < instructions.Count; i++)
      {
        //Console.WriteLine($"i: {instructions[i]} --------------------");
        for(int j = 0; j < instructions.Count; j++)
        {
          //Console.WriteLine($"j: {instructions[j]} --------------------");
          if (instructions[i].Title.Equals("B") && !instructions[i].Equals(instructions[j]))
          {
            if (instructions[i].label != null && instructions[j].label != null)
            {
                //Console.WriteLine($"{i} || {j} {instructions[i].Title}: {instructions[i].label} || {instructions[j].Title}: {instructions[j].label} ");
              if (instructions[i].label.Equals(instructions[j].label))
              {
               // Console.WriteLine($"FOUND: {i} || {j} {instructions[i].Title}: {instructions[i].label} || {instructions[j].Title}: {instructions[j].label} ");
                int hgfdsa = (j - i) + (-2);
                Console.WriteLine("calc: " + hgfdsa);                
                  ((Branch)instructions[i]).UpdateOffsetValue(hgfdsa);
              }
            }
          }
        }
      }
    }
  }
  //public void UpdateOffset()
  //{
  //  for (int i = 0; i < instructions.Count; i++)
  //  {
  //    for (int j = 0; j < instructions.Count; j++)
  //    {
  //      if (instructions[i].Title.Equals("B") || instructions[i].Title.Equals("BL"))
  //      {
  //        if (instructions[i].label != null && instructions[j].label != null)
  //        {
  //          if (instructions[i].label.Equals(instructions[j].label))
  //          {
  //            int offsetValue = (j - i) * 4 - 8;  // PC-relative offset
  //            if (instructions[i] is Branch)
  //            {
  //              ((Branch)instructions[i]).UpdateOffsetValue(offsetValue);
  //            }
  //            else if (instructions[i] is BranchLink)
  //            {
  //              ((BranchLink)instructions[i]).UpdateOffsetValue(offsetValue);
  //            }
  //          }
  //        }
  //      }
  //    }
  //  }
  //}

}