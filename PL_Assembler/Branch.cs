using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PL_Assembler
{
  //uhhhhhhhh yuh branch (B commands like B{cond}, B Loop, BEQ (error))
  public class Branch : Instruction
  {
    public Branch(string[] instructions) : base(instructions)
    {
      ParseInstructionsForPookie(instructions);
      ProduceInstruction();
    }

    private string B { get; set; } = "101";

    private string L { get; set; } = "0";
    private int Offset { get; set; }
    private string RealOffset { get; set; }


    public override void ParseInstructionsForPookie(string[] instructions)
    {
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


      SetUpB(instructions[0]);
      //GetConditionalExecutionBinary(instructions[0]);
      //L = instructions[2];
      Offset = int.Parse(instructions[1]);
      DieLol(Offset);
    }

    public void SetUpB(string b)
    {
      b = b.Replace("B", "");
      if (b.Equals("PL")) B = "101";
    }

    public void DieLol(int whateverthisis)
    {
      Console.WriteLine("a: " + whateverthisis);
      string binaryWhatever = Convert.ToString(whateverthisis, 2);
      if (binaryWhatever.Length > 24) binaryWhatever = binaryWhatever.Substring(8);
      binaryWhatever = binaryWhatever.PadLeft(24, whateverthisis < 0 ? '1' : '0');
      Console.WriteLine($"b: {binaryWhatever}");

      RealOffset = binaryWhatever;
    }


    public override void ProduceInstruction()
    {
      BinaryMeBBG = $"{Cond}{B}{L}{RealOffset}";

    }



    public override string ToString()
    {
      return $": {BinaryMeBBG} ({BinaryMeBBG.Length}) <- B";
    }

  }
}
