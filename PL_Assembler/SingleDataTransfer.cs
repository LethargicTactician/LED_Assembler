﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace PL_Assembler
{
  /* Notes
  * LDR loads a value from memory into a register. = can be null
  * STR stores a value from a register into memory. = can be null
  * R indicates that the instruction affects the condition flags.
  * <cond> is an optional condition code.
  * <Rd> is the destination register.
  * <Rn> is the base register containing the memory address.
  * <offset> is an optional immediate value or register offset.
   * */

  public class SingleDataTransfer : Instruction
  {
    public SingleDataTransfer(string[] instructions) : base(instructions)
    {
      ParseInstructionsForPookie(instructions);
      ProduceInstruction();
    }
    public string _type { get; set; } = "0";
    public string L { get; set; } = "0";
    public string I { get; set; } = "0";
    public string P { get; set; } = "0";
    public string U { get; set; } = "0";
    public string B { get; set; } = "0";
    public string W { get; set; } = "0";
    public string Offset { get; set; } = "0";

    public override void ParseInstructionsForPookie(string[] instructions)
    {
      //assuming ldr is represented as 01 and str is 00?
      if (instructions[0].Equals("LDR")){
        _type = "1";
      }

      SetUpRd(instructions[0]);
      SetUpRn(instructions[1]);
      Offset = "000000000000";

    }
    //compiler breaks whenever we set up this method - We'd still need to convert Offset and S into binary wah

    //public void SetUpImmediateOp(string thing)
    //{
    //  int value = ConvertStringToInt(thing);
    //  string binaryWhatever = Convert.ToString(value, 2).PadLeft(16, '0');

    //  S = binaryWhatever.Substring(4);
    //  Offset = binaryWhatever.Substring(0,12);
    //}

    public override void ProduceInstruction()
    {
      BinaryMeBBG = $"{Cond}01{I}{P}{U}{B}{W}{L}{Rn}{Rd}{Offset}";
    }

    public override string ToString()
    {
      return $"{BinaryMeBBG} ({BinaryMeBBG.Length}) <-DP Res";
    }
  }
}