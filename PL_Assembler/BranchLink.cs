﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PL_Assembler
{
  public class BranchLink : Instruction
  {
    //I WAS USING THIS CLASS BUT NOT ANYMORE AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA

    public BranchLink(string[] instructions) : base(instructions)
    {
      ParseInstructions(instructions);
      Offset = 0;
      ProduceInstruction();
    }

    private string B { get; set; } = "101";
    private string L { get; set; } = "1";  
    private int Offset { get; set; }
    private string RealOffset { get; set; }

    public override void ParseInstructions(string[] instructions)
    {
      Title = "BL";
      Cond = "1110";  
      SetLabel(instructions);
    }

    public void UpdateOffsetValue(int offsetValue)
    {
      TurnToBinary(offsetValue);
      ProduceInstruction();
    }

    public void TurnToBinary(int numValue)
    {
      string binaryWhatever = Convert.ToString(numValue, 2);
      if (binaryWhatever.Length > 24) binaryWhatever = binaryWhatever.Substring(8);
      binaryWhatever = binaryWhatever.PadLeft(24, numValue < 0 ? '1' : '0');

      RealOffset = binaryWhatever;
    }

    public override void ProduceInstruction()
    {
      BinaryOutput = $"{Cond}{B}{L}{RealOffset}";
    }

    public override string ToString()
    {
      return $": {BinaryOutput} ({BinaryOutput.Length}) <- BL Res";
    }
  }
}
