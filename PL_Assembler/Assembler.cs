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
    //adding labels to the dictionary along with values
    private Dictionary<string, int> labels;
    private int[] labelValues;
    private string[] labelInputs;
    private string[] labelOutputs;



    public Assembler()
    {
      instructions = new List<Instruction>();
      instructionList = new List<string>();
      //same thing here
      labels = new Dictionary<string, int>();
      labelValues = new int[0];
      labelInputs = new string[0];
      labelOutputs = new string[0];

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
        if (!string.IsNullOrWhiteSpace(line) || !line.StartsWith("#"))
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
    }

    public void ProcessInstruction()
    {
      int currentAddress = 0;

      foreach (string instruction in instructionList)
      { 
        string[] instructionPart = instruction.Replace(",", "").Replace(")", "").Replace("(", "").Split(' ');

        foreach (string part in instructionPart)
        {
          if (part.StartsWith(':') || part.StartsWith('$'))
          {
            string label = part.Substring(1);

            if (!labels.ContainsKey(label))
            {
              labels[label] = currentAddress;
            }
          }
          //currentAddress ++; 
        }

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
            string targetLabel = instructionPart[1].TrimStart(':');
            int targetAddress = labels[targetLabel];

            Console.WriteLine($"Target Address: {targetAddress}, Current Address: {currentAddress}");
            int offset = targetAddress - currentAddress - 2;
            Console.WriteLine($"Offset: {offset}");
            instructionPart[1] = offset.ToString();
            instructions.Add(new Branch(instructionPart));
            break;
          #region Single Data Transfer
          case "STR":
          case "LDR":
            #endregion
            instructions.Add(new SingleDataTransfer(instructionPart)); 
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

  }
}