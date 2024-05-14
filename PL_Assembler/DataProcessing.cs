using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Formats.Asn1;
using System.Linq;
using System.Runtime.Intrinsics.Arm;
using System.Text;
using System.Threading.Tasks;

namespace PL_Assembler
{

  /*Notes
  *OP is the operation to be performed (e.g., ADD, SUB, AND, ORR, etc.).
  *{S} is an optional flag indicating whether to update the condition flags based on the result of the operation.
  *{cond} is an optional condition code specifying when the instruction should be executed.
  *Rd is the destination register where the result will be stored.
  *Rn is the first operand register.
  *Operand2 is the second operand, which can be an immediate value, register value, or shifted register value.
   */
  public class DataProcessing : Instruction
  {
    public string S { set; get; } = "0";
    public string I { set; get; } = "1";
    public string OpCode { set; get; }
    public string Operand2 { get; set; }

    public DataProcessing(string[] instructions) : base(instructions)
    {
      ParseInstructions(instructions);
      ProduceInstruction();
    }

    public void GeConditionCodes(string firstCondition)
    {
      switch (firstCondition)
      {
        case "AND":
          OpCode = "0000"; 
          break;
        case "EOR":
          OpCode = "0001"; 
          break;
        case "SUB":
          OpCode = "0010";
          break;
        case "SUBS":
          OpCode = "0010";
          S = "1";
          break;
        case "RSB":
          OpCode = "0011"; 
          break;
        case "ADD":
          OpCode = "0100"; 
          break;
        case "ADC":
          OpCode = "0101";
          break;
        case "SBC":
          OpCode = "0110";
          break;
        case "RSC":
          OpCode   = "0111"; 
          break;
        case "TST":
          OpCode = "1000"; 
          break;
        case "TEQ":
          OpCode = "1001";
          break;
        case "CMP":
          OpCode = "1010"; 
          break;
        case "CMN":
          OpCode = "1011"; 
          break;
        case "ORR":
          OpCode = "1100";
          break;
        case "MOV":
          OpCode = "1101";
          break;
        case "BIC":
          OpCode = "1110";
          break;
        case "MVN":
          OpCode = "1111";
          break;
        default:
          throw new NullReferenceException();
          break;
          //case "ADD": Cond = 
      }

    }

    public override void ParseInstructions(string[] instructions)
    {
      Title = "DP";
      try
      {
        GeConditionCodes(instructions[0]);
        SetUpRd(instructions[1]);
        SetUpRn(instructions[2]); 
        SetUpImmediateOp(instructions[3]);
        SetLabel(instructions);
      }catch (Exception error)
      {
        Console.WriteLine(error + "<--- ERROR");
      }
}

    public void SetUpImmediateOp(string thing)
    {
      I = thing.ToLower().StartsWith('r') ? "0" : "1";
      
      int rep = ConvertStringToInt(thing);
      string binaryurmom = Convert.ToString(rep, 2);
      //(condition) ? true - clause : false - clause
      //if rep less than 0, return 1 else its 0
      //
      Operand2 = binaryurmom.PadLeft(12, rep<0 ? '1' : '0');
       
    }

    public override void ProduceInstruction()
    {
      BinaryOutput = $"{Cond}00{I}{OpCode}{S}{Rn}{Rd}{Operand2}";
    }

    public override string ToString()
    {
      return $"{BinaryOutput} ({BinaryOutput.Length}) <-DP Res";
    }


  }
}
