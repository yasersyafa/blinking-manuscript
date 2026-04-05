using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IState
{
    void OnEnter(GameStateManager manager);
    void OnExecute(GameStateManager manager);
    void OnExit(GameStateManager manager);
}

// IShutdownHandler.cs
public interface IShutdownHandler
{
    void OnShutdownClicked(GameStateManager manager);
}

// IPhoneHandler.cs
public interface IPhoneHandler
{
    void OnPhoneStopClicked(GameStateManager manager);
}

// IBathroomHandler.cs
public interface IBathroomHandler
{
    void OnBathroomClicked(GameStateManager manager);
}

// ISinkHandler.cs
public interface ISinkHandler
{
    void OnSinkClicked(GameStateManager manager);
}

// IFileManagerHandler.cs
public interface IFileManagerHandler
{
    void OnFileManagerClicked(GameStateManager manager);
}

// ICalendarHandler.cs
public interface ICalendarHandler
{
    void OnCalendarClicked(GameStateManager manager);
}

// ITypeWriterHandler.cs
public interface ITypeWriterHandler
{
    void OnTypeWriterFinished(GameStateManager manager);
}