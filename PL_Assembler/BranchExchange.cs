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
  public class BranchExchange : Instruction
  {
    //private int memoryAddress;

    public BranchExchange(string[] instructions) : base(instructions)
    {
      ParseInstructions(instructions);
      ProduceInstruction();
    }


    public override void ParseInstructions(string[] instructions)
    {
      Title = "BX";
      SetUpRn(instructions[1]);
    }

    public override void ProduceInstruction()
    {
      BinaryOutput = $"{Cond}000100101111111111110001{Rn}";
    }

    public override string ToString()
    {
      return $": {BinaryOutput} ({BinaryOutput.Length}) <- B Res";
    }

  }
}
