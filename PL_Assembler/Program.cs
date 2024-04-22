namespace PL_Assembler
{
  internal class Program
  {
    static void Main(string[] args)
    {
      Assembler assembleRAHH = new Assembler();
      assembleRAHH.LoadAssemblyFile("C:\\Users\\Rosy Gonzalez\\VS2022_Projects\\Programming Languages\\PL_Assembler\\PL_Assembler\\kernels\\assembly.txt");
      assembleRAHH.ExportToKernel("kernel7.img");

      //Console.WriteLine("Hello, World!");
    }
  }
}
