using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace PL_Assembler
{
  // (B commands like B{cond}, B Loop, BEQ (error))
  public class Branch : Instruction
  {
    //private int memoryAddress;

    public Branch(string[] instructions) : base(instructions)
    {
      ParseInstructions(instructions);
      Offset = 0;
      ProduceInstruction();
    }

    private string B { get; set; } = "101"; 
    private string L { get; set; } = "0";
    private int Offset { get; set; }
    private string RealOffset { get; set; }


    public override void ParseInstructions(string[] instructions)
    {
      Title = "B";
      if (instructions[0].Equals("BPL"))
      {
        Cond = "1010";
      }
      else
      {
        Cond = "1110";
      }

      if (instructions[0].Equals("BL"))
      {
        L = "1";
      }

      //SetUpB(instructions[0]);
      SetLabel(instructions);
      Console.WriteLine("HI: " + label);
            //Offset = int.Parse(instructions[1]);
      //TurnToBinary(Offset)/*;*/
    }

    public void UpdateOffsetValue(int offsetValue)
    {
      TurnToBinary(offsetValue);
      ProduceInstruction();
    }

    public void SetUpB(string b)
    {
      b = b.Replace("B", "");
      //if (b.Equals("PL")) B = "101";
      //if (b.Equals("BL")) B = "0";
    }

    public void TurnToBinary(int numValue)
    {
      Console.WriteLine("a: " + numValue);
      string binaryWhatever = Convert.ToString(numValue, 2);
      if (binaryWhatever.Length > 24) binaryWhatever = binaryWhatever.Substring(8);
      binaryWhatever = binaryWhatever.PadLeft(24, numValue < 0 ? '1' : '0');
      Console.WriteLine($"b: {binaryWhatever}");

      RealOffset = binaryWhatever;
    }


    public override void ProduceInstruction()
    {
      BinaryOutput = $"{Cond}{B}{L}{RealOffset}";

    }

    public override string ToString()
    {
      return $": {BinaryOutput} ({BinaryOutput.Length}) <- B Res";
    }

  }
}
