﻿using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace PL_Assembler
{
  public class StackOperations : Instruction
  {
    public StackOperations(string[] instructions) : base(instructions)
    {
      ParseInstructions(instructions);
      ProduceInstruction();
    }
    //set default values 
    //r13 is the storing register, this is where you load & store values to the other assembly instructions
    //create a variable that will store the values in r13 
    //when the stack assembly file contains for example {R0-R12} this is getting the value of r13 and adding it to all of the instructns 
    //

    private string P { get; set; } = "1";
    private string U { get; set; } = "0";
    private string W { get; set; } = "1";
    private string L { get; set; } = "0";
    private string Rlist { get; set; }

    public override void ParseInstructions(string[] instructions)
    {
      Title = instructions[0].Equals("LDMEA") ? "LDMEA" : "STMEA";

      Console.WriteLine("INT: " + instructions);
      Rlist = GetRegisterList(instructions[2]);
            
      if (!instructions[1].EndsWith("!"))
      {
        this.W = "0";
      }
      SetUpRn(instructions[1].Replace("!", ""));
      GetDelayValue(instructions[1]);
      
    }

    public void SetUpStackINstruction(string[] instructions)
    {
      foreach(string instruction in instructions)
      {
        switch (instructions[0])
        {
          case "LDMED":
            L = "1";
            P = "1";
            U = "1";
            break;
          case "STMEA":
            L="0"; P = "0"; U = "1";
            break;
          case "LDMFD":
            L = "1"; P = "0"; U = "0";
            break;
          case "LDMEA":
            L = "1"; P = "1"; U ="0";
            break;
          case "LDMFA":
            L = "0"; P = "1"; U="1";
            break;
          case "STMFA": 
            L = "0"; P = "1"; U="1";
            break;
          case "STMFD":
            L = "0"; P = "1"; U="0";
            break;
          case "STMED":
            L = "0"; P = "0";U = "0";
            break;
        }
      }

    }

    //public void SetRegister13(string rValue)
    //{
    //  //removes "R" and "!" from "R13!", leaving "13" as the value of that register
    //  rValue = rValue.Replace("!", "").Replace("R", "");

      

    //}

    private string GetRegisterList(string registers)
    {
      registers = registers.Replace("{", "").Replace("}", "").Replace("R", "");
      
      Console.WriteLine("crying: " + registers);
      string finalReg = "";

      if (registers.Contains("-")) {
        var regList = registers.Split('-');
        for (int i = int.Parse(regList[0]); i < int.Parse(regList[1]); i++)
        {
          finalReg += "1";
        }

        finalReg.PadLeft(16, '0');
        //val.Add(registers.Split("-"));
        
      }      
        Console.WriteLine("rosy is gay:" + finalReg);
      return finalReg.ToString();
      //   return Convert.ToString((1 << (int.Parse(regList[1].Replace("R", "")) + 1)) - 1, 2).PadLeft(16, '0');

    }

    public string GetDelayValue(string delayedValue)
    {
      if (delayedValue.EndsWith("*"))
      {
        delayedValue = delayedValue.Replace("*", "");
        delayedValue.PadLeft(16, '0');
      }
      Console.Write("-----> Delayed Valyue" + delayedValue);
      return delayedValue.ToString();
    }

    public override void ProduceInstruction()
    {
      BinaryOutput = $"{Cond}100{P}{U}{0}{W}{L}{Rn}1101{Rlist}";
    }

    public override string ToString()
    {
      return $"{BinaryOutput} ({BinaryOutput.Length}) <- Stack Operation Res";
    }
  }
}
