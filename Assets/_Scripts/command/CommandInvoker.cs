using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommandInvoker : MonoBehaviour
{
    PlayerAction InputAction;

    static Queue<ICommand> commandBuffer;
    static List<ICommand> commandHistory;

    static int counter=0;

    // Start is called before the first frame update
    void Start()
    {
        commandBuffer = new Queue<ICommand>();
        commandHistory = new List<ICommand>();

        InputAction = PlayerInputController.controller.inputAction;

        InputAction.Editor.Undo.performed += cntxt => UndoCommand();

    }
    public void UndoCommand()
    {
        Debug.Log("tried to undo");
        if(commandHistory.Count >= 0)
        {
            if (counter > 0)
            {
                counter--;
                commandHistory[counter].Undo();
            }
        }
    }
    public static void AddCommand(ICommand command)
    {
        while (commandHistory.Count > counter)
        {
            commandHistory.RemoveAt(counter);
        }
        commandBuffer.Enqueue(command);
    }
    // Update is called once per frame
    void Update()
    {
        if (commandBuffer.Count > 0)
        {
            ICommand c = commandBuffer.Dequeue();
            c.Execute();

            commandHistory.Add(c);
            counter++;

            Debug.Log("command History length" + commandHistory.Count);
            Debug.Log("counter " + counter);
        }
       
    }
}
