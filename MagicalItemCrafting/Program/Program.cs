namespace MagicalCrafting;

public static class Program
{
    public static void Main()
    {
        ProgramMethods.LoadItems();

        bool isGameRunning = true;

        while(isGameRunning)
        {
            ProgramMethods.ShowMenu();
            int input = ProgramMethods.GetUserInput();
            isGameRunning = ProgramMethods.HandleUserChoice(input);
        }
    }
}
